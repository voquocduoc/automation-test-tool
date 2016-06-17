using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AUI_Test.ABT.Model;

namespace AUI_Test.ABT.Auto
{
    public class ExcelReporter : SourceFile, ABT.Model.IReporter
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="parser">the file parser</param>
        public ExcelReporter(IFileParser parser)     : base(parser)
        {
        }

        /// <summary>
        /// total number of check
        /// </summary>
        private int TotalCheck { get; set; }

        /// <summary>
        /// total number of 'passed'
        /// </summary>
        private int TotalPassed { get; set; }

        /// <summary>
        /// total number of warnings
        /// </summary>
        private int TotalWarning { get; set; }

        /// <summary>
        /// current indent of the report
        /// </summary>
        private int Indent { get; set; }

        /// <summary>
        /// create new report
        /// </summary>
        /// <param name="name">name of the report</param>
        /// <param name="datasetName">name of the dataset</param>
        public void BeginReport(string name, string datasetName)
        {
            Name = name;
            Parser.Create(Constants.ReportText.ReportNamePrefix + DateTime.Now.ToString(Constants.ReportText.ReportDateFormat));

            SourceLine line = new SourceLine();
            line.Columns.Add(Constants.ReportText.BeginReport);
            line.Columns.Add(name);

            Lines.Add(line);

            if (datasetName != null)
            {
                line = new SourceLine();
                line.Columns.Add(Constants.ReportText.BeginDataSet);
                line.Columns.Add(datasetName);
                Lines.Add(line);
            }
        }

        /// <summary>
        /// close current report and save to file
        /// </summary>
        public bool EndReport()
        {
            SourceLine line = new SourceLine();
            line.Columns.Add(Constants.ReportText.EndReport);
            line.Columns.Add(Name);

            Lines.Add(line);

            line = new SourceLine();
            line.Columns.Add(Constants.ReportText.SummaryReport);
            line.Columns.Add(Constants.ReportText.TotalCheck + TotalCheck.ToString());
            line.Columns.Add(Constants.ReportText.TotalPassed + TotalPassed.ToString());
            line.Columns.Add(Constants.ReportText.TotalWarning + TotalWarning.ToString());
            Lines.Insert(1, line);

            try
            {
                Parser.Save(Name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// begin new section 'script' in current report
        /// </summary>
        /// <param name="scriptName">name of the script</param>
        public void BeginScript(string scriptName)
        {
            Indent++;

            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(Constants.ReportText.BeginScript);
            line.Columns.Add(scriptName);

            Lines.Add(line);
        }

        /// <summary>
        /// end current 'script' section
        /// </summary>
        /// <param name="scriptName">name of the script</param>
        public void EndScript(string scriptName)
        {
            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(Constants.ReportText.EndScript);
            line.Columns.Add(scriptName);

            Lines.Add(line);

            Indent--;
        }

        /// <summary>
        /// write a line to the report
        /// </summary>
        /// <param name="actLine">information about the action</param>
        /// <param name="result">result of the action</param>
        public void WriteLine(ActionLine actLine, ActionResult result)
        {
            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent + 1; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(actLine.ActionName);
            if (actLine.WindowName != null || actLine.ControlName != null)
            {
                line.Columns.Add(actLine.WindowName != null ? actLine.WindowName : string.Empty);
                line.Columns.Add(actLine.ControlName != null ? actLine.ControlName : string.Empty);
            }
            foreach (string key in actLine.Arguments.Keys)
                line.Columns.Add(key + Constants.PropertyDelimeter + actLine.Arguments[key]);
            line.Columns.Add(result != ActionResult.NORET ? result.ToString() : string.Empty);

            if (result == ActionResult.PASSED || result == ActionResult.FAILED)
            {
                TotalCheck++;
                if (result == ActionResult.PASSED)
                    TotalPassed++;
            }
            else if (result == ActionResult.WARNING)
                TotalWarning++;

            Lines.Add(line);
        }

        /// <summary>
        /// write an error to the report
        /// </summary>
        /// <param name="actLine">the error action line</param>
        /// <param name="why">the reason</param>
        public void WriteError(ActionLine actLine, string why)
        {
            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent + 1; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(actLine.ActionName);
            if (actLine.WindowName != null || actLine.ControlName != null)
            {
                line.Columns.Add(actLine.WindowName != null ? actLine.WindowName : string.Empty);
                line.Columns.Add(actLine.ControlName != null ? actLine.ControlName : string.Empty);
            }
            foreach (string key in actLine.Arguments.Keys)
                line.Columns.Add(key + Constants.PropertyDelimeter + actLine.Arguments[key]);
            line.Columns.Add(Constants.ReportText.ErrorLinePrefix + why);

            Lines.Add(line);
        }

        /// <summary>
        /// new instance copied
        /// </summary>
        public IReporter NewInstance
        {
            get
            {
                return new ExcelReporter(Parser.NewInstance);
            }
        }

        /// <summary>
        /// begin new section 'data row' in current report
        /// </summary>
        /// <param name="id">row id</param>
        public void BeginDataRow(int id)
        {
            Indent++;

            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(Constants.ReportText.BeginDataRow);
            line.Columns.Add(id.ToString());

            Lines.Add(line);
        }

        /// <summary>
        /// end current 'data row' section
        /// </summary>
        /// <param name="id">row id</param>
        public void EndDataRow(int id)
        {
            SourceLine line = new SourceLine();
            for (int i = 0; i < Indent; i++)
                line.Columns.Add(string.Empty);

            line.Columns.Add(Constants.ReportText.EndDataRow);
            line.Columns.Add(id.ToString());

            Lines.Add(line);

            Indent--;
        }

        /// <summary>
        /// default extension of the report file
        /// </summary>
        public string FileExtension
        {
            get { return Parser.FileExtension; }
        }

        /// <summary>
        /// the working directory
        /// </summary>
        public string WorkingDir
        {
            get { return Parser.WorkingDir; }
            set { Parser.WorkingDir = value + Constants.Directory.ReportDir; }
        }
    }
}
