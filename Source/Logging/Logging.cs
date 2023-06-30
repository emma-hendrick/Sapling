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
    /// How indented the current log should be
    /// </summary>
    private string _indent;

    /// <summary>
    /// Whether or not we are outputting to the console
    /// </summary>
    private bool _printOutput;

    /// <summary>
    /// Whether or not we are outputting to the console
    /// </summary>
    private bool _debug;

    /// <summary>
    /// The name of the file this log is generated for.
    /// </summary>
    private string _filename = "";

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
    public Logger(string filename = Constants.DefaultFileName, bool printOutput = false, bool debug = false)
    {
        if (!Directory.Exists("./logs")) {
            Directory.CreateDirectory("./logs");
        }

        _startTime = DateTime.Now.ToString("MMddyy-Hmmss");
        _indent = "";
        _filename = filename;
        _printOutput = printOutput;
        _debug = debug;
    }

    /// <summary>
    /// Increase the logger indent.
    /// </summary>
    public void IncreaseIndent()
    {
        _indent = $"{_indent}  ";
    }

    /// <summary>
    /// Decrease the logger indent.
    /// </summary>
    public void DecreaseIndent()
    {
        if (!(_indent.Length > 1)) throw new Exception("Trying to decrease logger indent when logger is not indented.");
        _indent = _indent.Substring(0, _indent.Length - 2);
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
        _Add(message);
    }
    
    /// <summary>
    /// This method adds a new integer to the current log.
    /// <example>
    /// For example:
    /// <code>
    /// loggerInstance.Add(5);
    /// </code>
    /// will add a new line to the log containing the time and the message 5.
    /// </example>
    /// </summary>
    public void Add(int i)
    {
        _Add(i.ToString());
    }

    /// <summary>
    /// Add a message to the logger
    /// </summary>
    private void _Add(string message)
    {
        string time = DateTime.Now.ToString("H:mm:ss");
        string m = $"{time}| {_indent}{message}";

        using (StreamWriter writer = File.AppendText($"./logs/{_filename}-{_startTime}.log"))
        {
            writer.WriteLine(m);
        }

        if (_printOutput) Console.WriteLine(m);
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

    /// <summary>
    /// This method dumps an error and its stack trace
    /// <example>
    /// For example:
    /// <code>
    /// loggerInstance.Dump(new Exception("BAD"));
    /// </code>
    /// should create a new log containing that error and its stack.
    /// </example>
    /// </summary>
    public void Dump(Exception e)
    {
        if (!Directory.Exists("./logs/errs")) {
            Directory.CreateDirectory("./logs/errs");
        }
        
        string time = DateTime.Now.ToString("H:mm:ss");
        string m = $"{time}| {e.Message}";
        string? trace = e.StackTrace;

        using (StreamWriter writer = File.AppendText($"./logs/errs/Err-{_filename}-{_startTime}.log"))
        {
            writer.WriteLine(m);
            writer.WriteLine(trace);
        }

        // Just printing in like an orange to make the stack trace stand out better
        Console.ForegroundColor = ConsoleColor.DarkRed;
        if (_debug) Console.WriteLine(trace);
        Console.ResetColor();
    }
}