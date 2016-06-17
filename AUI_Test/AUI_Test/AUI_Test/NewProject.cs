using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace AUI_Test
{
    public partial class NewProject : Telerik.WinControls.UI.RadForm
    {
        public NewProject()
        {
            InitializeComponent();
        }

        string path;
        string duongdan
        {
            set
            {
                path = value;
            }
        }

        public string duongdanproject;
        public string returnduongdanproject
        {
            set
            {
                duongdanproject = value;
            }
        }


        public string _nameproject;
        public string nameproject
        {
            set
            {
                _nameproject = value;
            }
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void NewProject_Load(object sender, EventArgs e)
        {

        }

        private void radButtonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Chonduongdan = new FolderBrowserDialog();
            Chonduongdan.RootFolder = Environment.SpecialFolder.MyComputer;
            if (Chonduongdan.ShowDialog() == DialogResult.OK)
            {
                duongdan = Chonduongdan.SelectedPath;
                radTextBoxLocation.Text = path;
            }
           
        }

        private void radButtonOk_Click(object sender, EventArgs e)
        {
            DirectoryNew DN = new DirectoryNew();              
            DN.PathProject = path;
            DN.NameProject = radTextBoxNameProject.Text;
            returnduongdanproject = DN.CreateDir();
            nameproject = radTextBoxNameProject.Text;
            this.Close();
        }

        private void radButtonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
