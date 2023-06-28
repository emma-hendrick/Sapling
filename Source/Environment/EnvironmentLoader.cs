namespace Sapling;

/// <summary>
/// Class <c>EnviromnetParser</c> provides us with the methods to load environment variables once and then just get them from an instance of this.
/// </summary>
internal class EnvironmentLoader
{
    private Dictionary<string, string> variables = new Dictionary<string, string>();

    public EnvironmentLoader()
    {
        Load();
    }

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

    public string? Get(string key)
    {
        if (!variables.ContainsKey(key)) return null;
        return variables[key];
    }
}