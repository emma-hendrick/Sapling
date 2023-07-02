namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid ternary expression within the sapling programming language
/// </summary>
internal class SlTernaryExpression: SlExpression
{
    /// <summary>
    /// The condition of the ternary expression
    /// </summary>
    private SlExpression _cond;

    /// <summary>
    /// The value of the ternary expression if true
    /// </summary>
    private SlExpression _valIfTrue;

    /// <summary>
    /// The value of the ternary expression if false
    /// </summary>
    private SlExpression _valIfFalse;

    /// <summary>
    /// Construct a new SlTernaryExpression
    /// </summary>
    public SlTernaryExpression(Logger logger, SlExpression cond, SlExpression valIfTrue, SlExpression valIfFalse, SlScope scope): base(logger, GetReturnType(logger, cond, valIfTrue, valIfFalse), scope)
    {
        _cond = cond;
        _valIfTrue = valIfTrue;
        _valIfFalse = valIfFalse;
    }

    /// <summary>
    /// Get the return type of the ternary
    /// </summary>
    private static string GetReturnType(Logger logger, SlExpression cond, SlExpression valIfTrue, SlExpression valIfFalse)
    {
        if (cond.ExType != "bool") throw new Exception("The condition for a ternary operator in the sapling programming language must be a Boolean.");
        if (valIfTrue.ExType != valIfFalse.ExType) throw new Exception("The types of the valIfTrue and valIfFalse within a ternary operator must be the same.");
        return valIfTrue.ExType;
    }

    /// <summary>
    /// Generate the value of the ternary expression
    /// </summary>
    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBasicBlockRef entry)
    {
        // Create a function to perform the ternary operation
        LLVMSharp.LLVMTypeRef retType = Scope.FindType(Logger, _valIfTrue.ExType);
        LLVMSharp.LLVMTypeRef[] paramTypes = {Scope.FindType(Logger, "bool")};
        LLVMSharp.LLVMTypeRef functionType = LLVMSharp.LLVM.FunctionType(retType, paramTypes, false);
        LLVMSharp.LLVMValueRef function = LLVMSharp.LLVM.AddFunction(module, "ternary", functionType);

        // So... many... building blocks
        LLVMSharp.LLVMBasicBlockRef funcEntry = LLVMSharp.LLVM.AppendBasicBlock(function, "ternary_entry");
        LLVMSharp.LLVMBasicBlockRef trueBranch = LLVMSharp.LLVM.AppendBasicBlock(function, "true");
        LLVMSharp.LLVMBasicBlockRef falseBranch = LLVMSharp.LLVM.AppendBasicBlock(function, "false");
        LLVMSharp.LLVMBasicBlockRef mergeBranch = LLVMSharp.LLVM.AppendBasicBlock(function, "merge");
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, funcEntry);

        // Branch to true or false basic block based on comparison result
        LLVMSharp.LLVM.BuildCondBr(builder, LLVMSharp.LLVM.GetParam(function, 0), trueBranch, falseBranch);

        // Set builder to the true basic block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, trueBranch);
        LLVMSharp.LLVM.BuildBr(builder, mergeBranch);

        // Set builder to the false basic block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, falseBranch);
        LLVMSharp.LLVM.BuildBr(builder, mergeBranch);

        // Choose between valIfFalse and valIfTrue based on the condition
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, mergeBranch);
        LLVMSharp.LLVMValueRef phiNode = LLVMSharp.LLVM.BuildPhi(builder, Scope.FindType(Logger, GetReturnType(Logger, _cond, _valIfTrue, _valIfFalse)), "phi_node");
        LLVMSharp.LLVMValueRef[] incomingValues = new[] {_valIfTrue.GenerateValue(builder, module, entry), _valIfFalse.GenerateValue(builder, module, entry)};
        LLVMSharp.LLVMBasicBlockRef[] incomingBlocks = new[] {trueBranch, falseBranch};

        // Do the comparison and return the phi node which did the comparison
        LLVMSharp.LLVM.AddIncoming(phiNode, incomingValues, incomingBlocks, 2);
        LLVMSharp.LLVM.BuildRet(builder, phiNode);

        // Return the builder to its original position
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, entry);

        // Call the ternary function
        LLVMSharp.LLVMValueRef[] args = new LLVMSharp.LLVMValueRef[] { _cond.GenerateValue(builder, module, entry) };
        LLVMSharp.LLVMValueRef result = LLVMSharp.LLVM.BuildCall(builder, function, args, "ternary_result");
        return result;
    }
}