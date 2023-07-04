namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A statement within the sapling programming langauge
/// </summary>
internal abstract class SlStatement: SlNode
{
    /// <summary>
    /// Construct a new SlStatement
    /// </summary>
    public SlStatement(Logger logger, LLVMSharp.LLVMModuleRef module, SlScope scope): base(logger, module, scope)
    {
    }

    /// <summary>
    /// Generate the code for an SlStatement
    /// </summary>
    public abstract void GenerateCode(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry);
}