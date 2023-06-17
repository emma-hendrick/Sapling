namespace Sapling.Nodes;

/// <summary>
/// </summary>
internal abstract class SlNode
{
    /// <summary>
    /// Generate the LLVM bitcode for this node and its children.
    /// <example>
    public abstract void GenerateCode();
}