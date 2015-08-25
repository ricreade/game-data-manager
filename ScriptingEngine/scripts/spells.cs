using ScriptingEngine;
using PrototypeDataImpl;

/// <summary>
/// Handles non-arbitrary spell effects.  The request is parsed to determine
/// the source, spell, and targets and then executes the spell with consideration
/// given to spell resistance, saving throws, target validity, etc.
/// The input request must satisfy the syntax:
/// 
/// agent=[agentid][sep]source=[casterid][sep]targets=[targetids][sep]options=[optionlist]
/// 
/// [agentid]       The id value of the spell being cast.
/// [casterid]      The id value of the spell caster.  If this is empty, the caster level
///                 and save DC must be specified in the option list.
/// [targetids]     A comma-delimited list of target ids that will be subject to this spell.
///                 each will be evaluated independently.
/// [optionlist]    A comma-delimited list of options that will be applied to this casting.
///                 This may include caster level and save DC if no caster id is specified,
///                 override values for object properties, and ad hoc modifiers.
/// </summary>
public class Spells : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string agentid = "", sourceid = "", options = "", instruction, check;
        string[] targetids = null;
        PrototypeDataObject agent = null;
        PrototypeDataLayer layer = PrototypeGameStates.Instance.GetLayer(request.DataLayerId);

        if (layer == null)
            return ScriptUtil.CreateResult(
                ScriptResult.ResultType.Fail, 
                string.Format("No layer found with id '{0}'.", request.DataLayerId)
                );

        string[] requestArgs = ScriptUtil.SplitScriptString(request.Instruction);
        IScriptResult saveResult = null;
        
        for (int i = 0; i < requestArgs.Length; i++)
        {
            string[] arg = requestArgs[i].Split('=');

            if (arg.Length != 2)
                return ScriptUtil.CreateResult(
                    ScriptResult.ResultType.Fail, 
                    string.Format("Malformed request: '{0}'", request.Instruction)
                    );

            switch (arg[0])
            {
                case "agent":
                    agentid = arg[1];
                    agent = layer.GetDataObject(agentid);
                    break;
                case "source":
                    sourceid = arg[1];
                    break;
                case "targets":
                    targetids = ScriptUtil.SplitScriptString(arg[1], ',');
                    break;
                case "options":
                    options = arg[1];
                    break;
                default:
                    return ScriptUtil.CreateResult(
                        ScriptResult.ResultType.Fail,
                        string.Format("Unknown argument '{0}' in request: '{1}'", arg[0], request.Instruction)
                        );
            }
        }

        if (agent == null)
            return ScriptUtil.CreateResult(
                ScriptResult.ResultType.Fail,
                string.Format("Agent not found.")
                );

        foreach (string targetid in targetids)
        {
            // If this spell allows spell resistence, perform the appropriate check.

            // If this spell allows a save, perform the appropriate check.
            check = agent.getValue("save");
            if (check != null && !check.Equals("none"))
            {
                check = check.Trim().Split(' ')[0];
                instruction = string.Format("check={1}{0}source={2}{0}agent={3}{0}target={4}{0}options={5}",
                    ScriptUtil.Separator, check, sourceid, agentid, targetid, options);
                saveResult = ScriptUtil.ExecuteRequest(ScriptUtil.CreateRequest(instruction, "checks", "test"));
            }


        }
        

        return saveResult;
    }
}