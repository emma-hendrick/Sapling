namespace Sapling;

/// <summary>
/// Class <c>EnviromnetParser</c> provides us with the methods to load environment variables once and then just get them from an instance of this.
/// </summary>
internal class EnvironmentLoader
{
    /// <summary>
    /// Our environment variables
    /// </summary>
    private Dictionary<string, string> variables = new Dictionary<string, string>();

    /// <summary>
    /// Construct a new environment loader
    /// </summary>
    public EnvironmentLoader()
    {
        Load();
    }

    /// <summary>
    /// Load the variables from our .env file
    /// </summary>
    private void Load()
    {
        string[] lines = File.ReadAllLines(".env");

        foreach (string line in lines)
        {
            string[] splitLine = line.Split('=');
            if (splitLine.Length != 2) throw new Exception($"Invalid line in .env: {line}");
            variables.Add(splitLine[0], splitLine[1]);
        }
    }

    /// <summary>
    /// Get a variable from our environment file if it is there otherwise return null
    /// </summary>
    public string? Get(string key)
    {
        if (!variables.ContainsKey(key)) return null;
        return variables[key];
    }
}