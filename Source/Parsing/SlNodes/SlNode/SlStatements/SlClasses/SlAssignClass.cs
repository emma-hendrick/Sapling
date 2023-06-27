namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignClass: SlStatement
{
    private string _classIdentifier;
    private SlClass _slClass;

    public SlAssignClass(string identifier, SlClass slClass)
    {
        _classIdentifier = identifier;
        _slClass = slClass;
    }
    
    /// <summary>
    /// Generate code for a LLVM class
    /// <example>
    public override void GenerateCode(Logger logger, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry, SlScope scope)
    {
        // TODO
        logger.Add("Generating code for SlAssignClass");
    }
}