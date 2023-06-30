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
    public string ExType => Constants.EquivalentTypes[_exType];

    /// <summary>
    /// Construct a new SlExpression
    /// </summary>
    public SlExpression(Logger logger, string exType, SlScope scope): base(logger, scope)
    {
        _exType = exType;
    }

    /// <summary>
    /// Generate a value for an SlExpression
    /// </summary>
    public abstract LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBasicBlockRef entry);
}