namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlScope
{
    static Dictionary<string, LLVMSharp.LLVMTypeRef> literalTypes = new Dictionary<string, LLVMSharp.LLVMTypeRef> 
    {
        {"int", LLVMSharp.LLVM.Int32Type()}
    };

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


}