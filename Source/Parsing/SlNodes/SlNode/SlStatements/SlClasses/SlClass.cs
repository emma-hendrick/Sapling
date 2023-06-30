namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid class within the sapling programming language
/// </summary>
internal class SlClass: SlNode
{
    /// <summary>
    /// Construct a new SlClass
    /// </summary>
    public SlClass(Logger logger, SlScope scope): base(logger, scope)
    {
    }

    /// <summary>
    /// A list of statments which this method will execute when it is called
    /// </summary>
    private List<SlStatement> _statements = new List<SlStatement>();

    /// <summary>
    /// Generate the code for an SlClass
    /// </summary>
    public void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method)
    {
        Logger.Add("Generating code for SlClass");

        // Get the type of all returns in the function
        bool hasReturn = _statements.Any(statement => statement is SlReturn);

        // If the class does contain a return log that and then throw an error
        Logger.Add($"Class contains return: {hasReturn}");
        if (hasReturn) throw new Exception("Classes must not contain a return statement");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);
        
        // Generate the code for each statement in the class
        foreach (SlStatement statement in _statements)
        {
            statement.GenerateCode(module, builder, entry);
        }
    }

    /// <summary>
    /// Add a node as a child of this.
    /// <example>
    public void Add(SlStatement node)
    {
        _statements.Add(node);
    }
}