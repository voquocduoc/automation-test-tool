namespace AUI_Test
{
    partial class RunScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunScript));
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radSplitButtonScript = new Telerik.WinControls.UI.RadSplitButton();
            this.radSplitButtondata = new Telerik.WinControls.UI.RadSplitButton();
            this.radButtonOK = new Telerik.WinControls.UI.RadButton();
            this.radButtoncancel = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitButtonScript)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitButtondata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtoncancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(18, 22);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(56, 23);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Script";
            this.radLabel1.ThemeName = "Aqua";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(27, 59);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(47, 23);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "Data";
            this.radLabel2.ThemeName = "Aqua";
            // 
            // radSplitButtonScript
            // 
            this.radSplitButtonScript.DefaultItem = null;
            this.radSplitButtonScript.Location = new System.Drawing.Point(93, 22);
            this.radSplitButtonScript.Name = "radSplitButtonScript";
            this.radSplitButtonScript.Size = new System.Drawing.Size(243, 26);
            this.radSplitButtonScript.TabIndex = 2;
            this.radSplitButtonScript.ThemeName = "Aqua";
            // 
            // radSplitButtondata
            // 
            this.radSplitButtondata.DefaultItem = null;
            this.radSplitButtondata.Location = new System.Drawing.Point(93, 59);
            this.radSplitButtondata.Name = "radSplitButtondata";
            this.radSplitButtondata.Size = new System.Drawing.Size(243, 26);
            this.radSplitButtondata.TabIndex = 3;
            this.radSplitButtondata.ThemeName = "Aqua";
            // 
            // radButtonOK
            // 
            this.radButtonOK.Location = new System.Drawing.Point(153, 102);
            this.radButtonOK.Name = "radButtonOK";
            this.radButtonOK.Size = new System.Drawing.Size(89, 22);
            this.radButtonOK.TabIndex = 4;
            this.radButtonOK.Text = "Run";
            this.radButtonOK.ThemeName = "Aqua";
            this.radButtonOK.Click += new System.EventHandler(this.radButtonOK_Click);
            // 
            // radButtoncancel
            // 
            this.radButtoncancel.Location = new System.Drawing.Point(255, 102);
            this.radButtoncancel.Name = "radButtoncancel";
            this.radButtoncancel.Size = new System.Drawing.Size(89, 22);
            this.radButtoncancel.TabIndex = 5;
            this.radButtoncancel.Text = "Cancel";
            this.radButtoncancel.ThemeName = "Aqua";
            this.radButtoncancel.Click += new System.EventHandler(this.radButtoncancel_Click);
            // 
            // RunScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(361, 132);
            this.Controls.Add(this.radButtoncancel);
            this.Controls.Add(this.radButtonOK);
            this.Controls.Add(this.radSplitButtondata);
            this.Controls.Add(this.radSplitButtonScript);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RunScript";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunScript";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.RunScript_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitButtonScript)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitButtondata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtoncancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadSplitButton radSplitButtonScript;
        private Telerik.WinControls.UI.RadSplitButton radSplitButtondata;
        private Telerik.WinControls.UI.RadButton radButtonOK;
        private Telerik.WinControls.UI.RadButton radButtoncancel;
    }
}
