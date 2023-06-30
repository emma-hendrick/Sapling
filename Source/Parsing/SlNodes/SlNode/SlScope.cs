namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlScope
{
    static Dictionary<string, LLVMSharp.LLVMTypeRef> literalTypes = new Dictionary<string, LLVMSharp.LLVMTypeRef> 
    {
        {"int", LLVMSharp.LLVM.Int32Type()},
        {"bool", LLVMSharp.LLVM.Int1Type()},
        {"char", LLVMSharp.LLVM.Int8Type()},
        {"str", LLVMSharp.LLVM.ArrayType(LLVMSharp.LLVM.Int8Type(), 0)},
        {"float", LLVMSharp.LLVM.FloatType()},
    };

    private Dictionary<string, string> _types = new Dictionary<string, string>();
    private Dictionary<string, LLVMSharp.LLVMValueRef> _values = new Dictionary<string, LLVMSharp.LLVMValueRef>();

    public SlScope()
    {
    }

    public SlScope(SlScope parent)
    {
        _types = parent.GetTypes().ToDictionary(entry => entry.Key, entry => entry.Value);
        _values = parent.GetValues().ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<string, string> GetTypes()
    {
        return _types;
    }

    public Dictionary<string, LLVMSharp.LLVMValueRef> GetValues()
    {
        return _values;
    }

    public LLVMSharp.LLVMTypeRef FindType(Logger logger, string type)
    {
        logger.Add($"Getting type {type} from scope");

        // If the type is a literal type return it
        if (literalTypes.ContainsKey(type)) return literalTypes[type];
        
        // If the type is not a literal we will need to parse it to see if it could be a user defined type
        return ParseType(logger, type);
    }

    public LLVMSharp.LLVMTypeRef ParseType(Logger logger, string type)
    {
        // If parsing fails throw an exception
        throw new Exception($"Failed to parse type: {type}");
    }

    public LLVMSharp.LLVMValueRef Get(Logger logger, string identifier)
    {
        logger.Add($"Getting {identifier} from scope");
        if (!_values.ContainsKey(identifier)) throw new Exception($"Could not find {identifier} in scope");
        return _values[identifier];
    }

    public string GetType(Logger logger, string identifier)
    {
        logger.Add($"Getting {identifier} type from scope");
        if (!_types.ContainsKey(identifier)) throw new Exception($"Could not find {identifier} in scope");
        return _types[identifier];
    }

    public void Add(Logger logger, string identifier, LLVMSharp.LLVMValueRef value)
    {
        logger.Add($"Adding values of {identifier} to scope");
        _values.Add(identifier, value);
    }

    public void AddType(Logger logger, string identifier, string type)
    {
        logger.Add($"Adding type of {identifier} to scope");
        _types.Add(identifier, type);
    }
}