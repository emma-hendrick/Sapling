namespace Sapling.Lexer;
using Sapling.Tokens;

/// <summary>
/// Class <c>PrecedenceBasedLexer</c> interprets text from a file and gets valid tokens from the text utilizing regex and precedence.
/// </summary>
internal class PrecedenceBasedLexer
{
    /// <summary>
    /// A queue of valid tokens and their precedence in a programming language.
    /// </summary>    
    private readonly PriorityQueue<TokenDefinition, int> _tokenQueue;

    /// <summary>
    /// This constructs a new PrecedenceBasedLexer.
    /// </summary>
    public PrecedenceBasedLexer(List<(TokenDefinition, int)> tokenList)
    {
        _tokenQueue = new PriorityQueue<TokenDefinition, int>(tokenList);
    }

    /// <summary>
    /// Returns all the tokens in order of precedence.
    /// </summary>
    public IEnumerable<Token> GetTokens(string inputString)
    {
        List<Token> tokens = new List<Token>();
        string inputCopy = inputString;

        while (_tokenQueue.Count > 0)
        {
            TokenDefinition tokenDefinition = _tokenQueue.Dequeue();
            IEnumerable<Token> tokenMatches = tokenDefinition.FindMatches(inputString);
            inputString = tokenDefinition.GetPattern().Replace(inputString, match => new string(' ', match.Length));
            tokens.AddRange(tokenMatches);
        }

        tokens.Sort((t1, t2) => t1.StartIndex.CompareTo(t2.StartIndex));

        int startIndex = 0;
        foreach (Token token in tokens)
        {
            if (token.StartIndex != startIndex && inputCopy.Substring(startIndex, token.StartIndex - startIndex).Trim().Length != 0) throw new Exception($"Unmatched characters \"{inputCopy.Substring(startIndex, token.StartIndex - startIndex)}\" in input string from {startIndex} to {token.StartIndex}."); 
            startIndex = token.EndIndex;
        }

        return tokens;
    }
}

