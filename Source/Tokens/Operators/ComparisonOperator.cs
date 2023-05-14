namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>ComparisonOperator</c> represents a valid comparison operator (such <, ==, >=, !=) within the sapling programming language.
/// </summary>
internal class ComparisonOperator: Node, IOperator
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){
            typeof(Number),
            typeof(Number)
        };
    }
    
    /// <summary>
    /// This construsts a new instance of a comparison operator.
    /// <example>
    /// For example:
    /// <code>
    /// ComparisonOperator LT = new ComparisonOperator(0, 0, "<");
    ///
    /// Integer a = new Integer(0, 0, "1");
    ///
    /// Integer b = new Integer(0, 0, "2");
    ///
    /// LT.AppendChild(a);
    ///
    /// LT.AppendChild(b);
    ///
    /// LT.evaluate();
    /// </code>
    /// will return "True".
    /// </example>
    /// </summary>
    public ComparisonOperator(int startIndex, int endIndex, string value): base(startIndex, endIndex, value)
    {
    }

    /// <summary>
    /// Evaluates the token
    /// <example>
    /// For example:
    /// <code>
    /// ComparisonOperator LT = new ComparisonOperator(0, 0, "<");
    ///
    /// LT.AppendChild(new Integer(0, 0, "1"));
    ///
    /// LT.AppendChild(new Integer(0, 0, "2"));
    ///
    /// LT.evaluate();
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

        float num1 = float.Parse(CurrentChildren[0].Value);
        float num2 = float.Parse(CurrentChildren[1].Value);

        bool result;

        switch (Value)
        {
            case ">":
                result = num1 > num2;
            break;
            case "<":
                result = num1 < num2;
            break;
            case ">=":
                result = num1 >= num2;
            break;
            case "<=":
                result = num1 <= num2;
            break;
            case "==":
                result = num1 == num2;
            break;
            case "!=":
                result = num1 != num2;
            break;
            default:
                throw new Exception($"Invalid operator type {Value}");
        }

        // Otherwise, return it as a float
        return new Boolean(0, 0, result.ToString());
    }
}