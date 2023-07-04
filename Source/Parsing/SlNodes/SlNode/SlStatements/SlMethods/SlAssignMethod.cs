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
    private LLVMSharp.LLVMBasicBlockRef _methodEntry;
    private LLVMSharp.LLVMValueRef _newMethod;

    /// <summary>
    /// Construct a new SlAssignMethod
    /// </summary>
    public SlAssignMethod(Logger logger, LLVMSharp.LLVMModuleRef module, string identifier, SlMethod method, SlScope scope): base(logger, module, scope)
    {
        _methodIdentifier = identifier;
        _method = method;

        Initialize();
    }

    public void Initialize()
    {
        Logger.Add($"Initializing Method {_methodIdentifier}");

        // Add new method to the module
        LLVMSharp.LLVMTypeRef[] method_param_types = { };
        LLVMSharp.LLVMTypeRef method_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), method_param_types, false);
        _newMethod = LLVMSharp.LLVM.AddFunction(Module, _methodIdentifier, method_fn_type);

        // Entry/Exit point for the method
        Logger.Add($"Adding Basic Block: \"{_methodIdentifier}_entry\"");
        _methodEntry = LLVMSharp.LLVM.AppendBasicBlock(_newMethod, $"{_methodIdentifier}_entry");

        // Add function to the scope
        Scope.AddFunctionType(Logger, _methodIdentifier, method_fn_type);
        Scope.AddFunction(Logger, _methodIdentifier, _newMethod);
    }

    /// <summary>
    /// Generate code for a LLVM assign method statement
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        Logger.Add("Generating code for SlAssignMethod");
        Logger.IncreaseIndent();

        // Generate the code for the method
        _method.GenerateCode(Module, builder, _methodEntry, _newMethod);

        // We will then position the builder back at the end of this method
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);
    }
}