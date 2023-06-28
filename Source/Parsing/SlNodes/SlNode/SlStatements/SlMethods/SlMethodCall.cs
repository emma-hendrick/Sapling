namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlMethodCall: SlStatement
{
    public SlMethodCall(Logger logger, SlScope scope): base(logger, scope)
    {
    }

    /// <summary>
    /// Generate code for a LLVM method call
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry)
    {
        // TODO
        Logger.Add("Generating code for SlMethodCall");
    }
}