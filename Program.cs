namespace Sapling;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c>Program</c> interprets and runs the users command to either run, build, or test a sapling program.
/// </summary>
internal static class Program
{
    // A list of valid commands
    private static List<string> _validCommands = new List<string>()
        {
            "run",
            "build",
            "test"
        };

    // Define the execution command string in one place, so if it changes we only need to update it once
    private static string _executionStringFormat = $"Your execution command should be in the form of: sapling {{command}} {{filename.sl}}";

    // The programs entry point
    private static int Main(string[] args)
    {
        // Default to using source.sl if the user does not provide a file
        string filename = args.Length > 1 ? args[1]: "source.sl";

        // Check whether the filename is valid, and if not, print an error
        if(filename.Substring(filename.Length - 3) != ".sl")
        {
            PrintError($"You have entered an invalid filename. Valid filenames should end in .sl. \n{_executionStringFormat}");
            return 1;
        }

        // Default to run the program if the user does not provide a command
        // Valid commands are run, build, and test
        string command = args.Length != 0 ? args[0]: "run";

        // Check whether the command is valid, and if not, print an error
        if(!_validCommands.Contains(command))
        {
            PrintError($"You have entered an invalid command. Valid commands are {string.Join(", ", _validCommands)}. \n{_executionStringFormat}");
            return 1;
        }

        // Execute the users command on their provided file
        switch(command){
            case "run":
                return Run(filename);
            case "build":
                return Build(filename);
            case "test":
                return Test(filename);
        }

        // As somehow, the program has not caught the error that we are executing a valid command, lets return an error since nothing else has been returned.
        // There was no success, so there must have been an error.
        PrintError("Sorry, something went wrong. Please submit an issue on our GitHub repository, and we will address the issue as soon as possible.");
        return 1;
    }

    /// <summary>
    /// This method compiles a sapling program into an executable.
    /// <example>
    /// For example:
    /// <code>
    /// Build("source.sl");
    /// </code>
    /// results in the creation of source.exe.
    /// </example>
    /// </summary>
    private static int Build(string filename)
    {
        return 0;
    }

    /// <summary>
    /// This method runs an executable.
    /// <example>
    /// For example:
    /// <code>
    /// Run("source.sl");
    /// </code>
    /// results in the creation and execution of source.exe.
    /// </example>
    /// </summary>
    private static int Run(string filename)
    {
        return 0;
    }

    /// <summary>
    /// This method runs an sapling program in testing mode.
    /// <example>
    /// For example:
    /// <code>
    /// Test("source.sl");
    /// </code>
    /// results in the creation and testing of source.exe.
    /// </example>
    /// </summary>
    private static int Test(string filename)
    {
        return 0;
    }


    /// <summary>
    /// This method prints an error message.
    /// <example>
    /// For example:
    /// <code>
    /// PrintError("Fix Your Code!");
    /// </code>
    /// results in the message, "Fix Your Code," being printed to the terminal.
    /// </example>
    /// </summary>
    private static void PrintError(string Error)
    {
        // Just printing in red
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(Error);
        Console.ForegroundColor = ConsoleColor.White;
    }
}