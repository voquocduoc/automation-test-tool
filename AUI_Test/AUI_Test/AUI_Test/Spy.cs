using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;

namespace AUI_Test
{

    public partial class Spy : Telerik.WinControls.UI.RadForm
    {
        Dictionary<string, TestStack.White.UIItems.IUIItem> matchedControls;

        public Spy()
        {
            InitializeComponent();

            IAutomation automation = new Automation(new ExcelFileParser(), new ExcelReporter(new ExcelFileParser()), @"C:\Users\10110_000\Desktop\Duoc");
            UIAActionManager actionManager = new UIAActionManager(automation);
            actionManager.WaitTime = new TimeSpan(0, 0, 1);

            IInterface iff = new Interface(automation.Parser.NewInstance);
            iff.FileName = "Calculator.xls";

            View.ActionManager = actionManager;
            View.CurrentInterface = iff;
            View.WindowsRefreshed += View_WindowsRefreshed;
            Property.CurrentInterface = iff;
            View.SelectedItemChanged += View_SelectedItemChanged;
            
        }

        void View_SelectedItemChanged(TestStack.White.UIItems.IUIItem item, string name)
        {
            Property.CurrentName = name;
            Property.Object = item;
        }

        void View_WindowsRefreshed(Dictionary<string, TestStack.White.UIItems.IUIItem> matchedControls)
        {
            this.matchedControls = matchedControls;
            Matching.MatchedControls = matchedControls;
        }

        private void View_Load(object sender, EventArgs e)
        {
            View.RefreshWindows();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            Matching.Reset();
            Property.Reset();
            View.RefreshWindows();
        }

        private void Spy_Load(object sender, EventArgs e)
        {

        }

        
       

        

        

    }
}
