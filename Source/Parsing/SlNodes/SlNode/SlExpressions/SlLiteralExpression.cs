namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlLiteralExpression: SlExpression
{
    private string _type;
    private string _value;

    public SlLiteralExpression(Logger logger, string type, string value, SlScope scope): base(logger, type, scope)
    {
        _type = type;
        _value = value;
    }

    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        Func<string, LLVMSharp.LLVMBuilderRef, LLVMSharp.LLVMValueRef> parser;
        switch (_type)
        {
            case "Integer":
                parser = ParseInt;
                break;
            case "Float":
                parser = ParseFloat;
                break;
            case "String":
                parser = ParseString;
                break;
            case "Char":
                parser = ParseChar;
                break;
            case "Bool":
                parser = ParseBool;
                break;
            default:
                throw new Exception($"Unrecognized type {_type}");
        }

        return parser(_value, builder);
    }

    public LLVMSharp.LLVMValueRef ParseInt(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        // These should be 32 bits long
        int i = int.Parse(value, System.Globalization.NumberStyles.AllowLeadingSign);
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), (ulong)i, true);
    }

    public LLVMSharp.LLVMValueRef ParseFloat(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        // These should be 32 bits long
        float i = float.Parse(value, System.Globalization.NumberStyles.AllowLeadingSign);
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.FloatType(), (ulong)i, true);
    }

    public LLVMSharp.LLVMValueRef ParseString(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        // TODO - This is a little more complicated since it should be an array
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int1Type(), 0, false);
    }

    public LLVMSharp.LLVMValueRef ParseChar(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        if ((value.Length - 2) != 1) throw new Exception($"Invalid char length of {(value.Length - 2)} in char {value}");

        char c = value[1];
        int i = (int)c; // Convert that char to a string
        
        // These should be a byte long
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int8Type(), (ulong)i, false);
    }

    public LLVMSharp.LLVMValueRef ParseBool(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        switch(value)
        {
            // These should be a bit long
            case "true":
                return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int1Type(), 1, false);
            case "false":
                return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int1Type(), 0, false);

            default:
                throw new Exception($"Unexpected expression, was expecting boolean, got {value}");
        }
    }
}