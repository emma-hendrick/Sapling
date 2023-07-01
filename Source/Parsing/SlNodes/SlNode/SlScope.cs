namespace Sapling.Nodes;
using Sapling.Logging;
using System.Linq;

/// <summary>
/// This class represents a scope within a sapling program, this scope will contain a list of the types of identifiers which are active in the scope, and their values
/// </summary>
internal class SlScope
{
    /// <summary>
    /// A list of types which are used by literal expressions
    /// </summary>
    private static Dictionary<string, LLVMSharp.LLVMTypeRef> _literalTypes = new Dictionary<string, LLVMSharp.LLVMTypeRef> 
    {
        {"int", LLVMSharp.LLVM.Int32Type()},
        {"bool", LLVMSharp.LLVM.Int1Type()},
        {"str", LLVMSharp.LLVM.PointerType(LLVMSharp.LLVM.Int8Type(), 0)},
        {"float", LLVMSharp.LLVM.FloatType()},
    };

    /// <summary>
    /// The types of identifiers which are alive in the scope
    /// </summary>
    private Dictionary<string, string> _types = new Dictionary<string, string>();

    /// <summary>
    /// The values of identifiers which are alive in the scope
    /// </summary>
    private Dictionary<string, LLVMSharp.LLVMValueRef> _values = new Dictionary<string, LLVMSharp.LLVMValueRef>();

    /// <summary>
    /// The types of functions which are alive in the scope
    /// </summary>
    private Dictionary<string, LLVMSharp.LLVMTypeRef> _functionTypes = new Dictionary<string, LLVMSharp.LLVMTypeRef>();

    /// <summary>
    /// The values of functions which are alive in the scope
    /// </summary>
    private Dictionary<string, LLVMSharp.LLVMValueRef> _functionValues = new Dictionary<string, LLVMSharp.LLVMValueRef>();

    /// <summary>
    /// Construct a new empty SlScope
    /// </summary>
    public SlScope()
    {
    }

    /// <summary>
    /// Construct a new SlScope, and have it inherit the values of its parent.
    /// </summary>
    public SlScope(SlScope parent)
    {
        _types = parent.GetTypes().ToDictionary(entry => entry.Key, entry => entry.Value);
        _values = parent.GetValues().ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    /// <summary>
    /// Get the types within a scope
    /// </summary>
    public Dictionary<string, string> GetTypes()
    {
        return _types;
    }


    /// <summary>
    /// Get the values within a scope
    /// </summary>
    public Dictionary<string, LLVMSharp.LLVMValueRef> GetValues()
    {
        return _values;
    }

    /// <summary>
    /// Find a type from within the scope
    /// </summary>
    public LLVMSharp.LLVMTypeRef FindType(Logger logger, string type)
    {
        logger.Add($"Getting type {type} from scope");

        // If the type is a literal type return it
        if (_literalTypes.ContainsKey(type)) return _literalTypes[type];
        // Otherwise throw an exception
        throw new Exception($"Failed to parse type: {type}");
    }

    /// <summary>
    /// Find a type string from within the scope
    /// </summary>
    public string FindFunctionTypeString(Logger logger, LLVMSharp.LLVMTypeRef type)
    {
        logger.Add($"Getting ret type string of function {type.ToString()} from scope");

        // Retrieve the return types
        LLVMSharp.LLVMTypeRef returnType = LLVMSharp.LLVM.GetReturnType(type);

        // If the type is a literal type return it
        foreach (var KVP in _literalTypes) if (KVP.Value.Equals(returnType)) return KVP.Key;
        // Otherwise throw an exception
        throw new Exception($"Failed to parse type: {type}");
    }

    /// <summary>
    /// Get the value linked to an identifier in the scope
    /// </summary>
    public LLVMSharp.LLVMValueRef Get(Logger logger, string identifier)
    {
        logger.Add($"Getting {identifier} from scope");
        if (!_values.ContainsKey(identifier)) throw new Exception($"Could not find {identifier} in scope");
        return _values[identifier];
    }

    /// <summary>
    /// Get the type linked to an identifier in the scope
    /// </summary>
    public string GetType(Logger logger, string identifier)
    {
        logger.Add($"Getting {identifier} type from scope");
        if (!_types.ContainsKey(identifier)) throw new Exception($"Could not find {identifier} in scope");
        return _types[identifier];
    }

    /// <summary>
    /// Get the value linked to an function identifier in the scope
    /// </summary>
    public LLVMSharp.LLVMValueRef GetFunction(Logger logger, string identifier)
    {
        logger.Add($"Getting function {identifier} from scope");
        if (!_functionValues.ContainsKey(identifier)) throw new Exception($"Could not find function {identifier} in scope");
        return _functionValues[identifier];
    }

    /// <summary>
    /// Get the type linked to an function identifier in the scope
    /// </summary>
    public LLVMSharp.LLVMTypeRef GetFunctionType(Logger logger, string identifier)
    {
        logger.Add($"Getting function {identifier} type from scope");
        if (!_functionTypes.ContainsKey(identifier)) throw new Exception($"Could not find function {identifier} in scope");
        return _functionTypes[identifier];
    }

    /// <summary>
    /// Add an identifier and its value to the scope
    /// </summary>
    public void Add(Logger logger, string identifier, LLVMSharp.LLVMValueRef value)
    {
        logger.Add($"Adding value of {identifier} to scope");
        _values.Add(identifier, value);
    }

    /// <summary>
    /// Add an identifier and its type to the scope
    /// </summary>
    public void AddType(Logger logger, string identifier, string type)
    {
        logger.Add($"Adding type of {identifier} to scope");
        _types.Add(identifier, type);
    }

    /// <summary>
    /// Add a function identifier and its value to the scope
    /// </summary>
    public void AddFunction(Logger logger, string identifier, LLVMSharp.LLVMValueRef value)
    {
        logger.Add($"Adding function {identifier} to scope");
        _functionValues.Add(identifier, value);
    }

    /// <summary>
    /// Add a function identifier and its type to the scope
    /// </summary>
    public void AddFunctionType(Logger logger, string identifier, LLVMSharp.LLVMTypeRef type)
    {
        logger.Add($"Adding type of function {identifier} to scope");
        _functionTypes.Add(identifier, type);
    }
}