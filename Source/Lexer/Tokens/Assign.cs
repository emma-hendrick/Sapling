namespace Sapling.Tokens;

/// <summary>
/// Class <c>Asslgn</c> represents the assignment operator ( = ) within the sapling programming language.
/// </summary>
internal class Assign: Token
{
    /// <summary>
    /// This construsts a new instance of an assignment operator ( = ).
    /// <example>
    /// For example:
    /// <code>
    /// Assign eq = new Assign(0, 0, "=");
    /// </code>
    /// will create a new Assign instance with the value of =.
    /// </example>
    /// </summary>
    public Assign(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}