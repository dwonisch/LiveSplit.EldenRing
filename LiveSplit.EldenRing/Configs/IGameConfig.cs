using System;

namespace LiveSplit.EldenRing.Configs {
    interface IGameConfig {
        IntPtr GameTimeAddress { get; }
        int GameTimeOffset { get; }
        IntPtr ScreenStateAddress { get; }
        int ScreenStateOffset { get; }
    }
}
