namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid return statement within the sapling programming language
/// </summary>
internal class SlReturn: SlStatement
{       
    /// <summary>
    /// The expression to return
    /// </summary>
    private SlExpression _return;

    /// <summary>
    /// The type of the expression to return
    /// </summary>
    public string ExType => _return.ExType;

    /// <summary>
    /// Construct a new SlReturn
    /// </summary>
    public SlReturn(Logger logger, SlExpression r, SlScope scope): base(logger, scope)
    {
        _return = r;
    }

    /// <summary>
    /// Generate code for a LLVM return statement
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