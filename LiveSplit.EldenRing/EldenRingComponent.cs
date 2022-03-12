using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.EldenRing.Settings;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.EldenRing {
    public class EldenRingComponent : IComponent {
        private readonly EldenRingSettingsControl control;
        private readonly EldenRingSplitter splitter;
        private readonly EldenRingSettings settings;

        public EldenRingComponent(LiveSplitState liveSplitState) {
            settings = new EldenRingSettings();
            control = new EldenRingSettingsControl(settings);
            splitter = new EldenRingSplitter(liveSplitState, settings);
        }

        public void Dispose() {
            control?.Dispose();
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion) {
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion) {
        }

        public Control GetSettingsControl(LayoutMode mode) {
            return control;
        }

        public XmlNode GetSettings(XmlDocument document) {
            var node = document.CreateNode(XmlNodeType.Element, "EldenRing", "");
            this.settings.SaveTo(node);
            return node;
        }

        public void SetSettings(XmlNode settings) {
            this.settings.LoadFrom(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) {
            splitter.Update();
        }

        public string ComponentName => "Elden Ring - Ingame Time Component";
        public float HorizontalWidth => 0;
        public float MinimumHeight => 0;
        public float VerticalHeight => 0;
        public float MinimumWidth => 0;
        public float PaddingTop => 0;
        public float PaddingBottom => 0;
        public float PaddingLeft => 0;
        public float PaddingRight => 0;
        public IDictionary<string, Action> ContextMenuControls => null;
    }
}
