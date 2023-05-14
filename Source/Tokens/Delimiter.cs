namespace Sapling.Tokens;
using Sapling.Interfaces;

/// <summary>
/// Class <c>Delimeter</c> represents a valid delimeter ( { } ) within the sapling programming language.
/// </summary>
internal class Delimeter: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){};
    }

    /// <summary>
    /// This construsts a new instance of a delimeter ( { } ).
    /// <example>
    /// For example:
    /// <code>
    /// Delimeter LParen = new Delimeter(0, 0, "(");
    /// </code>
    /// will create a new Delimeter instance with the value of (.
    /// </example>
    /// </summary>
    public Delimeter(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}