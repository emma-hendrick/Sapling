namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlClass: SlNode
{
    public SlClass(Logger logger, SlScope scope): base(logger, scope)
    {
    }

    // Create a list of statments which this method will execute when it is called
    List<SlStatement> statements = new List<SlStatement>();

    public void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method)
    {
        Logger.Add("Generating code for SlClass");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);
        
        foreach (SlStatement statement in statements)
        {
            statement.GenerateCode(module, builder, entry);
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