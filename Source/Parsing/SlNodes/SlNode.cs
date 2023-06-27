namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlNode
{
    /// <summary>
    /// Base method for LLVM generation
    /// <example>
    public void GenerateCode(Logger logger)
    {
        logger.Add("Code Generation Base Method Called");
    }
}