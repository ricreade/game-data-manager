using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// Interface to encapsulate a transaction to submit to the script engine.
    /// This might not be the way we ultimately represent a script request,
    /// but it'll serve as a stand-in for the time being.  This object should
    /// contain all the information required for the script engine to function,
    /// including the name of all scripts to be invoked in order and the
    /// required data references.  The transaction object should indicate
    /// the manner in which the transaction should be processed.  For example,
    /// if the entire transaction should be rolled back if any instruction fails.
    /// </summary>
    interface IScriptTransaction
    {

    }
}
