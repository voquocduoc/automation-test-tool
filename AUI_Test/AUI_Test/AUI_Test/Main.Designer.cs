namespace AUI_Test
{
    partial class Main
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
            this.breezeTheme1 = new Telerik.WinControls.Themes.BreezeTheme();
            this.radColorBox1 = new Telerik.WinControls.UI.RadColorBox();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            ((System.ComponentModel.ISupportInitialize)(this.radColorBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // radColorBox1
            // 
            this.radColorBox1.Location = new System.Drawing.Point(444, 111);
            this.radColorBox1.Name = "radColorBox1";
            this.radColorBox1.Size = new System.Drawing.Size(100, 20);
            this.radColorBox1.TabIndex = 0;
            this.radColorBox1.Text = "radColorBox1";
            this.radColorBox1.Value = System.Drawing.Color.Empty;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 214);
            this.Controls.Add(this.radColorBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.radColorBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.BreezeTheme breezeTheme1;
        private Telerik.WinControls.UI.RadColorBox radColorBox1;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
    }
}

