namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Node</c> represents a valid ASTNode (such as Float, NumberOperator, or many others) within the sapling programming language.
/// </summary>
internal abstract class Node: IToken
{
    protected int _lineNum;
    public int LineNum
    {
        get => _lineNum;
    }

    protected int _linePos;
    public int LinePos
    {
        get => _linePos;
    }

    protected string _value;
    public string Value
    {
        get => _value;
    }

    public abstract List<Type> RequiredChildren
    {
        get;
    }

    protected List<IToken> _currentChildren = new List<IToken>(){};
    public List<IToken> CurrentChildren
    {
        get => _currentChildren;
        set => _currentChildren = value;
    }

    public Node(int lineNum, int linePos, string value)
    {
        _lineNum = lineNum;
        _linePos = linePos;
        _value = value;
    }
    
    public bool Equals(IToken? other)
    {
        // We don't want it to check properties of null
        if (other is null) return false;

        // Ensure that all values are equal to check equality
        return  (CurrentChildren == other.CurrentChildren) && 
                (Value == other.Value) && 
                (LineNum == other.LineNum) && 
                (LinePos == other.LinePos);
    }

    /// <summary>
    /// This method returns true if the node has all of the children it needs to execute.
    /// <example>
    /// For example:
    /// <code>
    /// uncompletedNode.CompletedChildren();
    /// </code>
    /// will return false.
    /// </example>
    /// </summary>
    public bool CompletedChildren()
    {
        return (RequiredChildren.Count == CurrentChildren.Count);
    }

    /// <summary>
    /// This method allows us to add children to uncompleted nodes.
    /// <example>
    /// For example:
    /// <code>
    /// uncompletedNode.AppendChild(child);
    /// </code>
    /// will append the child to the uncompletedNode.
    /// </example>
    /// </summary>
    public void AppendChild(Node child)
    {
        
    }
}