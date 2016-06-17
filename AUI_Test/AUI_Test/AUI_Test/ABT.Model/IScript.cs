using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test.ABT.Model
{
    public interface IScript : ISourceFile
    {
        /// <summary>
        /// check if there is any action lines are waiting for executing
        /// </summary>
        bool HasNextLine { get; }

        /// <summary>
        /// current executing line number
        /// </summary>
        int CurrentLineNumber { get; }

        /// <summary>
        /// go to next action line
        /// </summary>
        /// <returns>null - if no action line left</returns>
        ActionLine Next();

        /// <summary>
        /// restart the script
        /// </summary>
        void Restart();
    }
}
