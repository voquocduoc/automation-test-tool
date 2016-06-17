namespace AUI_Test
{
    partial class NewProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProject));
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxNameProject = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxLocation = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonBrowse = new Telerik.WinControls.UI.RadButton();
            this.radButtonOk = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancle = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxNameProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(21, 17);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(112, 22);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Name Project";
            this.radLabel1.ThemeName = "Aqua";
            this.radLabel1.Click += new System.EventHandler(this.radLabel1_Click);
            // 
            // radTextBoxNameProject
            // 
            this.radTextBoxNameProject.Location = new System.Drawing.Point(131, 19);
            this.radTextBoxNameProject.Name = "radTextBoxNameProject";
            this.radTextBoxNameProject.Size = new System.Drawing.Size(245, 20);
            this.radTextBoxNameProject.TabIndex = 1;
            this.radTextBoxNameProject.TabStop = false;
            this.radTextBoxNameProject.ThemeName = "Aqua";
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.Color.Transparent;
            this.radLabel2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(53, 56);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(72, 22);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Location";
            // 
            // radTextBoxLocation
            // 
            this.radTextBoxLocation.Location = new System.Drawing.Point(131, 58);
            this.radTextBoxLocation.Name = "radTextBoxLocation";
            this.radTextBoxLocation.Size = new System.Drawing.Size(245, 20);
            this.radTextBoxLocation.TabIndex = 3;
            this.radTextBoxLocation.TabStop = false;
            this.radTextBoxLocation.ThemeName = "Aqua";
            // 
            // radButtonBrowse
            // 
            this.radButtonBrowse.Location = new System.Drawing.Point(394, 58);
            this.radButtonBrowse.Name = "radButtonBrowse";
            this.radButtonBrowse.Size = new System.Drawing.Size(100, 24);
            this.radButtonBrowse.TabIndex = 4;
            this.radButtonBrowse.Text = "Browse";
            this.radButtonBrowse.ThemeName = "Aqua";
            this.radButtonBrowse.Click += new System.EventHandler(this.radButtonBrowse_Click);
            // 
            // radButtonOk
            // 
            this.radButtonOk.Location = new System.Drawing.Point(148, 99);
            this.radButtonOk.Name = "radButtonOk";
            this.radButtonOk.Size = new System.Drawing.Size(95, 24);
            this.radButtonOk.TabIndex = 5;
            this.radButtonOk.Text = "OK";
            this.radButtonOk.ThemeName = "Aqua";
            this.radButtonOk.Click += new System.EventHandler(this.radButtonOk_Click);
            // 
            // radButtonCancle
            // 
            this.radButtonCancle.Location = new System.Drawing.Point(283, 99);
            this.radButtonCancle.Name = "radButtonCancle";
            this.radButtonCancle.Size = new System.Drawing.Size(93, 24);
            this.radButtonCancle.TabIndex = 6;
            this.radButtonCancle.Text = "Cancel";
            this.radButtonCancle.ThemeName = "Aqua";
            this.radButtonCancle.Click += new System.EventHandler(this.radButtonCancle_Click);
            // 
            // NewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(504, 130);
            this.Controls.Add(this.radButtonCancle);
            this.Controls.Add(this.radButtonOk);
            this.Controls.Add(this.radButtonBrowse);
            this.Controls.Add(this.radTextBoxLocation);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radTextBoxNameProject);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProject";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.NewProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxNameProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox radTextBoxNameProject;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox radTextBoxLocation;
        private Telerik.WinControls.UI.RadButton radButtonBrowse;
        private Telerik.WinControls.UI.RadButton radButtonOk;
        private Telerik.WinControls.UI.RadButton radButtonCancle;
    }
}
