namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlReturn: SlStatement
{   
    private SlExpression _return;

    public SlReturn(SlExpression r)
    {
        _return = r;
    }

    /// <summary>
    /// Generate code for a LLVM method call
    /// <example>
    public override void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope)
    {
        logger.Add("Generating code for SlReturn");

        LLVMSharp.LLVMValueRef expression = _return.GenerateValue(logger, builder, scope);
        LLVMSharp.LLVM.BuildRet(builder, expression);
    }
}