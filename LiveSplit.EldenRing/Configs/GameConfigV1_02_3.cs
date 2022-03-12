using System;

namespace LiveSplit.EldenRing.Configs {
    public class GameConfigV1_02_3 : IGameConfig {
        public IntPtr GameTimeAddress => new IntPtr(0x3C4B218);
        public int GameTimeOffset => 0xA0;
        public IntPtr ScreenStateAddress => new IntPtr(0x03C58B90);
        public int ScreenStateOffset => 0x718;

    }
}