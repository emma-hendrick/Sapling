namespace Sapling;
using Sapling.Logging;
using System.Diagnostics;

/// <summary>
/// Class <c>TestLLVM</c> provides methods we can use to check whether LLVM IR compilation and execution is working correctly.
/// </summary>
internal class TestLLVM
{
    /// <summary>
    /// This method allows us to run all of our llvm compilation tests
    /// </summary>
    internal static bool CompilationTest(Logger log)
    {
        return TestAdd(log);
    }
    
    /// <summary>
    /// This method uses LLVM to compile a function which adds two numbers and verifies that we get the correct result.
    /// </summary>
    internal static bool TestAdd(Logger log)
    {
        CompileAdd(log);

        log.NewSection();
        log.Add("Compiling executable");

        // Run the clang command
        Process process = new Process();
        process.StartInfo.FileName = "clang";
        process.StartInfo.Arguments = "test_add.bc -o test_add"; // Replace with the actual command arguments
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        // Read the output or wait for the process to exit
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Process the output or handle any errors
        if (output != "")
        {
            log.Add($"Compilation generated output: Output = {output}");
        }

        // Check the process exit code
        int exitCode = process.ExitCode;
        if (exitCode == 0)
        {
            log.Add("Compilation succeeded.");
        }
        else
        {
            log.Add("Compilation failed with exit code: " + exitCode);
        }

        log.NewSection();
        log.Add("Executing file");

        // Execute the generated file
        Process process2 = new Process();
        process2.StartInfo.FileName = "test_add"; // Replace with the actual path to your executable file
        process2.StartInfo.RedirectStandardOutput = true;
        process2.StartInfo.UseShellExecute = false;
        process2.Start();

        // Read the output or wait for the process to exit
        string output2 = process2.StandardOutput.ReadToEnd();
        process2.WaitForExit();

        // Process the output or handle any errors
        if (output2 != "")
        {
            log.Add($"Execution generated output: Output = {output2}");
        }

        // Get the return value
        int returnValue = process2.ExitCode;
        log.NewSection();
        log.Add($"Execution return value: {returnValue}");

        // Verify that the value is what we expected
        int expected = 4;
        bool success = returnValue == expected;

        // Do something with the output and return value
        log.Add($"Expected: {expected}");
        log.Add($"Success: {success}");

        return success;
    }
    
    /// <summary>
    /// This method uses LLVM to compile a function which adds two numbers.
    /// </summary>
    internal static void CompileAdd(Logger log)
    {
        log.NewSection();
        log.Add("Starting LLVM Add Test");

        // Create our module and execution engine
        LLVMSharp.LLVMModuleRef module = LLVMSharp.LLVM.ModuleCreateWithName("root");

        // Add sum to the module
        LLVMSharp.LLVMTypeRef[] sum_param_types = { LLVMSharp.LLVM.Int32Type(), LLVMSharp.LLVM.Int32Type() };
        LLVMSharp.LLVMTypeRef sum_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), sum_param_types, false);
        LLVMSharp.LLVMValueRef sum = LLVMSharp.LLVM.AddFunction(module, "sum", sum_fn_type);

        // Entry/Exit point for the add function
        LLVMSharp.LLVMBasicBlockRef sum_entry = LLVMSharp.LLVM.AppendBasicBlock(sum, "entry");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVMBuilderRef builder = LLVMSharp.LLVM.CreateBuilder();
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, sum_entry);

        // Add the two parameters and return them
        LLVMSharp.LLVMValueRef sum_result = LLVMSharp.LLVM.BuildAdd(builder, LLVMSharp.LLVM.GetParam(sum, 0), LLVMSharp.LLVM.GetParam(sum, 1), "result");
        LLVMSharp.LLVM.BuildRet(builder, sum_result);

        // Entry / Exit point for MAIN
        LLVMSharp.LLVMTypeRef[] main_param_types = { };
        LLVMSharp.LLVMTypeRef main_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), main_param_types, false);
        LLVMSharp.LLVMValueRef main = LLVMSharp.LLVM.AddFunction(module, "main", main_fn_type);
        LLVMSharp.LLVMBasicBlockRef main_entry = LLVMSharp.LLVM.AppendBasicBlock(main, "entry");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, main_entry);

        // Run the sum function and return the value
        LLVMSharp.LLVMValueRef[] main_args = { 
            LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), 2, false),
            LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), 2, false)
        };
        LLVMSharp.LLVMValueRef main_tmp = LLVMSharp.LLVM.BuildCall(builder, sum, main_args, "result");
        LLVMSharp.LLVM.BuildRet(builder, main_tmp);
        
        // Ensure we didnt screw up...
        LLVMSharp.LLVM.VerifyModule(module, LLVMSharp.LLVMVerifierFailureAction.LLVMAbortProcessAction, out string error);
        if (error is not null) log.Add($"Module Verification Error: {error}");

        // Set the data layout
        LLVMSharp.LLVM.SetDataLayout(module, "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128");

        // Dispose of our builder
        log.Add("Disposing of our builder");
        LLVMSharp.LLVM.DisposeBuilder(builder);

        // Compile it!
        log.Add("Outputting bitcode to test_add.bc");
        if (LLVMSharp.LLVM.WriteBitcodeToFile(module, "test_add.bc") != 0) {
            log.Add("Error: error writing bitcode to file");
        }

        // Shutdown LLVM
        log.Add("Shutting Down LLVM");
        LLVMSharp.LLVM.Shutdown();
    }
}