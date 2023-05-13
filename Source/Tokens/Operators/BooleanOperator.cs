namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>BooleanOperator</c> represents a valid boolean operator (such as and, or, and xor) within the sapling programming language.
/// </summary>
internal class BooleanOperator: Node, IOperator
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){
            typeof(Boolean),
            typeof(Boolean)
        };
    }
    
    /// <summary>
    /// This construsts a new instance of a boolean operator.
    /// <example>
    /// For example:
    /// <code>
    /// BooleanOperator or = new BooleanOperator(0, 0, "or");
    ///
    /// Boolean a = new Boolean(0, 0, "True");
    ///
    /// Boolean b = new Boolean(0, 0, "False");
    ///
    /// or.AppendChild(a);
    ///
    /// or.AppendChild(b);
    ///
    /// or.evaluate();
    /// </code>
    /// will return "True".
    /// </example>
    /// </summary>
    public BooleanOperator(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }

    /// <summary>
    /// Evaluates the token
    /// <example>
    /// For example:
    /// <code>
    /// BooleanOperator or = new BooleanOperator(0, 0, "xor");
    ///
    /// or.AppendChild(new Boolean(0, 0, "True"));
    ///
    /// or.AppendChild(new Boolean(0, 0, "False"));
    ///
    /// or.evaluate();
    /// </code>
    /// will return a boolean with the value True.
    /// </example>
    /// </summary>
    public Value Evaluate()
    {
        if (RequiredChildren.Count != CurrentChildren.Count)
        {
            throw new Exception($"Cannot evaluate \"{Value}\" at line {startIndex} position {endIndex}, not all children are present.");
        }

        bool bool1 = bool.Parse(CurrentChildren[0].Value);
        bool bool2 = bool.Parse(CurrentChildren[1].Value);

        bool result;

        switch (Value)
        {
            case "&&":
                result = bool1 && bool2;
            break;
            case "||":
                result = bool1 || bool2;
            break;
            case "^":
                result = !(bool1 == bool2);
            break;
            default:
                throw new Exception($"Invalid operator type {Value}");
        }

        // Otherwise, return it as a float
        return new Boolean(0, 0, result.ToString());
    }
}