namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlMethodCall: SlStatement
{
    /// <summary>
    /// Generate code for a LLVM method call
    /// <example>
    public override void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope)
    {
        // TODO
        logger.Add("Generating code for SlMethodCall");
    }
}