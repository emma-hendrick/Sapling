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
        _startTime = DateTime.Now.ToString("MMddyy-Hmmss");
        Add("------------------------------");
    }

    /// <summary>
    /// This method adds a new line to the current log.
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
        using (StreamWriter writer = File.AppendText($"./{_startTime}.log"))
        {
            writer.WriteLine($"{time}| {message}");
        }
    }
}