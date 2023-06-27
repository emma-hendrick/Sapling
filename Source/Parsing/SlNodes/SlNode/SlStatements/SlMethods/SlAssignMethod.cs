namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignMethod: SlStatement
{
    private string _methodIdentifier;
    private SlMethod _method;

    public SlAssignMethod(string identifier, SlMethod method)
    {
        _methodIdentifier = identifier;
        _method = method;
    }

    /// <summary>
    /// Generate code for a LLVM method
    /// <example>
    public override void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope)
    {
        logger.Add("Generating code for SlAssignMethod");

        // Add sum to the module
        LLVMSharp.LLVMTypeRef[] method_param_types = { };
        LLVMSharp.LLVMTypeRef method_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), method_param_types, false);
        
        LLVMSharp.LLVMValueRef new_method = LLVMSharp.LLVM.AddFunction(module, _methodIdentifier, method_fn_type);

        // Entry/Exit point for the add function
        LLVMSharp.LLVMBasicBlockRef method_entry = LLVMSharp.LLVM.AppendBasicBlock(new_method, "entry");

        // Generate the code for the method
        _method.GenerateCode(logger, module, builder, method_entry, new_method, scope);

        // We will then position the builder back at the end of this method
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, self_entry);
    }
}