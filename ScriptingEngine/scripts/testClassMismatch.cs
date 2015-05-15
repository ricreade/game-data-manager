using ScriptingEngine;
using System.Collections.Generic;

public class MismatchedClassName : IScriptInstanceTest
{
    public string GetTestValue()
    {
        return "some value";
    }

    public void StoreTestObject(List<string> list)
    {
        //nothing happens
    }

    public IList<string> RetrieveTestObject()
    {
        return null;
    }

    public ScriptResult ProcessTransactionRequest(IScriptRequest request)
    {
        return null;
    }
}