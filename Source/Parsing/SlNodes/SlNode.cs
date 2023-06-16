namespace Sapling.Nodes;

/// <summary>
/// </summary>
internal abstract class SlNode
{
    /// <summary>
    /// Append a node as a child of this.
    /// <example>
    public void Append(SlNode node)
    {
    }

    /// <summary>
    /// Generate the LLVM bitcode for this node and its children.
    /// <example>
    public void GenerateCode(SlNode node)
    {
    }
}