namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlExpression: SlNode
{
    private string _exType;
    public string ExType => _exType;

    public SlExpression(string exType)
    {
        _exType = exType;
    }

    public abstract LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope);
}