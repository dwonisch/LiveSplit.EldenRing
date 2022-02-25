using System;
using System.Reflection;
using LiveSplit.EldenRing;
using LiveSplit.Model;
using LiveSplit.UI.Components;

//Register component factory, to be found by LiveSplit
[assembly: ComponentFactory(typeof(EldenRingComponentFactory))]

namespace LiveSplit.EldenRing {
    public class EldenRingComponentFactory : IComponentFactory {
        public string UpdateName => ComponentName;
        public string XMLURL => "https://raw.githubusercontent.com/dwonisch/LiveSplit.EldenRing/master/Components/update.EldenRingAutoSplit.xml";
        public string UpdateURL => "https://raw.githubusercontent.com/dwonisch/LiveSplit.EldenRing/master/Components/LiveSplit.EldenRing.dll";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public IComponent Create(LiveSplitState state) {
            return new EldenRingComponent(state);
        }

        public string ComponentName => "Elden Ring - Ingame Time Compoennt";
        public string Description => "Elden Ring - Ingame Timer";
        public ComponentCategory Category => ComponentCategory.Timer;
    }
}
