namespace Sapling.Interfaces;
using Sapling.Tokens;

/// <summary>
/// Interface <c>IOperator</c> represents a valid operator (such as + - and not, etc) within the sapling programming language.
/// </summary>
internal interface IOperator: IToken
{
    /// <summary>
    /// Evaluates the token
    /// <example>
    /// For example:
    /// <code>
    /// NumberOperator operator1 = new Operator(1, 1, "+");
    ///
    /// operator1.AddChild(new Integer(3));
    ///
    /// operator1.AddChild(new Integer(4));
    ///
    /// operator1.Evaluate();
    /// </code>
    /// will return 7.
    /// </example>
    /// </summary>
    public Value Evaluate();
}