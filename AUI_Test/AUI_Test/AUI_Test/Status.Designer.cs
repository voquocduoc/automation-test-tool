namespace AUI_Test
{
    partial class Status
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Status));
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            this.label.Click += new System.EventHandler(this.label_Click);
            // 
            // Status
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Status";
            // 
            // 
            // 
            this.RootElement.AccessibleDescription = resources.GetString("Status.RootElement.AccessibleDescription");
            this.RootElement.AccessibleName = resources.GetString("Status.RootElement.AccessibleName");
            this.RootElement.Alignment = ((System.Drawing.ContentAlignment)(resources.GetObject("Status.RootElement.Alignment")));
            this.RootElement.AngleTransform = ((float)(resources.GetObject("Status.RootElement.AngleTransform")));
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.FlipText = ((bool)(resources.GetObject("Status.RootElement.FlipText")));
            this.RootElement.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("Status.RootElement.Margin")));
            this.RootElement.Padding = ((System.Windows.Forms.Padding)(resources.GetObject("Status.RootElement.Padding")));
            this.RootElement.Text = resources.GetString("Status.RootElement.Text");
            this.RootElement.TextOrientation = ((System.Windows.Forms.Orientation)(resources.GetObject("Status.RootElement.TextOrientation")));
            this.ShowIcon = false;
            this.ThemeName = "Desert";
            this.Load += new System.EventHandler(this.Status_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private System.Windows.Forms.Label label;
    }
}
