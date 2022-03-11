using System;

namespace LiveSplit.EldenRing.Configs {
    public class GameConfigV1_02 : IGameConfig {
        public IntPtr GameTimeAddress => new IntPtr(0x3C481B8);
        public int GameTimeOffset => 0xA0;
    }
}