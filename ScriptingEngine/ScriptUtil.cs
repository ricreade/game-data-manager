using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;

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
        /// Singleton constructor to initialize the script engine search.
        /// </summary>
        static ScriptUtil()
        {
            CSScript.GlobalSettings.AddSearchDir(@"scripts\");
        }

        /// <summary>
        /// Returns the IScriptInstance instantiated from the specified script.  
        /// This method assumes that the script name and class name are identical.
        /// The script name and class name do not need to have the same case.
        /// </summary>
        /// <param name="scriptName">The name of the script file to invoke.</param>
        /// <returns>The instantiated IScriptInstance.</returns>
        public static IScriptInstance GetScriptObject(string scriptName)
        {
            IScriptInstance inst;
            string className = scriptName.Split(new char[] {'.'})[0];

            inst = CSScript.Load(scriptName)
                .CreateInstance(className, true)
                .AlignToInterface<IScriptInstance>();

            return inst;
        }

        /// <summary>
        /// Returns the IScriptInstance instantiated from the specified
        /// class contained in the specified script.
        /// </summary>
        /// <param name="scriptName">The name of the script file to invoke.</param>
        /// <param name="className">The name of the class to instantiate.</param>
        /// <returns>The instantiated IScriptInstance.</returns>
        public static IScriptInstance GetScriptObject(string scriptName, string className)
        {
            IScriptInstance inst;

            inst = CSScript.Load(scriptName)
                .CreateInstance(className)
                .AlignToInterface<IScriptInstance>();

            return inst;
        }

        /// <summary>
        /// Submits a transaction to the script engine.  The transaction should
        /// contain all the commands (with script references) to execute.
        /// </summary>
        /// <param name="transaction">The transaction to execute.</param>
        /// <returns></returns>
        public static IScriptResult SubmitTransaction(ScriptTransaction transaction)
        {
            return null;        // so it'll compile.
        }
        
    }
}
