namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Number</c> represents a valid number (3, 6, 4.5, -3) within the sapling programming language.
/// </summary>
internal class Number: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    public Number(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }
}