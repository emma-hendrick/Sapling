namespace Sapling;
using Sapling.Logging;
using Sapling.Tokens;
using Sapling.Nodes;
using static Sapling.UtilFuncs;

/// <summary>
/// Class <c>Parser</c> converts a list of tokens into an abstract syntax tree.
/// </summary>
internal class Parser
{
    /// <summary>
    /// The tokens to parse.
    /// </summary>
    private LinkedList<Token> _tokens;
    
    /// <summary>
    /// The _current token.
    /// </summary>
    private LinkedListNode<Token>? _current;

    /// <summary>
    /// The logger to use.
    /// </summary>
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
        _tokens = new LinkedList<Token>(tokens);
        _logger = logger;
        _current = GetFirstNode();
    }

    /// <summary>
    /// Log a token.
    /// <example>
    private void LogToken(Token token)
    {
        _logger.Add($"{token.GetType()} \"{token.Value}\" at {token.StartIndex} to {token.EndIndex}");
    }

    /// <summary>
    /// Get the next node from the linked list.
    /// <example>
    private LinkedListNode<Token>? GetNextNode()
    {
        // Return null if it was already null
        if (_current is null) return null;

        // Get the next node
        _current = _current.Next;
        if (_current is null) return null;

        // Ignore the comments
        if (TypeEquivalence(typeof(Tokens.Comment), _current.Value.GetType())) 
        {    
            return GetNextNode();
        }

        // Get the _current token
        Token token = _current.Value;
        LogToken(token);
        
        // Return the _current token
        return _current;
    }

    /// <summary>
    /// Get the first node from the linked list.
    /// <example>
    private LinkedListNode<Token>? GetFirstNode()
    {
        // Get the next node
        _current = _tokens.First;
        if (_current is null) return null;

        // Ignore the comments
        if (TypeEquivalence(typeof(Tokens.Comment), _current.Value.GetType())) 
        {    
            return GetNextNode();
        }

        // Get the _current token
        Token token = _current.Value;
        LogToken(token);
        
        // Return the _current token
        return _current;
    }

    /// <summary>
    /// This method generates the AST from the tokens returned by the lexer.
    /// </summary>
    public void Parse()
    {
        // Parse the tokens
        while (_current != null)
        {
            // Update the _current node
            _current = GetNextNode();
        }
    }

    /// <summary>
    /// This method parses an assignment operator for a property and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignProperty ParseAssignProperty()
    {
    }

    /// <summary>
    /// This method parses an assignment operator for a method and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignMethod ParseAssignMethod()
    {
    }

    /// <summary>
    /// This method parses an assignment operator for a function and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignClass ParseAssignClass()
    {
    }

    /// <summary>
    /// This method parses an expression and adds the needed nodes to the AST.
    /// </summary>
    private SlExpression ParseExpression()
    {
    }

    /// <summary>
    /// This method parses an identifier and adds the needed nodes to the AST.
    /// </summary>
    private SlExpression ParseIdentifier()
    {
    }

    /// <summary>
    /// This method parses a parensthetical expression and adds the needed nodes to the AST.
    /// </summary>
    private SlParenExpression ParseParenExpression()
    {
    }

    /// <summary>
    /// This method parses a return statement and adds the needed nodes to the AST.
    /// </summary>
    private SlReturn ParseReturn()
    {
    }

    /// <summary>
    /// This method calls a function and adds the needed nodes to the AST.
    /// </summary>
    private SlFunctionCall ParseFunctionCall()
    {
    }
}