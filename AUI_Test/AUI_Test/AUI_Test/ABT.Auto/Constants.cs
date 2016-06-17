using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test.ABT.Auto
{
    public class Constants
    {
        public const string PropertyDelimeter = @":";
        public const string DefaultName = @"No name";
        public class WarningMessages
        {
            public const string Warning_ReadOnly_TextBox = @"Textbox is read-only";
        }
        public class PropertyNames
        {
            public const string Name = @"name";
            public const string Id = @"id";
            public const string Title = @"title";
            public const string ControlType = @"controltype";
            public const string AutomationId = @"automationid";
            public const string Text = @"text";
            public const string ClassName = @"classname";
            public const string Index = @"index";

            public const string Enabled = @"enabled";
            public const string Visible = @"visible";
            public const string Width = @"width";
            public const string Height = @"height";
        }
        public class Keywords
        {
            public const string KeywordWindow = @"window";
            public const string KeywordControl = @"control";

            public const string KeywordInterface = @"interface";
            public const string KeywordScript = @"script";
            public const string ActionUseInterface = @"use interface";
            public const string ActionStartScript = @"start script";

            public const string VariableNamePrefix = @"$(";
            public const string VariableNameSuffix = @")";
        }

        public class Messages
        {
            public const string Error_Parsing_Script = @"Script file parsing error: invalid action line";
            public const string Error_Parsing_Interface_InvalidWindow = @"Interface file parsing error: invalid window line";
            public const string Error_Parsing_Interface_InvalidControl = @"Interface file parsing error: invalid control line";

            public const string Error_Matching_Window_NoDefinition = @"No definition for window";
            public const string Error_Matching_Window_NotFound = @"Window not found, or not unique";

            public const string Error_Matching_Control_NoDefinition = @"No definition for control";
            public const string Error_Matching_Control_NotFound = @"Control not found, or not unique";

            public const string Error_Executing_NoAction = @"Action not found";
            public const string Error_Executing_InvalidArg = @"Invalid action arguments";

            public const string Error_ExcelFileNotFound = @"File not found or invalid format.";
            public const string Error_ExcelFileNoWorkbook = @"No workbook";
            public const string Error_ExcelFileNoWorksheet = @"No worksheet";
        }

        public class ControlTypeNames
        {
            public const string ControlTypeButton = @"button";
            public const string ControlTypeTextBox = @"textbox";
        }

        public class Directory
        {
            public const string InterfaceDir = @"\Interface\";
            public const string ScriptDir = @"\Script\";
            public const string DataDir = @"\Data\";
            public const string ReportDir = @"\Report\";
        }

        public class ReportText
        {
            public const string ReportNamePrefix = @"Report - ";
            public const string ReportNameSuffix = @" - No data set";
            public const string ReportDateFormat = @"yyyy-MM-dd.HH-mm";

            public const string BeginReport = @"REPORT";
            public const string EndReport = @"END REPORT";
            public const string BeginDataSet = @"DATASET";

            public const string BeginScript = @"begin script";
            public const string EndScript = @"end script";
            public const string BeginDataRow = @"DATA ROW";
            public const string EndDataRow = @"DATA ROW";
            public const string ErrorLinePrefix = @"ERROR: ";

            public const string SummaryReport = @"SUMMARY";
            public const string TotalCheck = @"Checks: ";
            public const string TotalPassed = @"Passed: ";
            public const string TotalWarning = @"Warning: ";
        }
    }
}
