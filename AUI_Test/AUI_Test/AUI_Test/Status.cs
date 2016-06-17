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
    public partial class Status : Telerik.WinControls.UI.RadForm
    {
        public Status()
        {
            InitializeComponent();
            
            
        }

        public void show(string s)
        {
         
            if(label.InvokeRequired)    
            label.Invoke(new MethodInvoker(delegate { label.Text = s; }));    
            
        }

        private void Status_Load(object sender, EventArgs e)
        {
             
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
