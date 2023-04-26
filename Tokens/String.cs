namespace Sapling.Tokens;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>String</c> represents a valid string ("a", "bob", "f34f893h4dwkSDVer!@%#") within the sapling programming language.
/// </summary>
internal class String: Value
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of a string.
    /// <example>
    /// For example:
    /// <code>
    /// String myName = new String(0, 0, "Michael");
    /// </code>
    /// will create a new String instance with the value of Michael.
    /// </example>
    /// </summary>
    public String(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }
}