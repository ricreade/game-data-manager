using ScriptingEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// This script handles die rolls.  It can be invoked in one of a number
/// of ways since a die rolls are required in a number of contexts.  The
/// roll syntax is '{[numdice]d[diesize] [+/- num]} [type]', where this syntax
/// is repeatable as necessary to resolve multiple rolls (either for 
/// different types or different dice).  Multiple rolls are divided by
/// commas.  Inputs are evaluated against a regular expression, which is
/// compiled once and cached.
/// This method returns a string in the IScriptResult message with the
/// total roll values by type.
/// 
/// Ex Input:   {1d6}
/// Ex Return:  4
/// Val Range:  1-6
/// 
/// Ex Input:   {2d6+3} fire, {1d4} piercing
/// Ex Return:  11 (fire), 2 (piercing)
/// Val Range:  5-15 fire, 1-4 piercing
/// 
/// Ex Input:   {2d6+3} slashing, {2d4 +1} slashing, {1d12} fire
/// Ex Return:  18 slashing, 7 fire
/// Val Range:  8-24 slashing (5-15 + 3-9), 1-12 fire
/// </summary>
public class DieRoll : IScriptInstance
{
    private Random _randGen;
    private Regex _regex;

    private const string REG_EXP = 
        @"^\s*\{(?<numdice>\d+)[Dd](?<diesize>\d+)\s*((?<sign>[\+-])\s*(?<modifier>\d+))??\s*\}\s*(?<type>\S+(\s+\S+)*)??\s*$";

    public DieRoll()
    {
        _randGen = new Random();
        _regex = new Regex(REG_EXP);
    }

    public IScriptResult ProcessRequest(IScriptRequest request)
    {
        Dictionary<string, int> results = new Dictionary<string, int>();
        string[] rolls = request.Instruction.Split(ScriptUtil.SeparatorArray, StringSplitOptions.RemoveEmptyEntries);
        string snumdice, sdiesize, ssign, smodifier, rolltype;
        string finalresult = "";
        int numdice, diesize, modifier, result;

        for (int i = 0; i < rolls.Length; i++)
        {
            Match match = _regex.Match(rolls[i]);
            if (match.Success)
            {
                snumdice = GetGroupValue(match.Groups, "numdice");
                sdiesize = GetGroupValue(match.Groups, "diesize");
                ssign = GetGroupValue(match.Groups, "sign");
                smodifier = GetGroupValue(match.Groups, "modifier");
                rolltype = GetGroupValue(match.Groups, "type");

                if (rolltype.Trim().Equals(""))
                {
                    rolltype = "none";
                }
                numdice = GetIntValue(snumdice.Trim());
                diesize = GetIntValue(sdiesize.Trim());
                modifier = GetIntValue(smodifier.Trim()) 
                    * (ssign.Trim().Equals("-") ? -1 : 1);

                result = RollDice(numdice, diesize, modifier);

                if (results.ContainsKey(rolltype))
                {
                    result += results[rolltype];
                    results[rolltype] = result;
                }
                else
                {
                    results.Add(rolltype, result);
                }
            }
        }

        IEnumerator<string> iter = results.Keys.GetEnumerator();
        if (iter.MoveNext())
        {
            finalresult = results[iter.Current].ToString();
            finalresult += iter.Current.Equals("none") ? "" : " (" + iter.Current + ")";
        }
        while (iter.MoveNext())
        {
            finalresult += ScriptUtil.Separator + results[iter.Current].ToString();
            finalresult += iter.Current.Equals("none") ? "" : " (" + iter.Current + ")";
        }

        return ScriptUtil.CreateResult(ScriptResult.ResultType.Success, finalresult);
    }

    /// <summary>
    /// Returns the group value for the group identified by the specified group
    /// name from the specified group collection.  If the group does not exist
    /// this method returns an empty string.
    /// </summary>
    /// <param name="groupCollection">The group collection to search.</param>
    /// <param name="groupName">The name of the group with the value to return.</param>
    /// <returns></returns>
    private string GetGroupValue(GroupCollection groupCollection, string groupName)
    {
        Group group;
        
        group = groupCollection[groupName];
        if (group.Success)
        {
            return group.Value;
        }
        return String.Empty;
    }

    /// <summary>
    /// Returns the integer equivalent of the specified string value. If the string
    /// value is not an integer or is an empty string, this method returns zero.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>The int32 equivalent value.</returns>
    private int GetIntValue(string value)
    {
        int val = 0;
        
        if (Int32.TryParse(value, out val))
        {
            return val;
        }

        return 0;
    }

    /// <summary>
    /// Returns a random total given the specified number of dice and a
    /// modifier.  Only one type of die is rolled per method call.
    /// </summary>
    /// <param name="numDice">The number of dice to roll.</param>
    /// <param name="dieSize">The die size to roll.</param>
    /// <param name="modifier">The numeric modifier to apply to the total.</param>
    /// <returns></returns>
    private int RollDice(int numDice, int dieSize, int modifier)
    {
        int total = modifier;

        if (numDice < 1 || dieSize < 1)
            return total;

        for (int i = 0; i < numDice; i++)
        {
            total += _randGen.Next(dieSize) + 1;
        }

        return total;
    }
}