namespace Sapling.Tokens;
using Sapling.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>NumberOperator</c> represents a valid numerical operator (such as + - * or /) within the sapling programming language.
/// </summary>
internal class NumberOperator: Node, IOperator
{
    public override List<Type> RequiredChildren
    {
        get => new List<Type>(){
            typeof(Number),
            typeof(Number)
        };
    }
    
    /// <summary>
    /// This construsts a new instance of a number operator.
    /// <example>
    /// For example:
    /// <code>
    /// NumberOperator multiply = new NumberOperator(0, 0, "*");
    ///
    /// Float a = new Float(0, 0, "3.1");
    ///
    /// Float b = new Float(0, 0, "2.6");
    ///
    /// multiply.AppendChild(a);
    ///
    /// multiply.AppendChild(b);
    ///
    /// multiply.evaluate();
    /// </code>
    /// will return "8.06".
    /// </example>
    /// </summary>
    public NumberOperator(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }

    /// <summary>
    /// Evaluates the token
    /// <example>
    /// For example:
    /// <code>
    /// NumberOperator operator1 = new Operator(0, 0, "+");
    ///
    /// operator1.AddChild(new Integer(0, 0, "3"));
    ///
    /// operator1.AddChild(new Integer(0, 0, "4"));
    ///
    /// operator1.Evaluate();
    /// </code>
    /// will return an integer with the value 7.
    /// </example>
    /// </summary>
    public Value Evaluate()
    {
        if (RequiredChildren.Count != CurrentChildren.Count)
        {
            throw new Exception($"Cannot evaluate \"{Value}\" at line {LineNum} position {LinePos}, not all children are present.");
        }

        int num1 = int.Parse(CurrentChildren[0].Value);
        int num2 = int.Parse(CurrentChildren[1].Value);

        int result;

        switch (Value)
        {
            case "+":
                result = num1 + num2;
            break;
            case "-":
                result = num1 - num2;
            break;
            case "*":
                result = num1 * num2;
            break;
            case "/":
                result = num1 / num2;
            break;
            default:
                throw new Exception($"Invalid operator type {Value}");
        }

        // If both children are integers (and the operator is not /) return the result as an int
        if (CurrentChildren[0].GetType() == typeof(Integer) && CurrentChildren[1].GetType() == typeof(Integer) && Value != "/")
        {
            return new Integer(0, 0, result.ToString());
        }

        // Otherwise, return it as a float
        return new Float(0, 0, result.ToString());
    }
}