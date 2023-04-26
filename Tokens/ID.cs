namespace Sapling.Tokens;
using Sapling.Interfaces;

/// <summary>
/// Class <c>ID</c> represents a valid id (x, y, myFunc, predicate?, isCheese?) within the sapling programming language.
/// </summary>
internal class ID: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){
            typeof(SaplingType)
        };
    }
    /// <summary>
    /// This construsts a new instance of an id (x, y, myFunc).
    /// <example>
    /// For example:
    /// <code>
    /// ID x = new ID(0, 0, "x");
    /// </code>
    /// will create a new ID instance with the value of x (which will be used then as a variable or function name).
    /// </example>
    /// </summary>
    public ID(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }
}