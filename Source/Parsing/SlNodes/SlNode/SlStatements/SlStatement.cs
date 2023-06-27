namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlStatement: SlNode
{
    public abstract void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope);
}