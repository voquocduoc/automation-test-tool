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
    public partial class NewFile : Telerik.WinControls.UI.RadForm
    {
        public NewFile()
        {
            InitializeComponent();
        }
        public string name;
        public string NameFile
        {
            set
            {
                name = value;
            }
        }
        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void _ReadFile_Load(object sender, EventArgs e)
        {

        }

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            NameFile = Filename.Text;
            this.Close();
        }
    }
}
