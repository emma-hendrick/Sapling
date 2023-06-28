namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlAssignClass: SlStatement
{
    private string _classIdentifier;
    private SlClass _slClass;

    public SlAssignClass(Logger logger, string identifier, SlClass slClass, SlScope scope): base(logger, scope)
    {
        _classIdentifier = identifier;
        _slClass = slClass;
    }
    
    /// <summary>
    /// Generate code for a LLVM class
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef self_entry)
    {
        // TODO
        Logger.Add("Generating code for SlAssignClass");
    }
}