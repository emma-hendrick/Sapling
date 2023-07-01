namespace Sapling;
using Sapling.Lexer;
using Sapling.Tokens;
using System.Collections.Generic;

/// <summary>
/// Class <c>Constants</c> contains constants used in the program.
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
    internal static List<(TokenDefinition, int)> TokenList = new List<(TokenDefinition, int)>(){
        
        // Comments should be parsed first
        (new TokenDefinition(
            @"###_.*_###", 
            "Multi-Line Embedded Documentation Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            1),

        (new TokenDefinition(
            @"#_.*_#", 
            "Multi-Line Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            2),
                    
        (new TokenDefinition(
            @"#.*", 
            "Comment", 
            (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
            3),
        
        // Literals should be parsed next
        (new TokenDefinition(
            @"""[^""]*""", 
            "StringLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.String(startIndex, endIndex, value)), 
            11),

        (new TokenDefinition(
            @"'[^']'", 
            "CharacterLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Character(startIndex, endIndex, value)), 
            12),
        
        (new TokenDefinition(
            @"(?<!\w)-?\d+\.\d+", 
            "FloatLiteral", 
            (startIndex, endIndex, value) => new Float(startIndex, endIndex, value)), 
            13),

        (new TokenDefinition(
            @"(?<!\w)-?\d+",
            "IntegerLiteral", 
            (startIndex, endIndex, value) => new Integer(startIndex, endIndex, value)), 
            14),

        (new TokenDefinition(
            @"\b(true|false)\b", 
            "BooleanLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Boolean(startIndex, endIndex, value)), 
            15),
        
        // Next we will have delimiters, keywords, types and BIFs
        (new TokenDefinition(
            @"(\(|\)|\{|\}|\;)", 
            "Delimeter", 
            (startIndex, endIndex, value) => new Delimeter(startIndex, endIndex, value)), 
            21),

        (new TokenDefinition(
            @"\b(return)\b", 
            "Keyword", 
            (startIndex, endIndex, value) => new Keyword(startIndex, endIndex, value)), 
            22),

        (new TokenDefinition(
            @"\b(int|float|str|char|bool)\b", 
            "Type", 
            (startIndex, endIndex, value) => new SaplingType(startIndex, endIndex, value)), 
            23),
        
        (new TokenDefinition(
            @"(printf|getchar)", 
            "Builtin Method", 
            (startIndex, endIndex, value) => new Builtin(startIndex, endIndex, value)), 
            24),

        // Next we will have all of our operators
        (new TokenDefinition(
            @"(:|\?)", 
            "TernaryOperator", 
            (startIndex, endIndex, value) => new Ternary(startIndex, endIndex, value)), 
            31),

        (new TokenDefinition(
            @"(==|!=|>=|<=|>|<)", 
            "ComparisonOperator", 
            (startIndex, endIndex, value) => new ComparisonOperator(startIndex, endIndex, value)), 
            32),

        (new TokenDefinition(
            @"(\+|\-|\*|/)", 
            "ArithmeticOperator", 
            (startIndex, endIndex, value) => new ArithmeticOperator(startIndex, endIndex, value)), 
            32),

        (new TokenDefinition(
            @"(&&|\|\||\^)", 
            "BooleanOperator", 
            (startIndex, endIndex, value) => new BooleanOperator(startIndex, endIndex, value)), 
            32),

        (new TokenDefinition(
            @"=", 
            "Assignment", 
            (startIndex, endIndex, value) => new Assign(startIndex, endIndex, value)), 
            33),
        
        // Lastly we will have identifiers
        (new TokenDefinition(
            @"\b[A-Za-z_]\w*\b", 
            "Identifier", 
            (startIndex, endIndex, value) => new ID(startIndex, endIndex, value)), 
            41),
    };
    
    /// <summary>
    /// A list of type names which are equivalent for parsing
    /// </summary> 
    public static Dictionary<string, string> EquivalentParsingTypes = new Dictionary<string, string>
    {
        {"int", "int"},
        {"Integer", "int"},
        {"bool", "bool"},
        {"Boolean", "bool"},
        {"char", "char"}, 
        {"Character", "char"},
        {"str", "str"},
        {"String", "str"},
        {"float", "float"},
        {"Float", "float"},
    };
    
    /// <summary>
    /// A list of type names which are equivalent
    /// </summary> 
    public static Dictionary<string, string> EquivalentExpressionTypes = new Dictionary<string, string>
    {
        {"int", "int"},
        {"bool", "bool"},
        {"char", "int"}, 
        {"str", "str"},
        {"float", "float"},
    };
}