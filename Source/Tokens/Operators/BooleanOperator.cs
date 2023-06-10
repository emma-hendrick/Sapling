namespace Sapling.Tokens;

/// <summary>
/// Class <c>BooleanOperator</c> represents a valid boolean operator (such as and, or, and xor) within the sapling programming language.
/// </summary>
internal class BooleanOperator: Token
{
    /// <summary>
    /// This construsts a new instance of a boolean operator.
    /// <example>
    /// For example:
    /// <code>
    /// BooleanOperator or = new BooleanOperator(0, 0, "or");
    /// </code>
    /// will create a new instance of a boolean operator.
    /// </example>
    /// </summary>
    public BooleanOperator(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}