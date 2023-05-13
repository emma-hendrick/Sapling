namespace Sapling.Tokens;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>String</c> represents a valid character ('a', 'b', '╝') within the sapling programming language.
/// </summary>
internal class Character: Value
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of a character.
    /// <example>
    /// For example:
    /// <code>
    /// Character myInitial = new Character(0, 0, "M");
    /// </code>
    /// will create a new Character instance with the value of M.
    /// </example>
    /// </summary>
    public Character(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}