namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlIdentifierExpression: SlExpression
{
    string _identifier;

    public SlIdentifierExpression(string identifier): base(GetIdentifierType(identifier))
    {
        _identifier = identifier;
    }

    private static string GetIdentifierType(string identifier)
    {
        // TODO - Get the type of the identifier from the scope
        return "int";
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope)
    {
        // TODO - Get value from scope
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int1Type(), 0, false);
    }
}