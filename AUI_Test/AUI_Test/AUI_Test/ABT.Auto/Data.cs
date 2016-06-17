using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUI_Test.ABT.Model;
namespace AUI_Test.ABT.Auto
{
    public class Data : SourceFile, ABT.Model.IData
    {
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="parser"></param>
        public Data(IFileParser parser)
            : base(parser)
        {
            Parser.WorkingDir = Parser.WorkingDir + Constants.Directory.DataDir;
        }

        /// <summary>
        /// check whether the DataSet has next row
        /// </summary>
        public bool HasNextRow
        {
            get { return CurrentRowId < Lines.Count - 1; }
        }

        /// <summary>
        /// move the DataSet to next row
        /// </summary>
        /// <returns>true if move successfully</returns>
        public bool MoveNext()
        {
            if (!HasNextRow)
                return false;

            CurrentRowId++;
            return true;
        }

        /// <summary>
        /// get data corresponding with a column in current row
        /// </summary>
        /// <param name="variable">the column name</param>
        /// <returns>the value, null if column not exist</returns>
        public string this[string variable]
        {
            get
            {
                if (Lines.Count < 2)
                    return null;

                int idx = Lines[0].Columns.IndexOf(variable);
                if (idx < 0)
                    return null;

                return Lines[CurrentRowId].Columns[idx];
            }
        }

        /// <summary>
        /// current row within the DataSet
        /// </summary>
        public int CurrentRowId { get; set; }
    }
}
