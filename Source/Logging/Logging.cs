namespace Sapling.Logging;
using System.IO;
using System;

/// <summary>
/// Class <c>Logger</c> allows us to create log files during comilation, execution, and testing of sapling programs.
/// </summary>
internal class Logger
{
    /// <summary>
    /// The time the logger was initialized. This will be the name of the log file.
    /// </summary>
    private string _startTime;

    /// <summary>
    /// This construsts a new logger.
    /// <example>
    /// For example:
    /// <code>
    /// Logger loggerInstance = new Logger();
    /// </code>
    /// will create a new logger called logger instance and will begin creating a log file.
    /// </example>
    /// </summary>
    public Logger()
    {
        if (!Directory.Exists("./logs")) {
            Directory.CreateDirectory("./logs");
        }

        _startTime = DateTime.Now.ToString("MMddyy-Hmmss");
        NewSection();
    }

    /// <summary>
    /// This method adds a new line of text to the current log.
    /// <example>
    /// For example:
    /// <code>
    /// loggerInstance.Add("Test");
    /// </code>
    /// will add a new line to the log containing the time and the message "Test".
    /// </example>
    /// </summary>
    public void Add(string message)
    {
        string time = DateTime.Now.ToString("H:mm:ss");
        using (StreamWriter writer = File.AppendText($"./logs/{_startTime}.log"))
        {
            writer.WriteLine($"{time}| {message}");
        }
    }


    /// <summary>
    /// This method adds a new dividing line to the current log.
    /// <example>
    /// For example:
    /// <code>
    /// loggerInstance.NewSection();
    /// </code>
    /// will add a new line to the log containing the characters used as a dividing line.
    /// </example>
    /// </summary>
    public void NewSection()
    {
        Add("------------------------------");
    }
}