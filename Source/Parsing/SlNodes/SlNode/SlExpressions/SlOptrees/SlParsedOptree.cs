namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlParsedOptree: SlExpression
{
    private SlOptree _optree;
    public SlParsedOptree(Logger logger, SlOptree optree, SlScope scope): base(logger, optree.GetReturnType(), scope)
    {
        _optree = optree;
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        return _optree.GenerateValue(builder, module);
    }
}