using ScriptingEngine;

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
/// </summary>
public class Checks : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        string[] requestArgs = request.Instruction.Split('|');
        string check = null;
        string opts = null;

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
                    break;

                case "target":
                    // Get the defender
                    break;

                case "options":
                    // Get the options applied to this check.
                    opts = arg[1];
                    break;

                default:
                    break;
            }
        }

        switch (check)
        {
            case "save":
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