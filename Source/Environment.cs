namespace Sapling;
using Sapling.Lexer;
using Sapling.Tokens;
using System.Collections.Generic;

/// <summary>
/// Class <c>Enviromnet</c> constains environment variables for the program.
/// </summary>
internal static class Environment
{
    public static string Target => GetTarget();

    private static string GetTarget()
    {
        // TODO - get it from the environment, and add the ability for the user to set it with a command
        return "x86_64-w64-windows-gnu";
    }
}