namespace Sapling;
using Sapling.Logging;
using Sapling.Lexer;
using static Sapling.TestLLVM;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

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
    /// A list of invalid filenames which are used for specific testing cases.
    /// </summary>
    private static List<string> _invalidFilenames = new List<string>()
        {
            "test_add.sl"
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
            // Default if the user does not provide a file
            string filename = args.Length > 1 ? args[1]: Constants._defaultFileName;

            // Reinitialize the logger to use the filename provided in the parameters
            _logger = new Logger(filename.Substring(0, filename.Length - 3), printOutput: Environment.PrintOutput == "true", debug: Environment.Debug == "true");

            // Check whether the filename is valid, and if not, throw an error
            if(_invalidFilenames.Contains(filename)) throw new Exception($"You have entered an invalid filename: You cannot use the following, as they are reserved for tests: {string.Join(' ', _invalidFilenames)}");
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
            string baseFilename = filename.Substring(0, filename.Length - 3);
            int success = commandFunc(baseFilename);
            _logger.NewSection();
            _logger.Add($"Command \"{command}\" executed on \"{filename}\".");
            return success;

            // As somehow, the program has not caught the error that we are executing a valid command, lets throw an error since nothing else has been returned.
            // There was no success, so there must have been an error.
            throw new Exception("Sorry, something went wrong. Please submit an issue on our GitHub repository, and we will address the issue as soon as possible.");

        }
        catch (Exception exception)
        {

            // Print the error then dump the error
            PrintError(exception.Message);
            _logger.Dump(exception);
            return -1;

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
        // Create a new logger section and create a new builds folder if necessary
        _logger.NewSection();
        _logger.Add($"Compiling {filename}.sl");
        _logger.Add($"Current target: {Environment.Target}");

        // Create the builds folder if it doesn't exists
        if (!Directory.Exists("./builds")) {
            _logger.Add($"Creating builds folder");
            Directory.CreateDirectory("./builds");
        }
        
        // Remove the bitcode if it already exists
        if (File.Exists($"builds/{filename}.bc"))
        {
            File.Delete($"builds/{filename}.bc");
            _logger.Add($"builds/{filename}.bc deleted.");
        }

        IEnumerable<Tokens.Token> tokens;
        // Get all tokens from the file
        string fileContent = File.ReadAllText($"{filename}.sl");
        PrecedenceBasedLexer lexer = new PrecedenceBasedLexer(Constants._tokenList);
        tokens = lexer.GetTokens(fileContent);

        _logger.NewSection();
        _logger.Add($"Acquired nodes");

        // Create a parser to handle our tokens
        Parser parser = new Parser(tokens, _logger);
        AST ast = parser.Parse();

        // Call the generate code method on the AST to emit the bitcode for our program
        ast.GenerateCode($"builds/{filename}.bc");

        // Generate the LLVM IR
        _logger.NewSection();
        _logger.Add("Writing LLVM IR");

        // Run the clang command to write the LLVM IR
        Process process1 = new Process();
        process1.StartInfo.FileName = "clang";
        process1.StartInfo.Arguments = $"builds/{filename}.bc -S -emit-llvm -o builds/{filename}.ll";
        process1.StartInfo.RedirectStandardOutput = true;
        process1.StartInfo.UseShellExecute = false;
        process1.Start();

        // Read the output or wait for the process to exit
        string output1 = process1.StandardOutput.ReadToEnd();
        process1.WaitForExit();

        // Process the output or handle any errors
        if (output1 != "")
        {
            _logger.Add($"LLVM IR writing generated output: Output = {output1}");
        }

        // Check the process exit code
        int exitCode1 = process1.ExitCode;
        if (exitCode1 == 0)
        {
            _logger.Add("LLVM IR writing succeeded.");
        }
        else
        {
            throw new Exception($"LLVM IR writing failed with exit code: {exitCode1}");
        }

        _logger.NewSection();
        _logger.Add("Compiling executable");

        // Run the clang command to compile the code
        Process process2 = new Process();
        process2.StartInfo.FileName = "clang";
        process2.StartInfo.Arguments = $"builds/{filename}.bc -o builds/{filename}";
        process2.StartInfo.RedirectStandardOutput = true;
        process2.StartInfo.UseShellExecute = false;
        process2.Start();

        // Read the output or wait for the process to exit
        string output2 = process2.StandardOutput.ReadToEnd();
        process2.WaitForExit();

        // Process the output or handle any errors
        if (output2 != "")
        {
            _logger.Add($"Compilation generated output: Output = {output2}");
        }

        // Check the process exit code
        int exitCode2 = process2.ExitCode;
        if (exitCode2 == 0)
        {
            _logger.Add("Compilation succeeded.");
        }
        else
        {
            throw new Exception($"Compilation failed with exit code: {exitCode2}");
        }

        return exitCode2;
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
        _logger.Add($"Running {filename}.sl");

        // Execute the generated file
        Process process = new Process();
        process.StartInfo.FileName = $"builds/{filename}";
        // process.StartInfo.Arguments = $""; TODO get args from input
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        // Read the output or wait for the process to exit
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Process the output or handle any errors
        if (output != "")
        {
            _logger.Add($"Execution generated output: Output = {output}");
        }

        // Check the process exit code
        int exitCode = process.ExitCode;
        if (exitCode == 0)
        {
            _logger.Add("Execution succeeded.");
        }
        else
        {
            throw new Exception($"Execution failed with exit code: {exitCode}");
        }

        return exitCode;
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
        _logger.Add($"Testing {filename}.sl");

        // Execute the generated file
        Process process = new Process();
        process.StartInfo.FileName = $"builds/{filename}";
        // process.StartInfo.Arguments = $"test"; TODO get args from input
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        // Read the output or wait for the process to exit
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Process the output or handle any errors
        if (output != "")
        {
            _logger.Add($"Testing generated output: Output = {output}");
        }

        // Check the process exit code
        int exitCode = process.ExitCode;
        if (exitCode == 0)
        {
            _logger.Add("Testing succeeded.");
        }
        else
        {
            throw new Exception($"Testing failed with exit code: {exitCode}");
        }

        return exitCode;
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
        Console.ResetColor();
    }
}