namespace Sapling.Tokens;
using Sapling.Interfaces;

/// <summary>
/// Class <c>Value</c> represents a valid value (string, bool, int, etc) within the sapling programming language.
/// </summary>
internal abstract class Value: Node, IToken
{
    /// <summary>
    /// This construsts a new instance of a value (string, bool, int, etc).
    /// <example>
    /// For example:
    /// <code>
    /// Value myName = new Value(0, 0, "Michael");
    /// </code>
    /// will create a new Value instance with the value of "Michael".
    /// </example>
    /// </summary>
    public Value(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}