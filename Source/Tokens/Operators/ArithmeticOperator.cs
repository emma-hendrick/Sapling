namespace Sapling.Tokens;

/// <summary>
/// Class <c>ArithmeticOperator</c> represents a valid numerical operator (such as + - * or /) within the sapling programming language.
/// </summary>
internal class ArithmeticOperator: Token
{
    /// <summary>
    /// This construsts a new instance of a number operator.
    /// <example>
    /// For example:
    /// <code>
    /// ArithmeticOperator multiply = new ArithmeticOperator(0, 0, "*");
    /// </code>
    /// will create a new instance of an arithmetic operator.
    /// </example>
    /// </summary>
    public ArithmeticOperator(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}