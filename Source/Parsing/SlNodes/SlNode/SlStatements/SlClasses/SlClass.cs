namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlClass: SlNode
{
    // Create a list of statments which this method will execute when it is called
    List<SlStatement> statements = new List<SlStatement>();

    public void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method, SlScope scope)
    {
        logger.Add("Generating code for SlClass");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);
        
        foreach (SlStatement statement in statements)
        {
            statement.GenerateCode(logger, module, builder, entry, scope);
        }
    }

    /// <summary>
    /// Append a node as a child of this.
    /// <example>
    public void Append(SlStatement node)
    {
        statements.Append(node);
    }
}