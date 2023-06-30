namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOptreeNode: SlExpression
{
    SlOperator _op;
    SlExpression _l;
    SlExpression _r;

    private List<string> _comparisonOperators = new List<string>
    {
        "==",
        "!=",
        "<",
        "<=",
        ">",
        ">="
    };

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

        if (!_comparisonOperators.Contains(_op.OpType))
        {
            // It is not a comparison operator
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
                case "&&":
                    func = LLVMSharp.LLVM.BuildAnd;
                    break;
                case "||":
                    func = LLVMSharp.LLVM.BuildOr;
                    break;
                case "^":
                    func = LLVMSharp.LLVM.BuildXor;
                    break;
                default:
                    throw new Exception($"Unexpected operator type {_op.OpType}");
            }

            // Create the right operator using the generated values of our left and right operands
            return func(builder, _l.GenerateValue(builder, module), _r.GenerateValue(builder, module), "result");
        }

        // It is a comparison operator
        LLVMSharp.LLVMIntPredicate compType;

        switch (_op.OpType)
        {
            case "==":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntEQ;
                break;
            case "!=":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntNE;
                break;
            case "<":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntSLT;
                break;
            case "<=":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntSLE;
                break;
            case ">":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntSGT;
                break;
            case ">=":
                compType = LLVMSharp.LLVMIntPredicate.LLVMIntSGE;
                break;
            default:
                throw new Exception($"Unexpected operator type {_op.OpType}");
        }

        // Create the correct comparison operator
        return LLVMSharp.LLVM.BuildICmp(builder, compType, _l.GenerateValue(builder, module), _r.GenerateValue(builder, module), "result");
    }
}