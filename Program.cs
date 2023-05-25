namespace Sapling;
using Sapling.Logging;
using Sapling.Lexer;
using static Sapling.UtilFuncs;
using static Sapling.TestLLVM;
using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Class <c>Program</c> interprets and runs the users command to either run, build, or test a sapling program.
/// </summary>
internal static class Program
{
    /// <summary>
    /// A logger to be used in Sapling compilation, execution, and testing.
    /// </summary>
    private static Logger _logger = new Logger();

    /// <summary>
    /// A list of valid commands when executing the Sapling compiler from the command line.
    /// </summary>
    private static List<string> _validCommands = new List<string>()
        {
            "run",
            "build",
            "test"
        };

    /// <summary>
    /// An example of the execution string, which we can show the user if they enter an incorrect command.
    /// </summary>
    private static string _executionStringFormat = $"Your execution command should be in the form of: sapling {{command}} {{filename.sl}}.";

    // The programs entry point
    private static int Main(string[] args)
    {
        try
        {

            // Default to using source.sl if the user does not provide a file
            string filename = args.Length > 1 ? args[1]: "source.sl";

            // Check whether the filename is valid, and if not, throw an error
            if(filename.Substring(filename.Length - 3) != ".sl") throw new Exception($"You have entered an invalid filename: Valid filenames should end in .sl. {_executionStringFormat}");

            // Default to run the program if the user does not provide a command
            // Valid commands are run, build, and test
            string command = args.Length != 0 ? args[0]: "run";

            // Execute the users command on their provided file, and store the result in success to return at the end
            Func<string, int> commandFunc;
            switch(command){
                case "run":
                    commandFunc = Run;
                    break;
                case "build":
                    commandFunc = Build;
                     break;
                case "test":
                    commandFunc = Test;
                    break;
                default:
                    throw new Exception($"You have entered an invalid command: Valid commands are {string.Join(", ", _validCommands)}. {_executionStringFormat}");

            
            }

            // Store the result of the users command to return after execution
            int success = commandFunc(filename);
            _logger.NewSection();
            _logger.Add($"Command \"{command}\" executed on \"{filename}\".");
            return success;

            // As somehow, the program has not caught the error that we are executing a valid command, lets throw an error since nothing else has been returned.
            // There was no success, so there must have been an error.
            throw new Exception("Sorry, something went wrong. Please submit an issue on our GitHub repository, and we will address the issue as soon as possible.");

        }
        catch (Exception exception)
        {

            // Log the error so that we can see what happened after the fact, then print the error
            _logger.Add($"Exception: {exception.Message}");
            PrintError(exception.Message);
            return 1;

        }
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
        _logger.NewSection();
        _logger.Add($"Compiling {filename}");
        IEnumerable<Tokens.Node> tokens;
        
        // Get all tokens from the file
        try
        {
            string fileContent = File.ReadAllText($"{filename}");
            PrecedenceBasedLexer lexer = new PrecedenceBasedLexer(Constants._tokenList);
            tokens = lexer.GetTokens(fileContent);

            _logger.NewSection();
            _logger.Add($"Acquired nodes");
        }
        catch (FileNotFoundException)
        {
            string error = "The file does not exist.";
            _logger.Add(error);
            PrintError(error);
            return 1;

        }
        catch (IOException ex)
        {
            string error = $"An error occurred while reading the file: {ex.Message}";
            _logger.Add(error);
            PrintError(error);
            return 1;
        }

        // Parse those tokens
        foreach (Tokens.Node token in tokens)
        {

            if (!TypeEquivalence(typeof(Tokens.Comment), token.GetType())) _logger.Add($"{token.GetType()} \"{token.Value}\" at {token.startIndex} to {token.endIndex}");

        }

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
        Build(filename);

        _logger.NewSection();
        _logger.Add($"Running {filename}");
        return 0;
    }

    /// <summary>
    /// This method tests LLVM IR generation, compilation, and execution, and then runs a sapling program in testing mode.
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
        if (!CompilationTest(_logger))
        {
            throw new Exception("There was an error when testing LLVM compilation. Please check our github, and if there is not an issue posted, please post it.");
        }

        Build(filename);

        _logger.NewSection();
        _logger.Add($"Testing {filename}");
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