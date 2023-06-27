namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOptree: SlExpression
{
    private SlExpression? _root;
    private Logger _logger;
    private static Dictionary <string, int> _precedence = new Dictionary<string, int>
        {
            {"+", 2}, 
            {"-", 1}, 
            {"*", 4}, 
            {"/", 3}
        };

    public SlOptree(Logger logger, List<SlExpression> expressions, List<SlOperator> operators, SlExpression root): base(root.ExType)
    {
        _logger = logger;
        _root = root;
    }

    public string GetReturnType()
    {
        if (_root is null) throw new Exception("Attempting to get type of unparsed optree");
        return _root.ExType;
    }

    public static SlExpression ParseToNodes(Logger logger, List<SlExpression> expressions, List<SlOperator> operators)
    {
        logger.Add("Parsing OpTree");

        // Here we will start putting the expression in the right order
        Stack<SlExpression> expressionStack = new Stack<SlExpression>();
        Stack<SlOperator> operatorStack = new Stack<SlOperator>();

        // For each token we will either add it to the expression stack if it is an expression, or we will re apply the precedence to the lists
        for (int i = 0; i < expressions.Count; i++)
        {
            if (i % 2 == 0)
            {
                expressionStack.Push(expressions[i]);
            }
            else
            {
                ApplyPrecedence(logger, operators[i / 2], expressionStack, operatorStack);
            }
        }

        // Convert expressions and operators to a set of nodes
        return BuildTree(logger, expressionStack, operatorStack);
    }

    private static void ApplyPrecedence(Logger logger, SlOperator op, Stack<SlExpression> expressionStack, Stack<SlOperator> operatorStack)
    {
        while (operatorStack.Count > 0 && _precedence[op.OpType] <= _precedence[operatorStack.Peek().OpType])
        {
            SlOperator currentOperator = operatorStack.Pop();
            SlExpression left = expressionStack.Pop();
            SlExpression right = expressionStack.Pop();

            SlOptreeNode node = new SlOptreeNode(currentOperator, left, right, 0);
            expressionStack.Append(node);
        }

        operatorStack.Append(op);
    }

    private static SlExpression BuildTree(Logger logger, Stack<SlExpression> expressionStack, Stack<SlOperator> operatorStack)
    {
        logger.Add("Building OpTree");

        // Build a tree from the expression and operator stacks
        while (operatorStack.Count > 0 && expressionStack.Count >= 2)
        {
            SlOperator currentOperator = operatorStack.Pop();
            SlExpression left = expressionStack.Pop();
            SlExpression right = expressionStack.Pop();

            SlOptreeNode node = new SlOptreeNode(currentOperator, left, right, 0);
            expressionStack.Append(node);
        }

        return expressionStack.Pop();
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope)
    {
        if (_root is null) throw new Exception("Attempting to generate value from unparsed optree");
        return _root.GenerateValue(logger, builder, scope);
    }
}


internal static class SlOptreeFactory
{
    public static SlOptree CreateInstance(Logger logger, List<SlExpression> expressions, List<SlOperator> operators)
    {
        SlExpression root = SlOptree.ParseToNodes(logger, expressions, operators);
        return new SlOptree(logger, expressions, operators, root);
    }
}