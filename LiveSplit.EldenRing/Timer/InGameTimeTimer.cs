using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memory;

namespace LiveSplit.EldenRing.Timer {
    public class InGameTimeTimer : ITimer {
        private readonly IntPtr gameTimeBaseAddress;
        
        public InGameTimeTimer(IntPtr gameTimeBaseAddress) {
            this.gameTimeBaseAddress = gameTimeBaseAddress;
        }

        public TimeSpan UpdateGameTime(ProcessMemory processMemory) {
            var currentMilliseconds = processMemory.ReadInt64(gameTimeBaseAddress);
            return TimeSpan.FromMilliseconds(currentMilliseconds);
        }
    }
}
