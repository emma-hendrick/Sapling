namespace Sapling;
using Sapling.Lexer;
using Sapling.Tokens;
using System.Collections.Generic;

/// <summary>
/// Class <c>Constants</c> constains constants used elsewhere in the program.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// A list of valid tokens and their precedence in the Sapling programming language.
    /// </summary> 
    internal static List<(TokenDefinition, int)> _tokenList = new List<(TokenDefinition, int)>(){

        (new TokenDefinition(
            @"\b(True|False)\b", 
            "BooleanLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Boolean(startIndex, endIndex, value)), 
            10),
        
        (new TokenDefinition(
            @"\d+", 
            "IntegerLiteral", 
            (startIndex, endIndex, value) => new Integer(startIndex, endIndex, value)), 
            10),
        
        (new TokenDefinition(
            @"\d+.d+", 
            "FloatLiteral", 
            (startIndex, endIndex, value) => new Float(startIndex, endIndex, value)), 
            10),
        
        (new TokenDefinition(
            @"""[^""]*""", 
            "StringLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.String(startIndex, endIndex, value)), 
            10),
        
        (new TokenDefinition(
            @"'[^']'", 
            "CharacterLiteral", 
            (startIndex, endIndex, value) => new Sapling.Tokens.Character(startIndex, endIndex, value)), 
            10),

        // (new TokenDefinition(
        //     @"\b(if|else|for|while|return|switch|case|break|continue)\b", 
        //     "Keyword", 
        //     (startIndex, endIndex, value) => new Keyword(startIndex, endIndex, value)), 
        //     10),

        (new TokenDefinition(
            @"\b(int|float|str|char|bool|void)\b", 
            "Type", 
            (startIndex, endIndex, value) => new SaplingType(startIndex, endIndex, value)), 
            10),

        // (new TokenDefinition(
        //     @"(==|>|<|>=|<=|!=)", 
        //     "ComparisonOperator", 
        //     (startIndex, endIndex, value) => new ComparisonOperator(startIndex, endIndex, value)), 
        //     10),

        (new TokenDefinition(
            @"(=|\+|\-|\*|/)", 
            "ArithmeticOperator", 
            (startIndex, endIndex, value) => new ArithmeticOperator(startIndex, endIndex, value)), 
            10),

        (new TokenDefinition(
            @"(&&|\|\||\^)", 
            "BooleanOperator", 
            (startIndex, endIndex, value) => new BooleanOperator(startIndex, endIndex, value)), 
            10),
        
        // (new TokenDefinition(
        //     @"(\(|\)|\{|\}|\;|\?|\:)", 
        //     "Delimeter", 
        //     (startIndex, endIndex, value) => new Delimeter(startIndex, endIndex, value)), 
        //     10),
        
        (new TokenDefinition(
            @"\b[A-Za-z_]\w*\b", 
            "Identifier", 
            (startIndex, endIndex, value) => new ID(startIndex, endIndex, value)), 
            10),
        
        // (new TokenDefinition(
        //     @"#.*", 
        //     "Comment", 
        //     (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
        //     10),
        
        // (new TokenDefinition(
        //     @"#_.*_#", 
        //     "Multi-Line Comment", 
        //     (startIndex, endIndex, value) => new Comment(startIndex, endIndex, value)), 
        //     10),

    };
}