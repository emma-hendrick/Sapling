namespace Sapling.Tokens;

/// <summary>
/// Class <c>Ternary</c> represents part of the ternary operator ( x ? y : z ) within the sapling programming language.
/// </summary>
internal class Ternary: Token
{
    /// <summary>
    /// This construsts a new instance of an ternary operator ( ? and : ).
    /// <example>
    /// For example:
    /// <code>
    /// Ternary tern = new Ternary(0, 0, ":");
    /// </code>
    /// will create a new Ternary instance with the value of :.
    /// </example>
    /// </summary>
    public Ternary(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}