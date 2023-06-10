namespace Sapling.Lexer;
using Sapling.Tokens;
using System.Text.RegularExpressions;

/// <summary>
/// Class <c>TokenDefinition</c> allows us to define a tokens pattern, and the token and precedence associated with it.
/// </summary>
internal class TokenDefinition
{
    /// <summary>
    /// The regular expression which will be used to match the tokens.
    /// </summary>
    private Regex _regex;
    
    /// <summary>
    /// The name of the associated token.
    /// </summary>
    private readonly string _name;
    
    /// <summary>
    /// The constructor of the associated token.
    /// </summary>
    private readonly Func<int, int, string, Token> _constructor;
        
    /// <summary>
    /// This constructs a new token definition.
    /// </summary>
    public TokenDefinition(string regexPattern, string name, Func<int, int, string, Token> constructor)
    {
        _regex = new Regex(regexPattern, RegexOptions.Compiled);
        _name = name;
        _constructor = constructor;
    }

    /// <summary>
    /// Get the pattern which will be used to match the tokens.
    /// </summary>
    public Regex GetPattern()
    {
        return _regex;
    }

    /// <summary>
    /// method <c>NumberOperator</c> represents a valid numerical operator (such as + - * or /) within the sapling programming language.
    /// </summary>
    public IEnumerable<Token> FindMatches(string inputString)
    {
        var matches = _regex.Matches(inputString);
        for(int i=0; i<matches.Count; i++)
        {
            yield return _constructor(matches[i].Index, matches[i].Index + matches[i].Length, matches[i].Value);
        }
    }
}