namespace Sapling.Tokens;

/// <summary>
/// Class <c>String</c> represents a valid character ('a', 'b', '╝') within the sapling programming language.
/// </summary>
internal class Character: Token
{
    /// <summary>
    /// This construsts a new instance of a character.
    /// <example>
    /// For example:
    /// <code>
    /// Character myInitial = new Character(0, 0, "M");
    /// </code>
    /// will create a new Character instance with the value of M.
    /// </example>
    /// </summary>
    public Character(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}