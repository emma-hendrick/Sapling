namespace Sapling.Tokens;

/// <summary>
/// Class <c>Integer</c> represents a valid integer (3, 6, 7, -3) within the sapling programming language.
/// </summary>
internal class Integer: Number
{
    /// <summary>
    /// This construsts a new instance of an integer.
    /// <example>
    /// For example:
    /// <code>
    /// Integer pi = new Integer(0, 0, "3");
    /// </code>
    /// will create a new Integer instance with the value of 3.
    /// </example>
    /// </summary>
    public Integer(int startIndex, int endIndex, string value): base(startIndex, endIndex, Intify(value))
    {
    }

    /// <summary>
    /// This will validate that the number passed to it is in fact an int. If it is not, it will convert it, or will raise an exception if the conversion fails.
    /// <example>
    /// For example:
    /// <code>
    /// Intify("3.3");
    /// </code>
    /// would raise an exception while
    /// <code>
    /// Intify("3.0");
    /// </code>
    /// would return "3".
    /// </example>
    /// </summary>
    private static string Intify(string value)
    {
        return value;
    }
}