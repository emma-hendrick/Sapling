namespace Sapling.Tokens;

/// <summary>
/// Class <c>Token</c> represents a valid lexicon within the sapling programming language.
/// </summary>
internal abstract class Token
{
    /// <summary>
    /// How many characters the beginning of this token is from the beginning of the program.
    /// </summary>
    private int _startIndex;
    public int StartIndex
    {
        get => _startIndex;
    }

    /// <summary>
    /// How many characters the end of this token is from the beginning of the program.
    /// </summary>
    private int _endIndex;
    public int EndIndex
    {
        get => _endIndex;
    }
    
    /// <summary>
    /// The value this token is given. For a variable this would be the id, for a boolean, it would be true or false.
    /// </summary>
    private string _value;
    public string Value
    {
        get => _value;
    }

    public Token(int startIndex, int endIndex, string value)
    {
        _startIndex = startIndex;
        _endIndex = endIndex;
        _value = value;
    }

    
}