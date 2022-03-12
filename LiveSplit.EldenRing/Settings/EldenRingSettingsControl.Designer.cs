
namespace LiveSplit.EldenRing.Settings {
    partial class EldenRingSettingsControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EldenRingSettingsControl));
            this.infoLabel = new System.Windows.Forms.Label();
            this.timingMethodCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoLabel.Location = new System.Drawing.Point(0, 392);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(464, 94);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = resources.GetString("infoLabel.Text");
            // 
            // timingMethodCombo
            // 
            this.timingMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timingMethodCombo.FormattingEnabled = true;
            this.timingMethodCombo.Items.AddRange(new object[] {
            "Real Time (with Load Remover)",
            "InGame Time"});
            this.timingMethodCombo.Location = new System.Drawing.Point(89, 8);
            this.timingMethodCombo.Name = "timingMethodCombo";
            this.timingMethodCombo.Size = new System.Drawing.Size(372, 21);
            this.timingMethodCombo.TabIndex = 1;
            this.timingMethodCombo.SelectedIndexChanged += new System.EventHandler(this.timingMethodCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Timing Method:";
            // 
            // EldenRingSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timingMethodCombo);
            this.Controls.Add(this.infoLabel);
            this.Name = "EldenRingSettingsControl";
            this.Size = new System.Drawing.Size(464, 486);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ComboBox timingMethodCombo;
        private System.Windows.Forms.Label label2;
    }
}
