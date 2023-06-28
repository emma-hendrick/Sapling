namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOptreeNode: SlExpression
{
    SlOperator _op;
    SlExpression _l;
    SlExpression _r;
    int _num;

    public SlOptreeNode(SlOperator op, SlExpression l, SlExpression r, int num): base(GetReturnType(op, l, r))
    {
        _op = op;
        _l = l;
        _r = r;
        _num = num;
    }

    private static string GetReturnType(SlOperator op, SlExpression l, SlExpression r)
    {
        return op.GetReturnType(l.ExType, r.ExType);
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope, LLVMSharp.LLVMModuleRef module)
    {
        logger.Add($"Generating a value for {_l.ExType} {_op.OpType} {_l.ExType}");
        Func<LLVMSharp.LLVMBuilderRef, LLVMSharp.LLVMValueRef, LLVMSharp.LLVMValueRef, string, LLVMSharp.LLVMValueRef> func;

        switch (_op.OpType)
        {
            case "+":
                func = LLVMSharp.LLVM.BuildAdd;
                break;
            case "-":
                func = LLVMSharp.LLVM.BuildSub;
                break;
            case "*":
                func = LLVMSharp.LLVM.BuildMul;
                break;
            case "/":
                func = LLVMSharp.LLVM.BuildUDiv;
                break;
            default:
                throw new Exception($"Unexpected operator type {_op.OpType}");
        }

        return func(builder, _l.GenerateValue(logger, builder, scope, module), _r.GenerateValue(logger, builder, scope, module), $"result_{_num.ToString()}");
    }
}