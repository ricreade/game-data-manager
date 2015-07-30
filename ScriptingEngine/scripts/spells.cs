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
        
        for (int i = 0; i < requestArgs.Length; i++)
        {
            string[] arg = requestArgs[i].Split('=');
            switch (arg[0])
            {
                case "agent":
                    break;
                case "source":
                    break;
                case "targets":
                    break;
                default:
                    break;
            }
        }
        // get the originator of the action


        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, null);
    }
}