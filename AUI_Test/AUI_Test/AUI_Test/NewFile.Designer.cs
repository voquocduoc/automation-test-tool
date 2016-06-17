namespace AUI_Test
{
    partial class NewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFile));
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.Filename = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radButtonAddfile = new Telerik.WinControls.UI.RadButton();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filename)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonAddfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(17, 34);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(85, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "File Name";
            this.radLabel1.ThemeName = "Aqua";
            this.radLabel1.UseWaitCursor = true;
            this.radLabel1.Click += new System.EventHandler(this.radLabel1_Click);
            // 
            // Filename
            // 
            this.Filename.Location = new System.Drawing.Point(134, 36);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(244, 20);
            this.Filename.TabIndex = 2;
            this.Filename.TabStop = false;
            this.Filename.ThemeName = "Aqua";
            this.Filename.UseWaitCursor = true;
            this.Filename.TextChanged += new System.EventHandler(this.radTextBox1_TextChanged);
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Location = new System.Drawing.Point(292, 75);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(87, 24);
            this.radButtonCancel.TabIndex = 4;
            this.radButtonCancel.Text = "Cancel";
            this.radButtonCancel.ThemeName = "Aqua";
            this.radButtonCancel.UseWaitCursor = true;
            // 
            // radButtonAddfile
            // 
            this.radButtonAddfile.Location = new System.Drawing.Point(190, 75);
            this.radButtonAddfile.Name = "radButtonAddfile";
            this.radButtonAddfile.Size = new System.Drawing.Size(90, 24);
            this.radButtonAddfile.TabIndex = 5;
            this.radButtonAddfile.Text = "Add File";
            this.radButtonAddfile.ThemeName = "Aqua";
            this.radButtonAddfile.UseWaitCursor = true;
            this.radButtonAddfile.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // NewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(398, 113);
            this.Controls.Add(this.radButtonAddfile);
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewFile";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New File";
            this.ThemeName = "Aqua";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this._ReadFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filename)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonAddfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox Filename;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadButton radButtonAddfile;
        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
    }
}
