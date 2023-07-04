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
    public SlReturn(Logger logger, LLVMSharp.LLVMModuleRef module, SlExpression r, SlScope scope): base(logger, module, scope)
    {
        _return = r;
    }

    /// <summary>
    /// Generate code for a LLVM return statement
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        Logger.Add("Generating code for SlReturn");

        LLVMSharp.LLVMValueRef expression = _return.GenerateValue(builder, entry);
        
        Logger.Add("Adding terminator for current method");
        Logger.DecreaseIndent();
        LLVMSharp.LLVM.BuildRet(builder, expression);
    }
}