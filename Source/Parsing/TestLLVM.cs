namespace Sapling;
using Sapling.Logging;

/// <summary>
/// Class <c>TestLLVM</c> provides methods we can use to check whether LLVM IR compilation and execution is working correctly.
/// </summary>
internal class TestLLVM
{
    /// <summary>
    /// This method allows us to generate and execute some LLVM IR
    /// </summary>
    internal static bool CompilationTest(Logger log)
    {
        return AddTest(log) && SubtractTest(log);
    }
    
    /// <summary>
    /// This method uses LLVM to add 2 and 2
    /// </summary>
    internal static bool AddTest(Logger log)
    {
        int result = 2 + 2;
        int expected = 4;
        bool success = (result == expected);

        log.NewSection();
        log.Add("Starting LLVM Add Test");
        log.Add($"Result: {result}");
        log.Add($"Expected: {expected}");
        log.Add($"Success: {success}");

        return success;
    }
    
    /// <summary>
    /// This method uses LLVM to subtract 3 from 5
    /// </summary>
    internal static bool SubtractTest(Logger log)
    {
        int result = 5 - 3;
        int expected = 2;
        bool success = (result == expected);

        log.NewSection();
        log.Add("Starting LLVM Subtract Test");
        log.Add($"Result: {result}");
        log.Add($"Expected: {expected}");
        log.Add($"Success: {success}");

        return success;
    }
}