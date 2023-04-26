namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Number</c> represents a valid number (3, 6, 4.5, -3) within the sapling programming language.
/// </summary>
internal abstract class Number: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of an number.
    /// <example>
    /// For example:
    /// <code>
    /// Number pi = new Number(0, 0, "3.14");
    /// </code>
    /// will create a new Number instance with the value of 3.14.
    /// </example>
    /// </summary>
    public Number(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }
}