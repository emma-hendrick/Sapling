namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid literal expression within the sapling programming language
/// </summary>
internal class SlLiteralExpression: SlExpression
{
    /// <summary>
    /// The type of the literal expression
    /// </summary>
    private string _type;
    
    /// <summary>
    /// The value of the literal expression
    /// </summary>
    private string _value;

    /// <summary>
    /// Construct a new SlLiteralExpression
    /// </summary>
    public SlLiteralExpression(Logger logger, string type, string value, SlScope scope): base(logger, type, scope)
    {
        _type = type;
        _value = value;
    }

    /// <summary>
    /// Generate a value for an SlLiteralExpression
    /// </summary>
    public override LLVMSharp.LLVMValueRef GenerateValue(LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMModuleRef module)
    {
        Func<string, LLVMSharp.LLVMBuilderRef, LLVMSharp.LLVMValueRef> parser;
        switch (Constants.EquivalentTypes[_type])
        {
            case "int":
                parser = ParseInt;
                break;
            case "float":
                parser = ParseFloat;
                break;
            case "str":
                parser = ParseString;
                break;
            case "char":
                parser = ParseChar;
                break;
            case "bool":
                parser = ParseBool;
                break;
            default:
                throw new Exception($"Unrecognized type {_type}");
        }

        return parser(_value, builder);
    }

    /// <summary>
    /// Get the value of an int
    /// </summary>
    public LLVMSharp.LLVMValueRef ParseInt(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        // These should be 32 bits long
        int i = int.Parse(value, System.Globalization.NumberStyles.AllowLeadingSign);
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), (ulong)i, true);
    }

    /// <summary>
    /// Get the value of a float
    /// </summary>
    public LLVMSharp.LLVMValueRef ParseFloat(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        // These should be 32? bits long
        float i = float.Parse(value, System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
        return LLVMSharp.LLVM.ConstReal(LLVMSharp.LLVM.FloatType(), (ulong)i);
    }

    /// <summary>
    /// Get the value of a string
    /// </summary>
    public LLVMSharp.LLVMValueRef ParseString(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        return LLVMSharp.LLVM.ConstString(value, (uint)value.Length, true);
    }

    /// <summary>
    /// Get the value of a char
    /// </summary>
    public LLVMSharp.LLVMValueRef ParseChar(string value, LLVMSharp.LLVMBuilderRef builder)
    {
        if ((value.Length - 2) != 1) throw new Exception($"Invalid char length of {(value.Length - 2)} in char {value}");

        char c = value[1];
        int i = (int)c; // Convert that char to its code
        
        // These should be a byte long
        return LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int8Type(), (ulong)i, false);
    }

    /// <summary>
    /// Get the value of a bool
    /// </summary>
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