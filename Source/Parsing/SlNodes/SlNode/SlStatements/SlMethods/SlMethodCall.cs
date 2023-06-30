namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid method call statement within the sapling programming language
/// </summary>
internal class SlMethodCall: SlStatement
{
    /// <summary>
    /// Construct a new SlMethodCall
    /// </summary>
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