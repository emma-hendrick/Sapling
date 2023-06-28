namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlIdentifierExpression: SlExpression
{
    string _identifier;

    public SlIdentifierExpression(Logger logger, string identifier, SlScope scope): base(logger, GetIdentifierType(logger, identifier, scope), scope)
    {
        _identifier = identifier;
    }

    private static string GetIdentifierType(Logger logger, string identifier, SlScope scope)
    {
        return scope.GetType(logger, identifier);
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        LLVMSharp.LLVMValueRef variable_alloc = Scope.Get(Logger, _identifier);
        return LLVMSharp.LLVM.BuildLoad(builder, variable_alloc, "loadedValue");
    }
}