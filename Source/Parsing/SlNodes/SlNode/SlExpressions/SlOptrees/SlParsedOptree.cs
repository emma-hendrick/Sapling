namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// An optree which has been successfully parsed into nodes
/// </summary>
internal class SlParsedOptree: SlExpression
{
    /// <summary>
    /// The optree
    /// </summary>
    private SlOptree _optree;
    
    /// <summary>
    /// Construct a new SlParsedOptree
    /// </summary>
    public SlParsedOptree(Logger logger, LLVMSharp.LLVMModuleRef module, SlOptree optree, SlScope scope): base(logger, module, optree.GetReturnType(), scope)
    {
        _optree = optree;
    }

    /// <summary>
    /// Generate the value of this optree
    /// </summary>
    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        return _optree.GenerateValue(builder, entry);
    }
}