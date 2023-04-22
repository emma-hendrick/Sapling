namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Float</c> represents a valid floating point number (3.2, 6.63, 7.3, -3.02) within the sapling programming language.
/// </summary>
internal class Float: Number
{
    public Float(int lineNum, int linePos, string value): base(lineNum, linePos, Floatify(value))
    {
    }

    private static string Floatify(string value)
    {
        return value;
    }
}