namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Integer</c> represents a valid integer (3, 6, 7, -3) within the sapling programming language.
/// </summary>
internal class Integer: Number
{
    public Integer(int lineNum, int linePos, string value): base(lineNum, linePos, Intify(value))
    {
    }

    private static string Intify(string value)
    {
        return value;
    }
}