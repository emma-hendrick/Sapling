namespace Sapling.Tokens;

/// <summary>
/// Class <c>Boolean</c> represents a valid boolean (True or False) within the sapling programming language.
/// </summary>
internal class Boolean: Token
{
    /// <summary>
    /// This construsts a new instance of a boolean.
    /// <example>
    /// For example:
    /// <code>
    /// Boolean dogsHaveTeeth = new Boolean(0, 0, "True");
    /// </code>
    /// will create a new Boolean instance with the value of True.
    /// </example>
    /// </summary>
    public Boolean(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }
}