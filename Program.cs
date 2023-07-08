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
            "run-raw",
            "build",
            "test",
            "help",
            "clean",
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

    /// <summary>
    /// Read and execute the users sapling command
    /// </summary>
    private static int Main(string[] args)
    {
        Console.WriteLine(Environment.AssemblyDirectory);
        try
        {
            // Default if the user does not provide a file
            string filename = args.Length > 1 ? args[1]: Constants.DefaultFileName;

            // Reinitialize the logger to use the filename provided in the parameters
            _logger = new Logger(filename.Substring(filename.Length - 3) == ".sl" ? filename.Substring(0, filename.Length - 3) : filename, printOutput: (Environment.PrintOutput == "true"), debug: (Environment.Debug == "true"));

            // Check whether the filename is valid, and if not, throw an error
            if(_invalidFilenames.Contains(filename)) throw new Exception($"You have entered an invalid filename: You cannot use the following, as they are reserved for tests: {string.Join(' ', _invalidFilenames)}");
            if(filename.Substring(filename.Length - 3) != ".sl") throw new Exception($"You have entered an invalid filename: Valid filenames should end in .sl. {_executionStringFormat}");

            // Default to run the help command if the user does not provide a command
            string command = args.Length != 0 ? args[0]: "help";

            // Execute the users command on their provided file, and store the result in success to return at the end
            Func<string, int> commandFunc;
            switch(command){
                case "run":
                    commandFunc = Run;
                    break;
                case "run-raw":
                    commandFunc = RunRaw;
                    break;
                case "build":
                    commandFunc = Build;
                     break;
                case "test":
                    commandFunc = Test;
                    break;
                case "help":
                    commandFunc = Help;
                    break;
                case "clean":
                    commandFunc = Clean;
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
        PrecedenceBasedLexer lexer = new PrecedenceBasedLexer(Constants.TokenList);
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
    /// This method creates and runs an executable.
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
        return Run(filename, false, true);
    }

    /// <summary>
    /// This method runs an executable.
    /// <example>
    /// For example:
    /// <code>
    /// Run("source.sl");
    /// </code>
    /// results in the execution of source.exe.
    /// </example>
    /// </summary>
    private static int RunRaw(string filename)
    {
        return Run(filename, false, false);
    }

    /// <summary>
    /// This method tests LLVM IR generation, compilation, and execution, and then runs a sapling program in testing mode.
    /// <example>
    /// For example:
    /// <code>
    /// Test("source.sl");
    /// </code>
    /// results in the creation and execution of source.exe in testing mode.
    /// </example>
    /// </summary>
    private static int Test(string filename)
    {
        if (!CompilationTest(_logger))
        {
            throw new Exception("There was an error when testing LLVM compilation. Please check our github, and if there is not an issue posted, please post it.");
        }

        return Run(filename, true, true);
    }

    /// <summary>
    /// This method runs an executable in either standard or testing mode.
    /// <example>
    /// For example:
    /// <code>
    /// Run("source.sl", true);
    /// </code>
    /// results in the creation and execution of source.exe in testing mode.
    /// </example>
    /// </summary>
    private static int Run(string filename, bool testing = false, bool compile = true)
    {
        if (compile) Build(filename);

        _logger.NewSection();
        string mode = testing ? "Testing" : "Running";
        _logger.Add($"{mode} {filename}.sl");

        // Execute the generated file
        Process process = new Process();
        process.StartInfo.FileName = $"builds/{filename}";
        process.StartInfo.Arguments = testing ? "test" : ""; // TODO get args from input
        process.StartInfo.RedirectStandardOutput = false;
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.WaitForExit();

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
    /// This method provides the user with help on how to use Sapling.
    /// <example>
    /// For example:
    /// <code>
    /// Help("source.sl");
    /// </code>
    /// results in printing the help instructions.
    /// </example>
    /// </summary>
    private static int Help(string filename)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Hello future Sapling user!");
        Console.WriteLine("Execute sapling run {filename} in order to run your very own sapling program.");
        Console.WriteLine("Sapling documentation is located here: ");
        Console.ResetColor();
        return 0;
    }

    /// <summary>
    /// This method deletes logs and build files (with the exception of the executable) for a specific file.
    /// <example>
    /// For example:
    /// <code>
    /// Clean("source.sl");
    /// </code>
    /// will delete all logs and build files (sans executable) of source.sl.
    /// </example>
    /// </summary>
    private static int Clean(string filename)
    {
        _logger.NewSection();
        _logger.Add($"Cleaning {filename}.sl");

        // Delete a file if it exists
        void DeleteIfExists(string filename)
        {  
            if (File.Exists(filename))
            {
                _logger.Add($"Deleting {filename}.");
                File.Delete(filename);
                _logger.Add($"{filename} deleted.");
            }
        }

        // Delete the build files (minus the executable)
        DeleteIfExists($"builds/{filename}.bc");
        DeleteIfExists($"builds/{filename}.ll");

        // Delete all logs from the logs directory
        string[] logs = Directory.GetFiles("logs");
        foreach (string logName in logs) 
        {
            if (logName.Substring(5, filename.Length) == filename && logName.Substring(5) != _logger.LogName) 
            {
                DeleteIfExists(logName);
            }
        }

        // Delete all errors from the error directory
        string[] errs = Directory.GetFiles("logs/errs");
        foreach (string errName in errs) 
        {
            if (errName.Substring(14, filename.Length) == filename && errName.Substring(10) != _logger.ErrLogName) 
            {
                DeleteIfExists(errName);
            }
        }
        
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
        Console.ResetColor();
    }
}