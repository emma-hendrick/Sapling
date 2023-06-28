namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignMethod: SlStatement
{
    private string _methodIdentifier;
    private SlMethod _method;

    public SlAssignMethod(Logger logger, string identifier, SlMethod method, SlScope scope): base(logger, scope)
    {
        _methodIdentifier = identifier;
        _method = method;
    }

    /// <summary>
    /// Generate code for a LLVM method
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry)
    {
        Logger.Add("Generating code for SlAssignMethod");

        // Add new method to the module
        LLVMSharp.LLVMTypeRef[] method_param_types = { };
        LLVMSharp.LLVMTypeRef method_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), method_param_types, false);
        LLVMSharp.LLVMValueRef new_method = LLVMSharp.LLVM.AddFunction(module, _methodIdentifier, method_fn_type);

        // Entry/Exit point for the method
        Logger.IncreaseIndent();
        Logger.Add($"Adding Basic Block: \"{_methodIdentifier}_entry\"");
        LLVMSharp.LLVMBasicBlockRef method_entry = LLVMSharp.LLVM.AppendBasicBlock(new_method, $"{_methodIdentifier}_entry");

        // Generate the code for the method
        _method.GenerateCode(module, builder, method_entry, new_method);

        // We will then position the builder back at the end of this method
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, self_entry);
    }
}