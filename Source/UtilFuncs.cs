namespace Sapling;
using System;

/// <summary>
/// Class <c>UtilFuncs</c> is used to provide useful functions in sapling..
/// </summary>
internal class UtilFuncs
{
    /// <summary>
    /// This method allows us to see whether a type is equivalent to (or descended from) another
    /// </summary>
    internal static bool TypeEquivalence(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
            || potentialDescendant == potentialBase;
    }
}