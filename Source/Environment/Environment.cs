namespace Sapling;

/// <summary>
/// Class <c>Enviromnet</c> constains environment variables for the program.
/// </summary>
internal static class Environment
{    
    /// <summary>
    /// The environment loader
    /// </summary>
    private static EnvironmentLoader _loader = new EnvironmentLoader(Path.Combine(AssemblyDirectory, ".env"));

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

    /// <summary>
    /// The assembly file
    /// </summary>
    private static string AssemblyFile => (System.Reflection.Assembly.GetEntryAssembly() ?? System.Reflection.Assembly.GetExecutingAssembly()).Location; // Use the entry assembly if there is one, otherwise use the executing assembly

    /// <summary>
    /// The assembly directory
    /// </summary>
    public static string AssemblyDirectory => Path.GetDirectoryName( AssemblyFile ) ?? "There was an error, so we don't want this path working. No messing up the hosts machine!";
}