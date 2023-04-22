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
    
    public NumberOperator(int lineNum, int linePos, string value): base(lineNum, linePos, value)
    {
    }

    /// <summary>
    /// Evaluates the token
    /// <example>
    /// For example:
    /// <code>
    /// NumberOperator operator1 = new Operator(1, 1, "+");
    ///
    /// operator1.AddChild(new Integer(3));
    ///
    /// operator1.AddChild(new Integer(4));
    ///
    /// operator1.Evaluate();
    /// </code>
    /// will return 7.
    /// </example>
    /// </summary>
    public Number Evaluate()
    {
        if (RequiredChildren.Count != CurrentChildren.Count)
        {
            throw new Exception($"Cannot evaluate {Value} at line {LineNum} position {LinePos}, not all children are present.");
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

        return new Number(0, 0, result.ToString());
    }
}