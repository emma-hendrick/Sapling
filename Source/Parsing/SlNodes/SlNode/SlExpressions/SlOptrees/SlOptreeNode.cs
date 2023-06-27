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

    public override LLVMSharp.LLVMValueRef GenerateValue(Logger logger, LLVMSharp.LLVMBuilderRef builder, SlScope scope)
    {
        LLVMSharp.LLVMValueRef result;

        switch (_op.OpType)
        {
            case "+":
                result = LLVMSharp.LLVM.BuildAdd(builder, _l.GenerateValue(logger, builder, scope), _r.GenerateValue(logger, builder, scope), $"result_{_num.ToString()}");
                break;
            case "-":
                result = LLVMSharp.LLVM.BuildSub(builder, _l.GenerateValue(logger, builder, scope), _r.GenerateValue(logger, builder, scope), $"result_{_num.ToString()}");
                break;
            case "*":
                result = LLVMSharp.LLVM.BuildMul(builder, _l.GenerateValue(logger, builder, scope), _r.GenerateValue(logger, builder, scope), $"result_{_num.ToString()}");
                break;
            default:
                throw new Exception($"Unexpected operator type {_op.OpType}");
        }

        return result;
    }
}