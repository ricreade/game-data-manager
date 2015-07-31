using ScriptingEngine;

/// <summary>
/// Handles non-arbitrary spell effects.  The request is parsed to determine
/// the source, spell, and targets and then executes the spell with consideration
/// given to spell resistance, saving throws, target validity, etc.
/// </summary>
public class Spells : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string[] requestArgs = request.Instruction.Split('|');
        IScriptResult saveResult = null;
        
        for (int i = 0; i < requestArgs.Length; i++)
        {
            string[] arg = requestArgs[i].Split('=');
            switch (arg[0])
            {
                case "agent":
                    // Get the spell being cast
                    break;
                case "source":
                    // Get the caster
                    break;
                case "targets":
                    // Get the list of targets
                    break;
                case "options":
                    // Get the casting options applied to this spell
                default:
                    break;
            }
        }

        // If this spell allows a save, perform the appropriate check.
        if (true)
        {
            saveResult = ScriptUtil.ExecuteRequest(ScriptUtil.CreateRequest("", "checks"));
        }


        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, null);
    }
}