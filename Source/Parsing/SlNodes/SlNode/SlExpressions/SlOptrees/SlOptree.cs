namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOptree: SlExpression
{
    private SlExpression? _root;
    private static Dictionary <string, int> _precedence = new Dictionary<string, int>
        {
            // Precedence for arithmetic operators
            {"+", 32}, 
            {"-", 31}, 
            {"*", 34}, 
            {"/", 33},

            // Precedence for boolean operators
            {"&&", 23},
            {"||", 22},
            {"^", 21},

            // Precedence for comparison operators
            {"==", 11},
            {"!=", 11},
            {"<", 12},
            {"<=", 12},
            {">", 12},
            {">=", 12},
        };

    public SlOptree(Logger logger, List<SlExpression> expressions, List<SlOperator> operators, SlExpression root, SlScope scope): base(logger, root.ExType, scope)
    {
        _root = root;
    }

    public string GetReturnType()
    {
        if (_root is null) throw new Exception("Attempting to get type of unparsed optree");
        return _root.ExType;
    }

    public static SlExpression ParseToNodes(Logger logger, List<SlExpression> expressions, List<SlOperator> operators, SlScope scope)
    {
        logger.Add("Parsing OpTree - Shunting Yard Time!");
        logger.IncreaseIndent();

        // Data structures we will need
        Stack<SlOperator> operatorsInProcessing = new Stack<SlOperator>();
        Queue<IShuntingYardable> postfixQueue = new Queue<IShuntingYardable>();

        // Algorithm Time!
        for (int i = 0; i < expressions.Count + operators.Count; i ++)
        {
            // We'll use this to know which expression or operator to access
            int key = i / 2;

            // If it's an expression
            if (i % 2 == 0 )
            {
                logger.Add($"Enqueueing expression of type: {expressions[key].ExType}");
                postfixQueue.Enqueue(expressions[key]);
            }
            else // If it's an operator
            {
                SlOperator currentOperator = operators[key];

                while (operatorsInProcessing.Count > 0 && _precedence[currentOperator.OpType] < _precedence[operatorsInProcessing.Peek().OpType])
                {
                    SlOperator op = operatorsInProcessing.Pop();

                    logger.Add($"Enqueueing operator of type: {op.OpType}");
                    postfixQueue.Enqueue(op);
                };
                
                logger.Add($"Adding operator of type {currentOperator.OpType} to stack");
                operatorsInProcessing.Push(currentOperator);
            }
        }

        logger.Add("Lists processed, adding remaining operators");

        while(operatorsInProcessing.Count > 0)
        {
            SlOperator op = operatorsInProcessing.Pop();

            logger.Add($"Enqueueing operator of type: {op.OpType}");
            postfixQueue.Enqueue(op);
        }

        // Done shunting those yards!
        logger.DecreaseIndent();

        // Time to parse the postfix queue!
        logger.Add("Parsing Postfix Queue!");
        logger.IncreaseIndent();
        Stack<SlExpression> postfixStack = new Stack<SlExpression>();

        while(postfixQueue.Count > 0)
        {
            IShuntingYardable item = postfixQueue.Dequeue();
            if(item is SlExpression) 
            {
                logger.Add($"Dequeueing expression of type: {((SlExpression)item).ExType}");
                postfixStack.Push((SlExpression)item);
            }
            else 
            {
                logger.Add($"Dequeueing operator of type: {((SlOperator)item).OpType}");
                SlExpression r = postfixStack.Pop();
                SlExpression l = postfixStack.Pop();
                postfixStack.Push(new SlOptreeNode(logger, (SlOperator)item, l, r, scope));
            }
        }

        // Done parsing that postfix!
        logger.DecreaseIndent();

        return postfixStack.Pop();
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        Logger.Add($"Generating a value for root of OpTree");
        if (_root is null) throw new Exception("Attempting to generate value from unparsed optree");
        return _root.GenerateValue(builder, module);
    }
}

internal static class SlOptreeFactory
{
    public static SlOptree CreateInstance(Logger logger, List<SlExpression> expressions, List<SlOperator> operators, SlScope scope)
    {
        SlExpression root = SlOptree.ParseToNodes(logger, expressions, operators, scope);
        return new SlOptree(logger, expressions, operators, root, scope);
    }
}