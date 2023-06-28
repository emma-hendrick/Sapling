namespace Sapling;

/// <summary>
/// Class <c>Enviromnet</c> constains environment variables for the program.
/// </summary>
internal static class Environment
{
    private static EnvironmentLoader _loader = new EnvironmentLoader();
    public static string Target => _loader.Get("TARGET") ?? Constants._defaultTarget;
}