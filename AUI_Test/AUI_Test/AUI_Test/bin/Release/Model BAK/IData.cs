using System;
using System.Collections.Generic;

namespace AUI_Test
{
    public interface IData : ISourceFile
    {
        /// <summary>
        /// check whether the DataSet has next row
        /// </summary>
        bool HasNextRow { get; }

        /// <summary>
        /// move the DataSet to next row
        /// </summary>
        /// <returns>true if move successfully</returns>
        bool MoveNext();

        /// <summary>
        /// get data corresponding with a column in current row
        /// </summary>
        /// <param name="variable">the column name</param>
        /// <returns>the value, null if column not exist</returns>
        string this[string variable] { get; }

        /// <summary>
        /// current row within the DataSet
        /// </summary>
        int CurrentRowId { get; }
    }
}
