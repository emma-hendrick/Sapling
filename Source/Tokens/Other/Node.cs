namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Node</c> represents a valid ASTNode (such as Float, NumberOperator, or many others) within the sapling programming language.
/// </summary>
internal abstract class Node: IToken
{
    protected int _startIndex;
    public int startIndex
    {
        get => _startIndex;
    }

    protected int _endIndex;
    public int endIndex
    {
        get => _endIndex;
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

    public Node(int startIndex, int endIndex, string value)
    {
        _startIndex = startIndex;
        _endIndex = endIndex;
        _value = value;
    }
    
    public bool Equals(IToken? other)
    {
        // We don't want it to check properties of null
        if (other is null) return false;

        // Ensure that all values are equal to check equality
        return  (CurrentChildren == other.CurrentChildren) && 
                (Value == other.Value) && 
                (startIndex == other.startIndex) && 
                (endIndex == other.endIndex);
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
    /// uncompletedNode.AddChild(child);
    /// </code>
    /// will add the child to the uncompletedNode.
    /// </example>
    /// </summary>
    public void AddChild(Node child)
    {
        // If we already have all of our children, throw an exception
        if (CompletedChildren()) throw new Exception($"Cannot add new child with value {child.Value} on line {child.startIndex} at char {child.endIndex}, all children completed.");

        // See which types we still need
        List<Type> neededTypes = RequiredChildren;
        foreach (Node node in CurrentChildren)
        {
            foreach (Type T in neededTypes.ToList())
            {
                if (TypeEquivalence(T, node.GetType()))
                {
                    neededTypes.Remove(T);
                    break;
                }
            }
        }

        // Iterate through the needed types
        foreach (Type T in neededTypes.ToList())
        {
            if (TypeEquivalence(T, child.GetType()))
            {
                _currentChildren.Add(child);
                return;
            }
        }

        // If we did not find the type in the last loop, then we know it is the wrong type
        throw new Exception($"Cannot add new child with type {child.GetType()} on line {child.startIndex} at char {child.endIndex}. Types needed are {string.Join(", ", neededTypes)}.");

    }
    

    /// <summary>
    /// This method allows us to see whether a type is equivalent to another
    /// </summary>
    private bool TypeEquivalence(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
            || potentialDescendant == potentialBase;
    }
}