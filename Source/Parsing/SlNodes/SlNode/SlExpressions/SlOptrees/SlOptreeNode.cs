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

    public SlOptreeNode(Logger logger, SlOperator op, SlExpression l, SlExpression r, SlScope scope): base(logger, GetReturnType(op, l, r), scope)
    {
        _op = op;
        _l = l;
        _r = r;
    }

    private static string GetReturnType(SlOperator op, SlExpression l, SlExpression r)
    {
        return op.GetReturnType(l.ExType, r.ExType);
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        Logger.Add($"Generating a value for {_l.ExType} {_op.OpType} {_l.ExType}");
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

        return func(builder, _l.GenerateValue(builder, module), _r.GenerateValue(builder, module), $"result_{_num.ToString()}");
    }
}