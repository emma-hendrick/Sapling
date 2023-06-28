namespace Sapling;

/// <summary>
/// Class <c>Enviromnet</c> constains environment variables for the program.
/// </summary>
internal static class Environment
{
    private static EnvironmentLoader _loader = new EnvironmentLoader();
    public static string Target => _loader.Get("TARGET") ?? Constants._defaultTarget;
    public static string PrintOutput => _loader.Get("PRINT_OUTPUT") ?? Constants._defaultPrintOutput;
    public static string Debug => _loader.Get("DEBUG") ?? Constants._defaultDebug;
}