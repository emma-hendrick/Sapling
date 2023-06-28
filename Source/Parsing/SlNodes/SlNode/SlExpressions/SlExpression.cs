namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlExpression: SlNode, IShuntingYardable
{
    private string _exType;
    public string ExType => _exType;

    public SlExpression(Logger logger, string exType, SlScope scope): base(logger, scope)
    {
        _exType = exType;
    }

    public abstract LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module);
}