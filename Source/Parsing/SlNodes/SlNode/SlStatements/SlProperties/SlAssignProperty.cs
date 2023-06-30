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
        Console.WriteLine(_expression.ExType);

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