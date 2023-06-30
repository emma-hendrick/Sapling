namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid assign property statement within the sapling programming language
/// </summary>
internal class SlAssignProperty: SlStatement
{    
    /// <summary>
    /// The properties type
    /// </summary>
    private string _type;
    
    /// <summary>
    /// The properties identifier
    /// </summary>
    private string _identifier;
    
    /// <summary>
    /// The properties value
    /// </summary>
    private SlExpression _expression;

    /// <summary>
    /// Construct a new SlAssignProperty
    /// </summary>
    public SlAssignProperty(Logger logger, string type, string identifier, SlExpression expression, SlScope scope): base(logger, scope)
    {
        _type = type;
        _identifier = identifier;
        _expression = expression;
        Scope.AddType(Logger, _identifier, type);
    }

    /// <summary>
    /// Generate code for an sl property assignment
    /// </summary>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        Logger.Add("Generating code for SlAssignProperty");
        LLVMSharp.LLVMValueRef expression = _expression.GenerateValue(builder, module, entry);

        // Get the llvm type of the user provided type from the scope
        LLVMSharp.LLVMTypeRef type = Scope.FindType(Logger, _type);

        // It is a string, and we need to get its length
        // TODO - maybe one day find a better solution to this jankiness
        if (type.Equals(LLVMSharp.LLVM.ArrayType(LLVMSharp.LLVM.Int8Type(), 0))) type = expression.TypeOf();

        LLVMSharp.LLVMValueRef variable_alloc = LLVMSharp.LLVM.BuildAlloca(builder, type, _identifier);
        LLVMSharp.LLVMValueRef variable_store = LLVMSharp.LLVM.BuildStore(builder, expression, variable_alloc);
        Scope.Add(Logger, _identifier, variable_alloc);
    }
}