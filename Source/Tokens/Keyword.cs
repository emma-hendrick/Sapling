namespace Sapling.Tokens;
using Sapling.Interfaces;

/// <summary>
/// Class <c>Keyword</c> represents a valid keyword (for, while, switch, if) within the sapling programming language.
/// </summary>
internal class Keyword: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of an keyword (for, while, switch, if).
    /// <example>
    /// For example:
    /// <code>
    /// Keyword If = new Keyword(0, 0, "if");
    /// </code>
    /// will create a new Keyword instance with the value of if.
    /// </example>
    /// </summary>
    public Keyword(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}