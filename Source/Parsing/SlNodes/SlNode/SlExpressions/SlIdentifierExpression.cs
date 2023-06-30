namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid identifier expression within the sapling programming language
/// </summary>
internal class SlIdentifierExpression: SlExpression
{
    /// <summary>
    /// The properties identifier
    /// </summary>
    private string _identifier;

    /// <summary>
    /// Construct a new SlIdentifierExpression
    /// </summary>
    public SlIdentifierExpression(Logger logger, string identifier, SlScope scope): base(logger, GetIdentifierType(logger, identifier, scope), scope)
    {
        _identifier = identifier;
    }

    /// <summary>
    /// Get the type of the identifier
    /// </summary>
    private static string GetIdentifierType(Logger logger, string identifier, SlScope scope)
    {
        return scope.GetType(logger, identifier);
    }

    /// <summary>
    /// Generate the value of the identifier expression
    /// </summary>
    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        LLVMSharp.LLVMValueRef variable_alloc = Scope.Get(Logger, _identifier);
        return LLVMSharp.LLVM.BuildLoad(builder, variable_alloc, "loadedValue");
    }
}