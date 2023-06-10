// namespace Sapling.Nodes;
// using Sapling.Tokens;
// using System;
// using System.Collections.Generic;

// /// <summary>
// /// Class <c>Node</c> represents a valid ASTNode (such as Expression, Statement, Assign, Return, Method, Property, Class) within the sapling programming language.
// /// </summary>
// internal abstract class Node
// {
//     public abstract List<Type> RequiredChildren
//     {
//         get;
//     }

//     protected List<Token> _currentChildren = new List<Token>(){};
//     public List<Token> CurrentChildren
//     {
//         get => _currentChildren;
//         set => _currentChildren = value;
//     }

//     /// <summary>
//     /// This method returns true if the syntax is valid.
//     /// <example>
//     /// For example:
//     /// <code>
//     /// invalidNode.CheckIfValid();
//     /// </code>
//     /// will return false.
//     /// </example>
//     /// </summary>
//     public bool CheckIfValid()
//     {
//     }

//     /// <summary>
//     /// This method allows us to add children to nodes.
//     /// <example>
//     /// For example:
//     /// <code>
//     /// node.AddChild(child);
//     /// </code>
//     /// will add the child to the node.
//     /// </example>
//     /// </summary>
//     public void AddChild(Node child)
//     {
//     }
// }