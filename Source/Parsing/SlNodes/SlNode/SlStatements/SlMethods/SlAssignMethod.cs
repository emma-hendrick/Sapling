namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid assign method statement within the sapling programming language
/// </summary>
internal class SlAssignMethod: SlStatement
{
    /// <summary>
    /// The identifier of the method
    /// </summary>
    private string _methodIdentifier;
    
    /// <summary>
    /// The method
    /// </summary>
    private SlMethod _method;

    /// <summary>
    /// Construct a new SlAssignMethod
    /// </summary>
    public SlAssignMethod(Logger logger, string identifier, SlMethod method, SlScope scope): base(logger, scope)
    {
        _methodIdentifier = identifier;
        _method = method;
    }

    /// <summary>
    /// Generate code for a LLVM assign method statement
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
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
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);
    }
}