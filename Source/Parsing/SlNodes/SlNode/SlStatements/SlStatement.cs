namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlStatement: SlNode
{
    public SlStatement(Logger logger, SlScope scope): base(logger, scope)
    {
    }

    public abstract void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry);
}