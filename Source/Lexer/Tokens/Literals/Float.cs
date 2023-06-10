namespace Sapling.Tokens;

/// <summary>
/// Class <c>Float</c> represents a valid floating point number (3.2, 6.63, 7.3, -3.02) within the sapling programming language.
/// </summary>
internal class Float: Token
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
    public Float(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}