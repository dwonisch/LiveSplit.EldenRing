using System.Reflection;
using System.Windows.Forms;

namespace LiveSplit.EldenRing.Settings {
    public partial class EldenRingSettingsControl : UserControl {
        private readonly EldenRingSettings settings;

        public EldenRingSettingsControl(EldenRingSettings settings) {
            this.settings = settings;
            this.settings.SettingsChanged += Settings_SettingsChanged;

            InitializeComponent();

            infoLabel.Text = infoLabel.Text.Replace("{VERSION}",
                typeof(EldenRingSettingsControl).Assembly.GetName().Version.ToString());

            UpdateSettings();
        }

        private void Settings_SettingsChanged(object sender, System.EventArgs e) {
            UpdateSettings();
        }

        private void timingMethodCombo_SelectedIndexChanged(object sender, System.EventArgs e) {
            settings.TimingMethod = timingMethodCombo.SelectedIndex;
        }

        public void UpdateSettings() {
            timingMethodCombo.SelectedIndex = settings.TimingMethod;
        }
    }
}
