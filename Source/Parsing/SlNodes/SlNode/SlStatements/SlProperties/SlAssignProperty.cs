namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignProperty: SlStatement
{
    private string _type;
    private string _identifier;
    private SlExpression _expression;

    public SlAssignProperty(string type, string identifier, SlExpression expression)
    {
        _type = type;
        _identifier = identifier;
        _expression = expression;
    }

    public override void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope)
    {
        logger.Add("Generating code for SlAssignProperty");
        LLVMSharp.LLVMValueRef expression = _expression.GenerateValue(logger, builder, scope, module);
        LLVMSharp.LLVMValueRef variable_alloc = LLVMSharp.LLVM.BuildAlloca(builder, scope.FindType(logger, _type), _identifier);
        LLVMSharp.LLVMValueRef variable_store = LLVMSharp.LLVM.BuildStore(builder, expression, variable_alloc);
    }
}

        // // Define a literal value for success
        // LLVMSharp.LLVMValueRef success = LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), 0, false);

        // // Return success value to signal success 
        // LLVMSharp.LLVM.BuildRet(builder, success);