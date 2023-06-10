namespace Sapling.Tokens;

/// <summary>
/// Class <c>String</c> represents a valid string ("a", "bob", "f34f893h4dwkSDVer!@%#") within the sapling programming language.
/// </summary>
internal class String: Token
{
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
    public String(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}