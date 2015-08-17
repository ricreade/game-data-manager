using ScriptingEngine;

/// <summary>
/// This script handles die rolls.  It can be invoked in one of a number
/// of ways since a die rolls are required in a number of contexts.  The
/// roll syntax is '{[numdice]d[diesize]} [dmgtype]', repeatable as
/// necessary.
/// </summary>
public class DieRoll : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        throw new System.NotImplementedException();
    }
}