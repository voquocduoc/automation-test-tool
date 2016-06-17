namespace AUI_Test
{
    partial class Open_Project
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
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.radBrowseEditor = new Telerik.WinControls.UI.RadBrowseEditor();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radBrowseEditor
            // 
            this.radBrowseEditor.Location = new System.Drawing.Point(250, 26);
            this.radBrowseEditor.Name = "radBrowseEditor";
            this.radBrowseEditor.Size = new System.Drawing.Size(159, 18);
            this.radBrowseEditor.TabIndex = 0;
            this.radBrowseEditor.Text = "radBrowseEditor";
            this.radBrowseEditor.ThemeName = "Desert";
            this.radBrowseEditor.ValueChanged += new System.EventHandler(this.radBrowseEditor_ValueChanged);
            // 
            // Open_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::AUI_Test.Properties.Resources.nenopen;
            this.ClientSize = new System.Drawing.Size(455, 128);
            this.Controls.Add(this.radBrowseEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Open_Project";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Open Project";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.Open_Project_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.UI.RadBrowseEditor radBrowseEditor;
    }
}
