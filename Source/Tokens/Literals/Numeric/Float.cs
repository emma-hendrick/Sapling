namespace Sapling.Tokens;

/// <summary>
/// Class <c>Float</c> represents a valid floating point number (3.2, 6.63, 7.3, -3.02) within the sapling programming language.
/// </summary>
internal class Float: Number
{
    /// <summary>
    /// This construsts a new instance of a floating point number.
    /// <example>
    /// For example:
    /// <code>
    /// Float pi = new Float(0, 0, "3.14");
    /// </code>
    /// will create a new Float instance with the value of 3.14.
    /// </example>
    /// </summary>
    public Float(int startIndex, int endIndex, string value): base(startIndex, endIndex, Floatify(value))
    {
    }

    /// <summary>
    /// This will validate that the number passed to it is in fact a float. If it is not, it will convert it, or will raise an exception if the conversion fails.
    /// <example>
    /// For example:
    /// <code>
    /// Floatify("3");
    /// </code>
    /// would return "3.0".
    /// </example>
    /// </summary>
    private static string Floatify(string value)
    {
        return value;
    }
}