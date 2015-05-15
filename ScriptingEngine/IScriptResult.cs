using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// I'm probably going to eliminate this.
    /// 
    /// An interface to represent the result of a script transaction.
    /// I figure a simple boolean or string is unlikely to provide all
    /// the required feedback.
    /// </summary>
    public interface IScriptResult
    {
        /// <summary>
        /// Returns the result of the script request after processing.
        /// </summary>
        string Result { get; }
    }
}
