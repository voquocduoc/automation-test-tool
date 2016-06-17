namespace AUI_Test
{
    partial class Spy
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
            this.components = new System.ComponentModel.Container();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Menu = new Telerik.WinControls.UI.RadMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.Matching = new AUI_Test.MatchesViewer();
            this.Property = new AUI_Test.PropertiesViewer();
            this.View = new AUI_Test.WindowsViewer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.AccessibleDescription = "Refresh";
            this.Menu.AccessibleName = "Refresh";
            this.Menu.Name = "Menu";
            this.Menu.Text = "Refresh";
            this.Menu.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.Menu.Click += new System.EventHandler(this.Menu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.View);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 406);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Windown View";
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.Menu});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(979, 21);
            this.radMenu1.TabIndex = 0;
            this.radMenu1.Text = "radMenu1";
            this.radMenu1.ThemeName = "Aqua";
            // 
            // Matching
            // 
            this.Matching.Location = new System.Drawing.Point(718, 27);
            this.Matching.Name = "Matching";
            this.Matching.Padding = new System.Windows.Forms.Padding(5);
            this.Matching.Size = new System.Drawing.Size(249, 410);
            this.Matching.TabIndex = 3;
            // 
            // Property
            // 
            this.Property.ActionManager = null;
            this.Property.CurrentInterface = null;
            this.Property.CurrentName = null;
            this.Property.Location = new System.Drawing.Point(328, 22);
            this.Property.Name = "Property";
            this.Property.Object = null;
            this.Property.Padding = new System.Windows.Forms.Padding(5);
            this.Property.Size = new System.Drawing.Size(384, 411);
            this.Property.TabIndex = 2;
            // 
            // View
            // 
            this.View.ActionManager = null;
            this.View.CurrentInterface = null;
            this.View.Location = new System.Drawing.Point(6, 19);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(294, 381);
            this.View.TabIndex = 0;
            this.View.Load += new System.EventHandler(this.View_Load);
            // 
            // Spy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(979, 444);
            this.Controls.Add(this.Matching);
            this.Controls.Add(this.Property);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radMenu1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Spy";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "";
            this.ThemeName = "Aqua";
            this.Load += new System.EventHandler(this.Spy_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Telerik.WinControls.UI.RadMenuItem Menu;
        private System.Windows.Forms.GroupBox groupBox1;
        private WindowsViewer View;
        private PropertiesViewer Property;
        private MatchesViewer Matching;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private Telerik.WinControls.UI.RadMenu radMenu1;


    }
}
