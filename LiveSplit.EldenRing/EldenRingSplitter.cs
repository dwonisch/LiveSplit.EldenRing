using System;
using System.Diagnostics;
using System.Linq;
using LiveSplit.EldenRing.Configs;
using LiveSplit.Model;
using Memory;

namespace LiveSplit.EldenRing {
    public class EldenRingSplitter : IDisposable {

        private readonly LiveSplitState liveSplitState;
        private Process process;
        private ProcessMemory processMemory;
        private IGameConfig config;
        private readonly TimerModel timer = new TimerModel();

        private IntPtr gameTimeBaseAddress;

        private DateTime lastUpdate = DateTime.Now;
        private IntPtr characterInfoBase;

        public EldenRingSplitter(LiveSplitState liveSplitState) {

            this.liveSplitState = liveSplitState;
            timer.CurrentState = liveSplitState;

            liveSplitState.OnStart += (sender, args) => {
                liveSplitState.IsGameTimePaused = true; // do not use LiveSplit GameTime
            };

            liveSplitState.OnReset += (sender, args) => {
                Reset();
            };
        }


        public void Update() {
            if (DateTime.Now - lastUpdate < TimeSpan.FromMilliseconds(40))
                return;

            lastUpdate = DateTime.Now;

            if (process == null || processMemory == null) {
                process = GetGameProcess();

                if (process == null)
                    return;

                Initialize();
            }

            if (gameTimeBaseAddress == IntPtr.Zero) {
                InitializeAddresses();
            }

            if (!UpdateGameTime()) {
                return; // Time is not running, so do not Update splits
            }
        }
        
        private bool UpdateGameTime() {
            var currentMilliseconds = processMemory.ReadInt64(gameTimeBaseAddress);

            if (currentMilliseconds == 0) {
                return false;
            }

            if (liveSplitState.CurrentPhase == TimerPhase.NotRunning) {
                Trace.WriteLine($"Triggering AutoStart at {currentMilliseconds} ms");
                AutoStart();
                return false;
            }

            liveSplitState.SetGameTime(TimeSpan.FromMilliseconds(currentMilliseconds));
            
            return currentMilliseconds != 0;
        }

        private void Initialize() {
            config = new GameConfigV102();
            
            processMemory = ProcessMemory.OpenRead(process.Id);
            
            InitializeAddresses();
        }

        private void InitializeAddresses() {
            var baseModule = processMemory.GetMainModule();

            characterInfoBase = baseModule.ReadIntPtr(config.GameTimeAddress);
            if (characterInfoBase == IntPtr.Zero)
                return;

            gameTimeBaseAddress = characterInfoBase + config.GameTimeOffset;
            Trace.WriteLine($"gameTimeBaseAddress: {(long)gameTimeBaseAddress:X2}");
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
