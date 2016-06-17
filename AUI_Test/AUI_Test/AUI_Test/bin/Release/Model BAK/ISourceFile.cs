using System;
using System.Collections.Generic;

namespace AUI_Test
{
    public interface ISourceFile
    {
        /// <summary>
        /// path to the source file
        /// </summary>
      //  string FileName { get; set; }

        /// <summary>
        /// name of the source file
        /// </summary>
        string Name { get; }

        /// <summary>
        /// lines of the source file
        /// </summary>
        List<SourceLine> Lines { get; }

        /// <summary>
        /// the file parser
        /// </summary>
        IFileParser Parser { get; }

        /// <summary>
        /// load data from file
        /// </summary>
       // void Load();

        /// <summary>
        /// save data back to file
        /// </summary>
        //void Save();
    }
}
