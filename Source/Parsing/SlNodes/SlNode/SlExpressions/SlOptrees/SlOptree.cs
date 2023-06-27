namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOptree
{
    private List<SlExpression> _expressions;
    private List<SlOperator> _operators;
    private SlOptreeNode? _root;

    public SlOptree(List<SlExpression> expressions, List<SlOperator> operators)
    {
        _expressions = expressions;
        _operators = operators;
    }

    public string GetReturnType()
    {
        if (_root is null) throw new Exception("Attempting to get type of unparsed optree");
        return _root.ExType;
    }

    public void ParseToNodes(Logger logger)
    {
        logger.Add("Parsing OpTree");
        // Convert expressions and operators to a set of nodes

        
    }

    public LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope)
    {
        if (_root is null) throw new Exception("Attempting to generate value from unparsed optree");
        return _root.GenerateValue(logger, builder, scope);
    }
}