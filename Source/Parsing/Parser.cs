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
    /// will create a new Parser, which can then be used to generate the AST for a set of tokens.
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
            _current = GetNextNode();
            return _current;
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
            _current = GetNextNode();
            return _current;
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
    public AST Parse()
    {
        // Create a new instance of an abstract syntax tree
        AST ast = new AST(_logger);

        // Parse the tokens and create AST nodes from them
        while (_current != null)
        {
            switch (_current.Value.GetType().Name)
            {
                case (nameof(Sapling.Tokens.SaplingType)):

                    // Assign to methods and classes as needed, else assign to a property
                    if (_current.Value.Value == "method") ParseAssignMethod();
                    else if (_current.Value.Value == "class") ParseAssignClass();
                    else ParseAssignProperty();
                    break;

                case (nameof(Sapling.Tokens.Keyword)):

                    // Parse the return statement if the keyword is return, if it isnt, throw an error
                    if (_current.Value.Value == "return") ParseReturn();
                    else throw new Exception($"Unexpected keyword \"{_current.Value.Value}\" in input string from {_current.Value.StartIndex} to {_current.Value.EndIndex}."); 
                    break;

                case (nameof(Sapling.Tokens.Boolean)): case (nameof(Sapling.Tokens.Character)): case (nameof(Sapling.Tokens.String)): case (nameof(Sapling.Tokens.Float)): case (nameof(Sapling.Tokens.Integer)): 

                    // Assign to methods and classes as needed, else assign to a property
                    ParseExpression();
                    break;

                case (nameof(Sapling.Tokens.Delimeter)):

                    // Parse a Paren expression if it is a L parenthesis, otherwise throw an error
                    if (_current.Value.Value == "(") ParseParenExpression();
                    else throw new Exception($"Unexpected delimiter \"{_current.Value.Value}\" in input string from {_current.Value.StartIndex} to {_current.Value.EndIndex}."); 
                    break;

                case (nameof(Sapling.Tokens.ID)):

                    // Parse a identifier as a function if it is immediately followed by a left parenthesis
                    if (_current.Next is not null && _current.Next.Value.Value == "(") ParseFunctionCall();
                    else ParseIdentifier();
                    break;

                default:
                    throw new Exception("Gadzooks! There was an unexpected token at the end of the parser!!");
            }

            // Update the _current linked list node
            GetNextNode();
        }

        // Return the tree
        return ast;
    }

    /// <summary>
    /// This method parses an assignment operator for a property and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignProperty ParseAssignProperty()
    {
        return new SlAssignProperty();
    }

    /// <summary>
    /// This method parses an assignment operator for a method and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignMethod ParseAssignMethod()
    {
        return new SlAssignMethod();
    }

    /// <summary>
    /// This method parses an assignment operator for a function and adds the needed nodes to the AST.
    /// </summary>
    private SlAssignClass ParseAssignClass()
    {
        return new SlAssignClass();
    }

    /// <summary>
    /// This method parses an expression and adds the needed nodes to the AST.
    /// </summary>
    private SlExpression ParseExpression()
    {
        return new SlExpression();
    }

    /// <summary>
    /// This method parses an identifier and adds the needed nodes to the AST.
    /// </summary>
    private SlIdentifierExpression ParseIdentifier()
    {
        return new SlIdentifierExpression();
    }

    /// <summary>
    /// This method parses a parensthetical expression and adds the needed nodes to the AST.
    /// </summary>
    private SlParenExpression ParseParenExpression()
    {
        return new SlParenExpression();
    }

    /// <summary>
    /// This method parses a return statement and adds the needed nodes to the AST.
    /// </summary>
    private SlReturn ParseReturn()
    {
        return new SlReturn();
    }

    /// <summary>
    /// This method calls a function and adds the needed nodes to the AST.
    /// </summary>
    private SlFunctionCall ParseFunctionCall()
    {
        return new SlFunctionCall();
    }
}