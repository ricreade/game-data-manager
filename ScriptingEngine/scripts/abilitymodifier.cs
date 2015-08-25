using System;
using ScriptingEngine;

/// <summary>
/// Accepts an ability score and returns the modifier for the score.
/// </summary>
public class AbilityModifier : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string score = request.Instruction;
        int val;

        if (score != null && Int32.TryParse(score.Trim(), out val))
        {
            val = (val - (val % 2 == 0 ? 10 : 11)) / 2;
            return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, val.ToString());
        }

        return ScriptUtil.CreateResult(
            ScriptResult.ResultType.Fail, 
            string.Format("Invalid ability value: {0}", request.Instruction)
            );
    }
}