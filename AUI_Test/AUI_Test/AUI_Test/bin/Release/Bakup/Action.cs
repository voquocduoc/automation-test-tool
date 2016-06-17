using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUI_Test
{
    public enum ActionResult
    {
        NORET,
        PASSED,
        FAILED,
        ERROR,
        WARNING
    }

    public abstract class Action 
    {
        Dictionary<string, string> m_Params;

        /// <summary>
        /// name of the action
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// result of the action
        /// </summary>
        public ActionResult Result { get; protected set; }

        /// <summary>
        /// more information about the result
        /// </summary>
        public string MoreDetailAboutResult { get; protected set; }

        /// <summary>
        /// parameters of the action
        /// </summary>
        public virtual Dictionary<string, string> Params
        {
            get { return m_Params; }
            set
            {
                foreach (string key in value.Keys)
                    Params.Add(key, value[key]);
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public Action()
        {
            m_Params = new Dictionary<string, string>();
            Result = ActionResult.NORET;
        }

        /// <summary>
        /// check if the parameters are valid for executing
        /// derived class should re-implement this function
        /// </summary>
        /// <returns>true - if parameters are ready for executing</returns>
        public virtual bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// execute the action
        /// derived class should re-implement this function for actual executing
        /// </summary>
        /// <returns>0 - if executing sucessfully</returns>
        public virtual int Execute()
        {
            return 0;
        }

        /// <summary>
        /// reset action after executing
        /// </summary>
        public virtual void Reset()
        {
            Params.Clear();
            Result = ActionResult.NORET;
        }
    }
}
