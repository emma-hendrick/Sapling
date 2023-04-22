namespace Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Interface <c>Token</c> represents a valid lexicon within the sapling programming language.
/// </summary>
internal interface IToken: IEquatable<IToken>
{
    public int LineNum
    {
        get;
    }
    public int LinePos
    {
        get;
    }
    public string Value
    {
        get;
    }
    public List<Type> RequiredChildren
    {
        get;
    }
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