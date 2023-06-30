namespace Sapling.Nodes;
using Sapling.Logging;
using System.Linq;

/// <summary>
/// A valid method within the sapling programming language
/// </summary>
internal class SlMethod: SlNode
{
    /// <summary>
    /// The return type of this method
    /// </summary>
    private string _retType;

    /// <summary>
    /// A list of statments which this method will execute when it is called
    /// </summary>
    private List<SlStatement> _statements = new List<SlStatement>();

    /// <summary>
    /// Construct a new SlMethod
    /// </summary>
    public SlMethod(Logger logger, SlScope scope, string type): base(logger, scope)
    {
        _retType = type;
    }

    /// <summary>
    /// Generate the code for an SlMethod
    /// </summary>
    public void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry, LLVMSharp.LLVMValueRef method)
    {
        Logger.Add("Generating code for SlMethod");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);

        // Get the type of all returns in the function
        List<string> returnTypes = _statements.FindAll(statement => statement is SlReturn).Select(statement => ((SlReturn)statement).ExType).ToList();

        // Iterate through those types and throw an error if they don't match the methods return type
        foreach (string retType in returnTypes) if(Constants.EquivalentTypes[retType] != Constants.EquivalentTypes[_retType]) throw new Exception($"Return type {Constants.EquivalentTypes[retType]} does not match expected type {Constants.EquivalentTypes[_retType]}");

        // If the method does not contain a return log that and then throw an error
        Logger.Add($"Method contains return: {returnTypes.Count != 0}");
        if (returnTypes.Count == 0) throw new Exception("Methods must contain an explicit return statement");
        
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