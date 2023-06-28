namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlParsedOptree: SlExpression
{
    private SlOptree _optree;
    public SlParsedOptree(SlOptree optree): base(optree.GetReturnType())
    {
        _optree = optree;
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope, LLVMSharp.LLVMModuleRef module)
    {
        return _optree.GenerateValue(logger, builder, scope, module);
    }
}