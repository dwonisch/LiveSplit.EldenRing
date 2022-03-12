using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LiveSplit.EldenRing.Configs;
using LiveSplit.EldenRing.Settings;
using LiveSplit.EldenRing.Timer;
using LiveSplit.Model;
using Memory;

namespace LiveSplit.EldenRing {
    public class EldenRingSplitter : IDisposable {

        private readonly LiveSplitState liveSplitState;
        private readonly EldenRingSettings settings;
        private Process process;
        private ProcessMemory processMemory;
        private IGameConfig config;
        private readonly TimerModel timer = new TimerModel();

        private IntPtr gameTimeBaseAddress;
        
        private ITimer timingMethod;

        public EldenRingSplitter(LiveSplitState liveSplitState, EldenRingSettings settings) {

            this.liveSplitState = liveSplitState;
            this.settings = settings;
            timer.CurrentState = liveSplitState;

            liveSplitState.OnStart += (sender, args) => {
                liveSplitState.IsGameTimePaused = true; // do not use LiveSplit GameTime
            };

            liveSplitState.OnReset += (sender, args) => {
                Reset();
            };

            //Trace.WriteLine($"Running on {Thread.CurrentThread.ManagedThreadId}");
        }


        public void Update() {
            if (process == null || processMemory == null) {
                process = GetGameProcess();

                if (process == null)
                    return;

                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                Initialize();
            }

            if (gameTimeBaseAddress == IntPtr.Zero) {
                InitializeAddresses();
            }

            //Trace.WriteLine($"Updating on {Thread.CurrentThread.ManagedThreadId}");

            if (!UpdateGameTime()) {
                return; // Time is not running, so do not Update splits
            }
        }
        
        private bool UpdateGameTime() {
            if (timingMethod == null)
                return false;

            var currentTime = timingMethod.UpdateGameTime(processMemory);

            if (currentTime == TimeSpan.Zero) {
                return false;
            }

            if (liveSplitState.CurrentPhase == TimerPhase.NotRunning) {
                Trace.WriteLine($"Triggering AutoStart at {currentTime.TotalMilliseconds} ms");
                AutoStart();
                return false;
            }

            liveSplitState.SetGameTime(currentTime);
            
            return currentTime != TimeSpan.Zero;
        }

        private void Initialize() {
            processMemory = ProcessMemory.OpenReadQuery(process.Id);

            config = new GameConfigV1_02_3();

            //string version = processMemory.GetFileVersion();
            //Trace.WriteLine($"Game Version detected: {version}");

            //switch (version) {
            //    case "1.2.1.0":
            //    case "1.2.2.0": config = new GameConfigV1_02(); break; 
            //    case "1.2.3.0": config = new GameConfigV1_02_3(); break;
            //    default: config = new GameConfigV1_02_3(); break;
            //}

            InitializeAddresses();
        }

        private void InitializeAddresses() {
            var baseModule = processMemory.GetMainModule();

            var characterInfoBase = baseModule.ReadIntPtr(config.GameTimeAddress);
            if (characterInfoBase == IntPtr.Zero)
                return;

            gameTimeBaseAddress = characterInfoBase + config.GameTimeOffset;
            Trace.WriteLine($"gameTimeBaseAddress: {(long)gameTimeBaseAddress:X2}");

            var screenStateBase = baseModule.ReadIntPtr(config.ScreenStateAddress);
            var screenStateBaseAddress = screenStateBase + config.ScreenStateOffset;
            Trace.WriteLine($"screenStateBaseAddress: {(long)screenStateBaseAddress:X2}");

            if(settings.TimingMethod == 0)
                timingMethod = new RealTimeTimer(gameTimeBaseAddress, screenStateBaseAddress);
            else
                timingMethod = new InGameTimeTimer(gameTimeBaseAddress);
        }

        private Process GetGameProcess() {
            process?.Dispose();
            processMemory?.Dispose();

            var processes = Process.GetProcessesByName("eldenring");

            if (processes.Length == 0)
                return null;

            process = processes.First();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => Reset();

            return process;
        }

        private void AutoStart() {
            timer.Start();
        }
        private void Reset() {
            processMemory?.Dispose();
            processMemory = null;
            process?.Dispose();
            process = null;
        }

        public void Dispose() {
            process?.Dispose();
            processMemory?.Dispose();
        }
    }
}
