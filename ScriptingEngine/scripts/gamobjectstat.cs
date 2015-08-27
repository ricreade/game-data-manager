using ScriptingEngine;

/// <summary>
/// Script responsible for calculating the total stat value for a game object.
/// This may include characters, NPCs, items, spells, and similar information.
/// This script reviews all embedded object attributes and all referenced
/// effects to find the total.  Options may be applied to force the script
/// to consider circumstances or effect descriptors.
/// The arguments for this script are the id of the object to check and
/// the name of the stat to calculate.  The syntax for the instruction is
/// 
/// objectid=[objectid][sep]statname=[name][sep]options=[optionlist]
/// 
/// where the [objectid] is the registered id of the object to investigate,
/// the [name] is the name of the stat to check, and [optionlist] is a
/// comma-delimited list of options to consider for the check.  When investigating
/// the object, the script will expect exact matches of the stat name to avoid
/// conflicts with (for example) nearly identical skill names.  Options can
/// be inclusions and/or exclusions where the arguments are space-delimited
/// and in quotes.  For example, to specifically include save bonuses that apply 
/// to mind-affecting effects or to exclude holy, the option syntax would be
/// 
/// options=include "mind-affecting"
/// options=exclude "holy"
/// options=include "mind-affecting" exclude "holy"
/// 
/// Multiple inclusions and exclusions can be specified
/// 
/// options=include "mind-affecting" "chaotic" exclude "holy" "good"
/// 
/// Any bonuses that do not specify such a property are automatically
/// included.  The final list of bonuses is then checked by another script to
/// verify that bonus stacking is considered.
/// </summary>
public class GameObjectStat : IScriptInstance
{

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        throw new System.NotImplementedException();
    }
}