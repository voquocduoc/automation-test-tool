using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using AUI_Test;
using AUI_Test.ABT.Model;
using AUI_Test.ABT.Auto;
using System.IO;
using Telerik.WinControls.UI;
using System.Windows.Input;
using Hotkeys;


namespace AUI_Test
{
    public partial class RunScript : Telerik.WinControls.UI.RadForm
    {

        public string message;

        private Hotkeys.GlobalHotkey ghk;
        private Hotkeys.GlobalHotkey ghk2;

        private KeyData ghkkey;
        private KeyData ghkkey2;
        List<KeysData> KeysList = new List<KeysData>();


        private void InitKeyList()
        {

            KeysList.Add(new KeysData("Back", 8));
            KeysList.Add(new KeysData("Tab", 9));
            KeysList.Add(new KeysData("Enter", 13));
            KeysList.Add(new KeysData("ESC", 27));
            KeysList.Add(new KeysData("Space", 32));
            KeysList.Add(new KeysData("PAGE UP", 33));
            KeysList.Add(new KeysData("Page Down", 34));
            KeysList.Add(new KeysData("END", 35));
            KeysList.Add(new KeysData("Home", 36));
            KeysList.Add(new KeysData("Left", 37));
            KeysList.Add(new KeysData("Up", 38));
            KeysList.Add(new KeysData("Right", 39));
            KeysList.Add(new KeysData("Down", 40));
            KeysList.Add(new KeysData("PrintScreen", 44));
            KeysList.Add(new KeysData("Insert", 45));
            KeysList.Add(new KeysData("D 0", 48));
            KeysList.Add(new KeysData("D 1", 49));
            KeysList.Add(new KeysData("D 2", 50));
            KeysList.Add(new KeysData("D 3", 51));
            KeysList.Add(new KeysData("D 4", 52));
            KeysList.Add(new KeysData("D 5", 53));
            KeysList.Add(new KeysData("D 6", 54));
            KeysList.Add(new KeysData("D 7", 55));
            KeysList.Add(new KeysData("D 8", 56));
            KeysList.Add(new KeysData("D 9", 57));
            KeysList.Add(new KeysData("A", 65));
            KeysList.Add(new KeysData("B", 66));
            KeysList.Add(new KeysData("C", 67));
            KeysList.Add(new KeysData("D", 68));
            KeysList.Add(new KeysData("E", 69));
            KeysList.Add(new KeysData("F", 70));
            KeysList.Add(new KeysData("G", 71));
            KeysList.Add(new KeysData("H", 72));
            KeysList.Add(new KeysData("I", 73));
            KeysList.Add(new KeysData("J", 74));
            KeysList.Add(new KeysData("K", 75));
            KeysList.Add(new KeysData("L", 76));
            KeysList.Add(new KeysData("M", 77));
            KeysList.Add(new KeysData("N", 78));
            KeysList.Add(new KeysData("O", 79));
            KeysList.Add(new KeysData("P", 80));
            KeysList.Add(new KeysData("Q", 81));
            KeysList.Add(new KeysData("R", 82));
            KeysList.Add(new KeysData("S", 83));
            KeysList.Add(new KeysData("T", 84));
            KeysList.Add(new KeysData("U", 85));
            KeysList.Add(new KeysData("V", 86));
            KeysList.Add(new KeysData("W", 87));
            KeysList.Add(new KeysData("X", 88));
            KeysList.Add(new KeysData("Y", 89));
            KeysList.Add(new KeysData("Z", 90));


            KeysList.Add(new KeysData("NumPad 0", 96));
            KeysList.Add(new KeysData("NumPad 1", 97));
            KeysList.Add(new KeysData("NumPad 2", 98));
            KeysList.Add(new KeysData("NumPad 3", 99));
            KeysList.Add(new KeysData("NumPad 4", 100));
            KeysList.Add(new KeysData("NumPad 5", 101));
            KeysList.Add(new KeysData("NumPad 6", 102));
            KeysList.Add(new KeysData("NumPad 7", 103));
            KeysList.Add(new KeysData("NumPad 8", 104));
            KeysList.Add(new KeysData("NumPad 9", 105));

            KeysList.Add(new KeysData("*", 106));
            KeysList.Add(new KeysData("+", 107));
            KeysList.Add(new KeysData("-", 109));
            KeysList.Add(new KeysData("Decimal", 110));
            KeysList.Add(new KeysData("/", 111));
            KeysList.Add(new KeysData("F1", 112));
            KeysList.Add(new KeysData("F2", 113));
            KeysList.Add(new KeysData("F3", 114));
            KeysList.Add(new KeysData("F4", 115));
            KeysList.Add(new KeysData("F5", 116));
            KeysList.Add(new KeysData("F6", 117));
            KeysList.Add(new KeysData("F7", 118));
            KeysList.Add(new KeysData("F8", 119));
            KeysList.Add(new KeysData("F9", 120));
            KeysList.Add(new KeysData("F10", 121));
            KeysList.Add(new KeysData("F11", 122));
            KeysList.Add(new KeysData("F12", 123));
        }


        private Keys GetKey(IntPtr LParam)
        {
            int hassan = LParam.ToInt32();
            hassan = LParam.ToInt32() ^ 1;
            return (Keys)((LParam.ToInt32()) >> 16); // not all of the parenthesis are needed, I just found it easier to see what's happening
        }

        internal class KeyData
        {
            internal bool Alt;
            internal bool Ctrl;
            internal bool Shift;
            internal bool Win;
            internal Keys Key;
            public KeyData(bool alt, bool ctrl, bool shift, bool win, Keys key)
            {
                Key = key;
                Win = win;
                Shift = shift;
                Ctrl = ctrl;
                Alt = alt;
            }
        }
        internal class KeysData
        {
            internal string Text;
            internal int Code;
            public KeysData(string text, int code)
            {
                Text = text;
                Code = code;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                //Keys Keyrecieve;// = Keys.D0;
                bool AltKeys = CheckSetModiferKey(m.LParam.ToInt32(), 1);
                bool CtrlKeys = CheckSetModiferKey(m.LParam.ToInt32(), 2);
                bool ShiftKeys = CheckSetModiferKey(m.LParam.ToInt32(), 4);
                bool WinKeys = CheckSetModiferKey(m.LParam.ToInt32(), 8);
                Keys Keyrecieve = GetKey(m.LParam);

                if (CtrlKeys && Keyrecieve == Keys.P)
                {
                    this.Stop();
                }
                else if (CtrlKeys && Keyrecieve == Keys.R)
                {
                    this.resume();
                }
            }
            //HandleHotkey();
            base.WndProc(ref m);
        }
        
        private bool CheckSetModiferKey(int LParam, int ModifierCode)
        {
            bool res = false;
            int CheckModi = LParam ^ ModifierCode;
            if (LParam > CheckModi)
            {
                res = true;
            }
            return res;
        }


        Status _status = new Status();      
       
        public RunScript()
        {
            _status.Show();
            _status.DesktopLocation = new Point(860,550);
            _status.Width = 500;
            _status.Height = 60;

            InitializeComponent();
            ispause = false;

            ghk = new Hotkeys.GlobalHotkey(Hotkeys.Constants.CTRL, Keys.P, this);
            ghk2 = new Hotkeys.GlobalHotkey(Hotkeys.Constants.CTRL, Keys.R, this);

            ghkkey = new KeyData(true, false, true, false, Keys.P);
            ghkkey = new KeyData(true, false, true, false, Keys.R);
            InitKeyList();
            
            
            
        }

        bool valuep;
        public bool ispause
        {
            get
            {
                return false;
            }
            set
            {
                valuep = value;
            }
        }

        public int ischeck()
        {
            return 1;
        }



        //-------- Duong dan cua project 

        public string pathproject;
        public string _pathproject
        {
            get
            {
                return pathproject = "";
            }
            set
            {
                pathproject = value;
            }
        }

        // ----- File data

        public string data = "";
        public string _data
        {
            set
            {
                data = value;
            }
        }

        //---- File Script
        public string script;
        public string _script
        {
            set
            {
                script = value;
            }
        }

        

        // --- Run Script
       
        public void Runscript(string path)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path + @"\Script", "*.xls", SearchOption.AllDirectories);

                int c = filePaths.Length;

                for (int i = 0; i < c; i++)
                {
                    string s = filePaths[i];

                    int locate = s.LastIndexOf("\\");
                    s = s.Substring(locate + 1);

                    radSplitButtonScript.Items.Add(new RadMenuItem(s));
                    RadMenuItem items = radSplitButtonScript.Items[i] as RadMenuItem;
                    items.Click += items_Click;
                }
            }
            catch
            {
            }
        }

        void items_Click(object sender, EventArgs e)
        {
            RadMenuItem items = (sender as RadMenuItem);
            this.radSplitButtonScript.Text = items.Text;
            this._script = items.Text;
        }

        //--------------------------------- Run

        public void Rundata(string path)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path + @"\Data", "*.xls", SearchOption.AllDirectories);

                int c = filePaths.Length;

                for (int i = 0; i < c; i++)
                {
                    string s = filePaths[i];

                    int locate = s.LastIndexOf("\\");
                    s = s.Substring(locate + 1);

                    radSplitButtondata.Items.Add(new RadMenuItem(s));

                    RadMenuItem item = radSplitButtondata.Items[i] as RadMenuItem;
                    item.Click += item_Click;

                }
            }
            catch
            {
                MessageBox.Show("You Is Not Choose Open Project Or New Project ");
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            RadMenuItem item = (sender as RadMenuItem);
            this.radSplitButtondata.Text = item.Text;
            this._data = item.Text;
        }


        IAutomation currentAutomation = null;

        private void radButtonOK_Click(object sender, EventArgs e)
        {
            IAutomation at = new Automation(new ExcelFileParser(), new ExcelReporter(new ExcelFileParser()), this.pathproject);
            UIAActionManager am = new UIAActionManager(at);
            currentAutomation = at;

            try
            {
                Script startScript = new Script(at.Parser.NewInstance);
                startScript.FileName = this.script;



                at.Name = "Regression";
                at.Speed = 10;
                if (this.data != null && this.data.Length > 0)
                {
                    Data data = new Data(at.Parser.NewInstance);
                    data.FileName = this.data;

                    at.Data = data;
                }
                
                
                at.StartScript = startScript;
                at.Start();   
             
                //----------------------------------

                at.Paused += at_Paused;
                at.Resumed += at_Resumed;
                at.Interupted += at_Interupted;
                at.Ended += at_Ended;
                at.ActionPerforming += at_ActionPerforming;
                                
               
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

           
        }

        public void Stop()
        {
            currentAutomation.Pause();
          
        }

        public void resume()
        {
            currentAutomation.Resume();
           
        }

        private void radButtoncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RunScript_Load(object sender, EventArgs e)
        {
            ghk.Register();
            ghk2.Register();
        }
                 
        //--------------------------------------

       
        void at_ActionPerforming(string s)
        {
            _status.show(s);
        }

        void at_Ended(IAutomation at)
        {

            _status.show("End Automation");
        }

        void at_Interupted(IAutomation at)
        {

            _status.show(at.ErrorMessage.ToString());
        }

        void at_Resumed(IAutomation at)
        {

            _status.show("Resume");
        }

        void at_Paused(IAutomation at)
        {

            _status.show("Pause");
        }
                
    }
}
