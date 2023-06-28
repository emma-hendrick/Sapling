namespace Sapling;
using Sapling.Lexer;
using Sapling.Tokens;
using System.Collections.Generic;

/// <summary>
/// Class <c>Constants</c> contains constants used elsewhere in the program.
/// </summary>
internal static class Constants
{
    
    /// <summary>
    /// Default of whether or not to print our loggers output.
    /// </summary> 
    internal const string DefaultPrintOutput = "false";
    
    /// <summary>
    /// Default of whether or not to debug.
    /// </summary> 
    internal const string DefaultDebug = "false";

    /// <summary>
    /// The name of the default sapling file.
    /// </summary> 
    internal const string DefaultFileName = "source.sl";

    /// <summary>
    /// The default target of the compiler
    /// </summary>
    internal const string DefaultTarget = "";
    
    /// <summary>
    /// A list of valid tokens and their precedence in the Sapling programming language.
    /// </summary> 
    internal static List<(TokenDefinition, int)> _tokenList = new List<(TokenDefinition, int)>(){
        
        (new TokenDefinition(
            @"###_.*_###", 
            "Multi-Line Embedded Documentation Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            -2),

        (new TokenDefinition(
            @"#_.*_#", 
            "Multi-Line Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            -1),
                    
        (new TokenDefinition(
            @"#.*", 
            "Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            0),
        
        (new TokenDefinition(
            @"-?\d+", 
            "IntegerLiteral", 
            (startIndex, endIndex, value) => new Integer(startIndex, endIndex, value)), 
            5),
        
        (new TokenDefinition(
            @"-?\d+.d+", 
            "FloatLiteral", 
            (startIndex, endIndex, value) => new Float(startIndex, endIndex, value)), 
            4),
        
        (new TokenDefinition(
            @"'[^']'", 
            "CharacterLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Character(startIndex, endIndex, value)), 
            4),

        (new TokenDefinition(
            @"\b(true|false)\b", 
            "BooleanLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Boolean(startIndex, endIndex, value)), 
            4),
        
        (new TokenDefinition(
            @"""[^""]*""", 
            "StringLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.String(startIndex, endIndex, value)), 
            5),

        (new TokenDefinition(
            @"\b(int|float|str|char|bool)\b", 
            "Type", 
            (startIndex, endIndex, value) => new SaplingType(startIndex, endIndex, value)), 
            3),

        (new TokenDefinition(
            @"\b(return)\b", 
            "Keyword", 
            (startIndex, endIndex, value) => new Keyword(startIndex, endIndex, value)), 
            2),

        (new TokenDefinition(
            @"(==|>|<|>=|<=|!=)", 
            "ComparisonOperator", 
            (startIndex, endIndex, value) => new ComparisonOperator(startIndex, endIndex, value)), 
            6),

        (new TokenDefinition(
            @"(\+|\-|\*|/)", 
            "ArithmeticOperator", 
            (startIndex, endIndex, value) => new ArithmeticOperator(startIndex, endIndex, value)), 
            6),

        (new TokenDefinition(
            @"(&&|\|\||\^)", 
            "BooleanOperator", 
            (startIndex, endIndex, value) => new BooleanOperator(startIndex, endIndex, value)), 
            6),

        (new TokenDefinition(
            @"=", 
            "Assignment", 
            (startIndex, endIndex, value) => new Assign(startIndex, endIndex, value)), 
            7),
        
        (new TokenDefinition(
            @"(\(|\)|\{|\}|\;)", 
            "Delimeter", 
            (startIndex, endIndex, value) => new Delimeter(startIndex, endIndex, value)), 
            1),
        
        (new TokenDefinition(
            @"\b[A-Za-z_]\w*\b", 
            "Identifier", 
            (startIndex, endIndex, value) => new ID(startIndex, endIndex, value)), 
            100),
    };
    
    // A list of types which are equivalent
    public static Dictionary<string, string> EquivalentTypes = new Dictionary<string, string>
    {
        {"int", "int"},
        {"Integer", "int"},
        {"bool", "bool"},
        {"Boolean", "bool"}
    };
}