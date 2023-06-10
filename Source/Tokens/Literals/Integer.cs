namespace Sapling.Tokens;

/// <summary>
/// Class <c>Integer</c> represents a valid integer (3, 6, 7, -3) within the sapling programming language.
/// </summary>
internal class Integer: Token
{
    /// <summary>
    /// This construsts a new instance of an integer.
    /// <example>
    /// For example:
    /// <code>
    /// Integer pi = new Integer(0, 0, "3");
    /// </code>
    /// will create a new Integer instance with the value of 3.
    /// </example>
    /// </summary>
    public Integer(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}