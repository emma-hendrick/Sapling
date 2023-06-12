namespace Sapling;
using Sapling.Logging;
using Sapling.Tokens;
using static Sapling.UtilFuncs;

/// <summary>
/// Class <c>Parser</c> converts a list of tokens into an abstract syntax tree.
/// </summary>
internal class Parser
{
    private IEnumerable<Token> _tokens;
    private Logger _logger;

    /// <summary>
    /// This construsts a new Parser.
    /// <example>
    /// For example:
    /// <code>
    /// Parser p = new Parser(tokens, log);
    /// </code>
    /// will create a new Parser, which can then be used to generate the LLVM for a set of tokens.
    /// </example>
    /// </summary>
    public Parser(IEnumerable<Token> tokens, Logger logger)
    {
        _tokens = tokens;
        _logger = logger;
    }

    /// <summary>
    /// This method generates the AST from the tokens returned by the lexer.
    /// </summary>
    public void Parse()
    {
        // Parse the tokens
        foreach (Tokens.Token token in _tokens)
        {
            if (!TypeEquivalence(typeof(Tokens.Comment), token.GetType())) _logger.Add($"{token.GetType()} \"{token.Value}\" at {token.StartIndex} to {token.EndIndex}");
        }
    }

}