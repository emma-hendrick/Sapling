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
        _type = Constants.EquivalentParsingTypes[type];
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

        if (_expression.ExType != _type) throw new Exception($"SlAssignProperty type mismatch. Expected {_type}, got {_expression.ExType}");

        // Get the llvm type of the user provided type from the scope
        LLVMSharp.LLVMTypeRef type = Scope.FindType(Logger, _type);

        // Allocate space for the variable and store it
        LLVMSharp.LLVMValueRef variable_alloc = LLVMSharp.LLVM.BuildAlloca(builder, type, _identifier);
        LLVMSharp.LLVMValueRef variable_store = LLVMSharp.LLVM.BuildStore(builder, expression, variable_alloc);
        Scope.Add(Logger, _identifier, variable_alloc);
    }
}