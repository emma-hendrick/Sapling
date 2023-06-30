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
    public SlStatement(Logger logger, SlScope scope): base(logger, scope)
    {
    }

    /// <summary>
    /// Generate the code for an SlStatement
    /// </summary>
    public abstract void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry);
}