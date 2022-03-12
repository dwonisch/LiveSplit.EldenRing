using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memory;

namespace LiveSplit.EldenRing.Timer {
    public interface ITimer {
        TimeSpan UpdateGameTime(ProcessMemory processMemory);
    }
}
