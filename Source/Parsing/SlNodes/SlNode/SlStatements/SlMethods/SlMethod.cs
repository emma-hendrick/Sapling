namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlMethod: SlNode
{
    public SlMethod(Logger logger, SlScope scope): base(logger, scope)
    {
    }
    
    // Create a list of statments which this method will execute when it is called
    List<SlStatement> statements = new List<SlStatement>();

    public void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method)
    {
        Logger.Add("Generating code for SlMethod");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);

        bool hasReturn = statements.Any(statement => statement is SlReturn);
        
        foreach (SlStatement statement in statements)
        {
            statement.GenerateCode(module, builder, entry);
        }

        Logger.Add($"Method contains return: {hasReturn}");

        if (!hasReturn)
        {
            // Return 0 to indicate a successful run
            Logger.Add("Adding terminator for current method (it did not have its own)");
            Logger.DecreaseIndent();
            LLVMSharp.LLVM.BuildRet(builder, LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), 0, true));
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