using System;
using ScriptingEngine;
using PrototypeDataImpl;

/// <summary>
/// Hands checks between a source and a target.  The source is always the
/// initiator of the action and the target is always the defender.  The
/// context of the source and target may vary from check to check.  For
/// instance, when a PC makes an attack roll against an NPC, the PC is the
/// source (applying a certain attack roll) and the NPC is the target
/// (applying AC).  When making a save against a spell, the source is the
/// spell and the target is the target of the spell.  When making a check
/// against an environmental DC (such as defeating a trap or swimming against
/// current) the source is the one making the check and the target is the
/// given DC.
/// For example, to make a check on a reflex save for a spell, the syntax
/// is: 
/// check=ref[sep]source=[casterid][sep]agent=[spellid][sep]target=[targetid]
///     [sep]options=[optionslist]
/// where [sep] is the system separater, [casterid] is the object id of the
/// caster, [spellid] is the object id of the spell [targetid] is the object 
/// id of the target, and [optionslist] is a comma-delimited list of options 
/// for the check.
/// </summary>
public class Checks : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string[] requestArgs = ScriptUtil.SplitScriptString(request.Instruction);
        string[] optsList = null, option = null;
        string check = "", sourceid = "", agentid = "", targetid = "", opts = "";
        int dc = -1;

        PrototypeDataObject source = null, agent = null, target = null;
        PrototypeDataLayer layer = PrototypeGameStates.Instance.GetLayer(request.DataLayerId);

        for (int i = 0; i < requestArgs.Length; i++)
        {
            string[] arg = requestArgs[i].Split('=');
            switch (arg[0])
            {
                case "check":
                    // Get the kind of check to perform
                    check = arg[1];
                    break;

                case "source":
                    // Get the attacker
                    sourceid = arg[1];
                    source = layer.GetDataObject(sourceid);
                    break;

                case "agent":
                    // Get the spell
                    agentid = arg[1];
                    agent = layer.GetDataObject(agentid);
                    break;

                case "target":
                    // Get the defender
                    targetid = arg[1];
                    target = layer.GetDataObject(targetid);
                    break;

                case "options":
                    // Get the options applied to this check.
                    opts = arg[1];
                    optsList = ScriptUtil.SplitScriptString(opts, ',');
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

        if (target == null)
            return ScriptUtil.CreateResult(
                ScriptResult.ResultType.Fail,
                string.Format("Target not found.")
                );

        if (source == null)
        {
            if (optsList != null)
            {
                for (int i = 0; i < optsList.Length; i++)
                {
                    option = ScriptUtil.SplitScriptString(optsList[i], ' ');
                    if (option != null && option.Length == 2 && option[0] == "dc")
                    {
                        int val;
                        if (Int32.TryParse(option[1], out val))
                        {
                            dc = val;
                        }
                    }
                }
            }
            if (dc == -1)
            {
                return ScriptUtil.CreateResult(
                    ScriptResult.ResultType.Fail,
                    string.Format("Cannot determine save DC.")
                    );
            }
        }

        switch (check)
        {
            case "fort":
            case "ref":
            case "will":
                return Save(null, null, opts);

            case "skill":
                return Skill(null, null, opts);

            default:
                break;
        }

        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, null);

        
    }

    /// <summary>
    /// This is a save against some sort of effect.  The source indicates the type of
    /// save to make (unless the options indicate an override) as well as the DC
    /// (unless the options indicate an override).  In some cases, the source may be
    /// null and the options entirely define the terms of the save.  If no source
    /// is defined in the source or options arguments, this procedure will report
    /// an error.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    private IScriptResult Save(object source, object target, string options)
    {



        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, null);
    }

    /// <summary>
    /// This is a skill check against some sort of effect or defender.  The source
    /// indicates the PC or NPC making the check.  The target indicates the defender
    /// of the check and the options indicate the skill to use.  In some cases, there
    /// might not be a target, in which case the options will determine the DC of
    /// the check.  If the target should defend with an opposing skill (such as
    /// Hide to defeat Spot), the skill being used will indicate as such and the
    /// target must be defined.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    private IScriptResult Skill(object source, object target, string options)
    {


        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, null);
    }
}