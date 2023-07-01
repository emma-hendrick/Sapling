namespace Sapling.Tokens;

/// <summary>
/// Class <c>Builtin</c> represents a builtin method ( printf / getchar ) within the sapling programming language.
/// </summary>
internal class Builtin: Token
{
    /// <summary>
    /// This construsts a new instance of a builtin function ( printf / getchar ).
    /// <example>
    /// For example:
    /// <code>
    /// Builtin print = new Builtin(0, 0, "printf");
    /// </code>
    /// will create a new Builtin instance with the value of printf.
    /// </example>
    /// </summary>
    public Builtin(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}