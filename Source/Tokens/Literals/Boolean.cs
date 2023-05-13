namespace Sapling.Tokens;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Boolean</c> represents a valid boolean (True or False) within the sapling programming language.
/// </summary>
internal class Boolean: Value
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of a boolean.
    /// <example>
    /// For example:
    /// <code>
    /// Boolean dogsHaveTeeth = new Boolean(0, 0, "True");
    /// </code>
    /// will create a new Boolean instance with the value of True.
    /// </example>
    /// </summary>
    public Boolean(int startIndex, int endIndex, string value): base(startIndex, endIndex, Boolify(value))
    {
    }

    /// <summary>
    /// This will validate that the value passed to it is in fact a boolean. If it is not, it will convert it, or will raise an exception if the conversion fails.
    /// <example>
    /// For example:
    /// <code>
    /// Boolify("3.3");
    /// </code>
    /// would raise an exception while
    /// <code>
    /// Boolify("1");
    /// </code>
    /// would return "True"
    /// and
    /// <code>
    /// Boolify("false");
    /// </code>
    /// would return "False"
    /// </example>
    /// </summary>
    private static string Boolify(string value)
    {
        return value;
    }
}