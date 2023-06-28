namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal abstract class SlNode
{
    private Logger _logger;
    public Logger Logger => _logger;
    private SlScope _scope;
    public SlScope Scope => _scope;

    public SlNode(Logger logger, SlScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    /// <summary>
    /// Base method for LLVM generation
    /// <example>
    public void GenerateCode(Logger logger)
    {
        logger.Add("Code Generation Base Method Called");
    }
}