namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid expression within the sapling programming language
/// </summary>
internal abstract class SlExpression: SlNode, IShuntingYardable
{
    /// <summary>
    /// The type of the expression
    /// </summary>
    private string _exType;
    
    /// <summary>
    /// The type of the expression
    /// </summary>
    public string ExType => Constants.EquivalentParsingTypes[_exType];

    /// <summary>
    /// Construct a new SlExpression
    /// </summary>
    public SlExpression(Logger logger, LLVMSharp.LLVMModuleRef module, string exType, SlScope scope): base(logger, module, scope)
    {
        _exType = exType;
    }

    /// <summary>
    /// Generate a value for an SlExpression
    /// </summary>
    public abstract LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry);
}