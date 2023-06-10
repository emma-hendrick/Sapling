namespace Sapling.Tokens;

/// <summary>
/// Class <c>ComparisonOperator</c> represents a valid comparison operator (such <, ==, >=, !=) within the sapling programming language.
/// </summary>
internal class ComparisonOperator: Token
{
    /// <summary>
    /// This construsts a new instance of a comparison operator.
    /// <example>
    /// For example:
    /// <code>
    /// ComparisonOperator LT = new ComparisonOperator(0, 0, "<");
    /// </code>
    /// will create a new comparison operator.
    /// </example>
    /// </summary>
    public ComparisonOperator(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}