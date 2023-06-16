namespace Sapling;
using Sapling.Logging;
using Sapling.Nodes;

/// <summary>
/// Class <c>AST</c> holds a set of nodes in a way which conveys execution order of tokens.
/// </summary>
internal class AST
{
    /// <summary>
    /// The root node of the AST.
    /// </summary>
    private SlMethod _root;

    /// <summary>
    /// The logger to use.
    /// </summary>
    private Logger _logger;

    /// <summary>
    /// This construsts a new AST.
    /// <example>
    /// For example:
    /// <code>
    /// AST ast = new AST(log);
    /// </code>
    /// will create a new AST, which can then be used to generate the LLVM bitcode for its nodes.
    /// </example>
    /// </summary>
    public AST(SlMethod root, Logger logger)
    {
        _logger = logger;
        _root = root;
    }

    /// <summary>
    /// Generate the LLVM bitcode for the nodes in the tree.
    /// <example>
    public void GenerateCode(string filename)
    {
    }

    /// <summary>
    /// Append a node to the root node.
    /// <example>
    public void Append(SlStatement node)
    {
        _root.Append(node);
    }
}