using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test.ABT.Model
{
    public sealed class SourceLine
    {
        /// <summary>
        /// constructor
        /// </summary>
        public SourceLine()
        {
            Columns = new List<string>();
        }

        /// <summary>
        /// number of column in the line
        /// </summary>
        public int ColumnCount
        {
            get { return Columns.Count; }
        }

        /// <summary>
        /// value of a column indicated by "index"
        /// </summary>
        /// <param name="index">the column zero-based index</param>
        /// <returns>the content of the column</returns>
        public List<string> Columns { get; set; }
    }
}
