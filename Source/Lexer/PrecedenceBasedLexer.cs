namespace Sapling.Lexer;
using Sapling.Tokens;
using System.Text.RegularExpressions;
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
    public IEnumerable<Node> GetTokens(string inputString)
    {
        List<Node> tokens = new List<Node>();
        string inputCopy = inputString;

        while (_tokenQueue.Count > 0)
        {
            TokenDefinition tokenDefinition = _tokenQueue.Dequeue();
            IEnumerable<Node> tokenMatches = tokenDefinition.FindMatches(inputString);
            inputString = tokenDefinition.GetPattern().Replace(inputString, match => new string(' ', match.Length));
            tokens.AddRange(tokenMatches);
        }

        tokens.Sort((t1, t2) => t1.startIndex.CompareTo(t2.startIndex));

        int startIndex = 0;
        foreach (Node token in tokens)
        {
            if (token.startIndex != startIndex && inputCopy.Substring(startIndex, token.startIndex - startIndex).Trim().Length != 0) throw new Exception($"Unmatched characters \"{inputCopy.Substring(startIndex, token.startIndex - startIndex)}\" in input string from {startIndex} to {token.startIndex}."); 
            startIndex = token.endIndex;
        }

        return tokens;
    }
}

