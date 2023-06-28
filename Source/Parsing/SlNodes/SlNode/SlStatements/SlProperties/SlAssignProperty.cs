namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignProperty: SlStatement
{
    private string _type;
    private string _identifier;
    private SlExpression _expression;

    public SlAssignProperty(Logger logger, string type, string identifier, SlExpression expression, SlScope scope): base(logger, scope)
    {
        _type = type;
        _identifier = identifier;
        _expression = expression;
        Scope.AddType(Logger, _identifier, type);
    }

    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry)
    {
        Logger.Add("Generating code for SlAssignProperty");
        LLVMSharp.LLVMValueRef expression = _expression.GenerateValue(builder, module);
        LLVMSharp.LLVMValueRef variable_alloc = LLVMSharp.LLVM.BuildAlloca(builder, Scope.FindType(Logger, _type), _identifier);
        LLVMSharp.LLVMValueRef variable_store = LLVMSharp.LLVM.BuildStore(builder, expression, variable_alloc);
        Scope.Add(Logger, _identifier, variable_alloc);
    }
}