using System;
using System.Diagnostics;
using Memory;

namespace LiveSplit.EldenRing.Timer {
    public class RealTimeTimer : ITimer {
        public RealTimeTimer(IntPtr gameTimeBaseAddress, IntPtr isInGameCheck) {
            this.isInGameCheck = isInGameCheck;
            this.gameTimeBaseAddress = gameTimeBaseAddress;
            internalTimer = new Stopwatch();

            Reset();
        }

        private readonly Stopwatch internalTimer;
        private readonly IntPtr gameTimeBaseAddress;
        private readonly IntPtr isInGameCheck;

        private bool isStarted;
        private long startOffset;

        public TimeSpan UpdateGameTime(ProcessMemory processMemory) {
            var currentMilliseconds = processMemory.ReadInt64(gameTimeBaseAddress);

            if (!isStarted && currentMilliseconds < 1000) {
                isStarted = true;
                startOffset = currentMilliseconds;
                
                internalTimer.Restart();
            } else if (!isStarted && currentMilliseconds > 0) {
                return TimeSpan.Zero; //invalid start conditions
            }

            var inGame = InGame(processMemory);
            if (internalTimer.IsRunning && !inGame) {
                internalTimer.Stop();
            } else if (!internalTimer.IsRunning && inGame) {
                internalTimer.Start();
            }

            return internalTimer.Elapsed.Add(TimeSpan.FromMilliseconds(startOffset));
        }

        private bool InGame(ProcessMemory processMemory) {
            var readInt32 = processMemory.ReadInt32(isInGameCheck);
            return readInt32 == 0;
        }

        public void Reset() {
            isStarted = false;
            startOffset = 0;

            internalTimer.Reset();
        }
    }
}
