using ScriptingEngine;
using System.Collections.Generic;
public class TestScript : IScriptInstanceTest
{
    private List<string> _list;
    public string GetTestValue()
    {
        return "Returned value from TestScript!";
    }

    public void StoreTestObject(List<string> list)
    {
        _list = list;
        _list.Add("hello");
    }

    public IList<string> RetrieveTestObject()
    {
        return _list;
    }

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        return null;
    }
}