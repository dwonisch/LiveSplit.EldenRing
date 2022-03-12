using System;
using System.Diagnostics;
using System.Xml;

namespace LiveSplit.EldenRing.Settings {
    public class EldenRingSettings {
        public event EventHandler SettingsChanged;
        public int TimingMethod { get; set; }

        public void LoadFrom(XmlNode settings) {
            Trace.WriteLine($"Load: {settings.OuterXml}");
            Trace.WriteLine($"Load: {settings.InnerXml}");


            if (settings["EldenRingSplitterSettings"] == null)
                return;

            TimingMethod = GetIntSetting(settings["EldenRingSplitterSettings"]);

            if(SettingsChanged != null)
                SettingsChanged(this, EventArgs.Empty);
        }

        public void SaveTo(XmlNode setting) {
            var element = setting.OwnerDocument.CreateElement("EldenRingSplitterSettings");
            element.SetAttribute("TimingMethod", TimingMethod.ToString());
            setting.AppendChild(element);

            Trace.WriteLine($"Save: {setting.ToString()}");

        }

        private XmlAttribute CreateAttribute(XmlNode setting, string qualifiedName, string value) {
            var xmlAttribute = setting.OwnerDocument.CreateAttribute(qualifiedName);
            xmlAttribute.Value = value;
            return xmlAttribute;
        }

        private static int GetIntSetting(XmlNode node) {
            var nodeAttribute = node.Attributes["TimingMethod"];
            if (nodeAttribute == null)
                return 0;
            return int.Parse(nodeAttribute.Value);
        }
    }
}