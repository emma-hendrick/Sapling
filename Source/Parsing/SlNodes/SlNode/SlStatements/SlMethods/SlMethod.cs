namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlMethod: SlNode
{
    // Create a list of statments which this method will execute when it is called
    List<SlStatement> statements = new List<SlStatement>();

    public void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method, SlScope scope)
    {
        logger.Add("Generating code for SlMethod");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);

        bool hasReturn = statements.Any(statement => statement is SlReturn);
        
        foreach (SlStatement statement in statements)
        {
            statement.GenerateCode(logger, module, builder, entry, scope);
        }

        logger.Add($"Method contains return: {hasReturn}");

        if (!hasReturn)
        {
            // Return 0 to indicate a successful run
            logger.Add("Adding terminator for current method (it did not have its own)");
            logger.DecreaseIndent();
            LLVMSharp.LLVM.BuildRet(builder, LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), 0, false));
        }
    }

    /// <summary>
    /// Add a node as a child of this.
    /// <example>
    public void Add(SlStatement node)
    {
        statements.Add(node);
    }
}