namespace Sapling;

/// <summary>
/// Class <c>Enviromnet</c> constains environment variables for the program.
/// </summary>
internal static class Environment
{    
    /// <summary>
    /// The environment loader
    /// </summary>
    private static EnvironmentLoader _loader = new EnvironmentLoader(".env"); // TODO - Get from exact path, same folder the sapling executable is in

    /// <summary>
    /// The target of the compiler
    /// </summary>
    public static string Target => _loader.Get("TARGET") ?? Constants.DefaultTarget;
    
    /// <summary>
    /// Whether or not to print the output of the logger 
    /// </summary>
    public static string PrintOutput => _loader.Get("PRINT_OUTPUT") ?? Constants.DefaultPrintOutput;
    
    /// <summary>
    /// Whether or not to print the loggers debug messages
    /// </summary>
    public static string Debug => _loader.Get("DEBUG") ?? Constants.DefaultDebug;
}