namespace Sapling.Tokens;

/// <summary>
/// Class <c>Delimeter</c> represents a valid delimeter ( { } ) within the sapling programming language.
/// </summary>
internal class Delimeter: Token
{
    /// <summary>
    /// This construsts a new instance of a delimeter ( { } ).
    /// <example>
    /// For example:
    /// <code>
    /// Delimeter LParen = new Delimeter(0, 0, "(");
    /// </code>
    /// will create a new Delimeter instance with the value of (.
    /// </example>
    /// </summary>
    public Delimeter(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}