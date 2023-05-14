namespace Sapling.Tokens;
using Sapling.Interfaces;

/// <summary>
/// Class <c>Comment</c> represents a valid comment (# hello) within the sapling programming language.
/// </summary>
internal class Comment: Node, IToken
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){
            typeof(SaplingType)
        };
    }
    /// <summary>
    /// This construsts a new instance of an comment (# hello).
    /// <example>
    /// For example:
    /// <code>
    /// Comment lemoofle = new Comment(0, 0, "# lemoodle");
    /// </code>
    /// will create a new Comment instance with the value of lemoodle.
    /// </example>
    /// </summary>
    public Comment(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}