namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlReturn: SlStatement
{   
    private SlExpression _return;

    public SlReturn(Logger logger, SlExpression r, SlScope scope): base(logger, scope)
    {
        _return = r;
    }

    /// <summary>
    /// Generate code for a LLVM method call
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry)
    {
        Logger.Add("Generating code for SlReturn");

        LLVMSharp.LLVMValueRef expression = _return.GenerateValue(builder, module);
        
        Logger.Add("Adding terminator for current method");
        Logger.DecreaseIndent();
        LLVMSharp.LLVM.BuildRet(builder, expression);
    }
}