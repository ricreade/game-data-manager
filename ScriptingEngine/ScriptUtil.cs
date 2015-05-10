using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// Singleton class to provide a common interface to the scripting engine.
    /// All script transactions are processed through this class.  I am going
    /// to assume that this class will not be responsible for knowing which
    /// scripts are available (the CS Script engine takes care of loading),
    /// so I'm not going to try to provide an enumeration.  Instead, the
    /// caller will need to provide the name of the script to invoke.
    /// This class should retain no state whatsoever.  All submitted data is
    /// encapsulated in the transaction and all results as encapsulated in the
    /// result.  All operations should be completely agnostic.
    /// </summary>
    public static class ScriptUtil
    {
        /// <summary>
        /// Submits a transaction to the script engine.  The transaction should
        /// contain all the commands (with script references) to execute.
        /// </summary>
        /// <param name="transaction">The transaction to execute.</param>
        /// <returns></returns>
        public static IScriptResult submitTransaction(ScriptTransaction transaction)
        {
            return null;        // so it'll compile.
        }
        
    }
}
