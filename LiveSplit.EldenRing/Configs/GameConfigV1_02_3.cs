using System;

namespace LiveSplit.EldenRing.Configs {
    public class GameConfigV1_02_3 : IGameConfig {
        public IntPtr GameTimeAddress => new IntPtr(0x3C4B218);
        public int GameTimeOffset => 0xA0;
    }
}