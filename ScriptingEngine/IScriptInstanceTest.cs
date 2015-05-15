using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// A dummy interface for testing purposes.
    /// </summary>
    public interface IScriptInstanceTest : IScriptInstance
    {
        string GetTestValue();
        void StoreTestObject(List<string> list);
        IList<string> RetrieveTestObject();
    }
}
