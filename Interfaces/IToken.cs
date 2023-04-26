namespace Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Interface <c>Token</c> represents a valid lexicon within the sapling programming language.
/// </summary>
internal interface IToken: IEquatable<IToken>
{
    /// <summary>
    /// The line number the token is found on.
    /// </summary>
    public int LineNum
    {
        get;
    }

    /// <summary>
    /// How many characters the token is from the left side of the screen.
    /// </summary>
    public int LinePos
    {
        get;
    }
    
    /// <summary>
    /// The value this token is given. For a variable this would be the id, for a boolean, it would be true or false.
    /// </summary>
    public string Value
    {
        get;
    }
    
    /// <summary>
    /// The children required for a token to execute.
    /// </summary>
    public List<Type> RequiredChildren
    {
        get;
    }
    
    /// <summary>
    /// The token's current children.
    /// </summary>
    public List<IToken> CurrentChildren
    {
        get; 
        set;
    } 

    /// <summary>
    /// Return true if two Tokens are equal.
    /// <example>
    /// For example:
    /// <code>
    /// return operator1.Equals(operator1);
    /// </code>
    /// will return true.
    /// </example>
    /// </summary>
    new public bool Equals(IToken? other);
}