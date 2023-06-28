namespace Sapling;
using Sapling.Logging;
using Sapling.Nodes;

/// <summary>
/// Class <c>AST</c> holds a set of nodes in a way which conveys execution order of tokens.
/// </summary>
internal class AST
{
    /// <summary>
    /// The root node of the AST.
    /// </summary>
    private SlMethod _root;

    /// <summary>
    /// The logger to use.
    /// </summary>
    private Logger _logger;

    /// <summary>
    /// This construsts a new AST.
    /// <example>
    /// For example:
    /// <code>
    /// AST ast = new AST(log);
    /// </code>
    /// will create a new AST, which can then be used to generate the LLVM bitcode for its nodes.
    /// </example>
    /// </summary>
    public AST(SlMethod root, Logger logger)
    {
        _logger = logger;
        _root = root;
    }

    /// <summary>
    /// Generate the LLVM bitcode for the nodes in the tree.
    /// <example>
    public void GenerateCode(string filename)
    {
        // Open new section to generate LLVM
        _logger.NewSection();
        _logger.Add("Generating LLVM");

        // Create our module
        LLVMSharp.LLVMModuleRef module = LLVMSharp.LLVM.ModuleCreateWithName("root");
        LLVMSharp.LLVM.SetTarget(module, Environment.Target);

        // We use this to add instructions to the functions block
        LLVMSharp.LLVMBuilderRef builder = LLVMSharp.LLVM.CreateBuilder();

        // Entry / Exit point for MAIN
        LLVMSharp.LLVMTypeRef[] main_param_types = { };
        LLVMSharp.LLVMTypeRef main_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), main_param_types, false);
        LLVMSharp.LLVMValueRef main = LLVMSharp.LLVM.AddFunction(module, "main", main_fn_type);
        _logger.IncreaseIndent();
        _logger.Add("Adding Basic Block: \"main_entry\"");
        LLVMSharp.LLVMBasicBlockRef main_entry = LLVMSharp.LLVM.AppendBasicBlock(main, "main_entry");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(builder, main_entry);
        
        // Now we will execute the code of the root node
        _root.GenerateCode(module, builder, main_entry, main);
        
        // Ensure we didnt screw up...
        _logger.Add("Verifying Module");
        LLVMSharp.LLVM.VerifyModule(module, LLVMSharp.LLVMVerifierFailureAction.LLVMAbortProcessAction, out string error);
        if (error is not null) _logger.Add($"Module Verification Error: {error}");

        // Set the data layout
        LLVMSharp.LLVM.SetDataLayout(module, "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128");

        // Dispose of our builder
        _logger.Add("Disposing of our builder");
        LLVMSharp.LLVM.DisposeBuilder(builder);

        // Compile it!
        _logger.Add($"Outputting bitcode to {filename}");
        if (LLVMSharp.LLVM.WriteBitcodeToFile(module, filename) != 0) {

            // Shutdown LLVM
            _logger.Add("Error Occurred, Shutting Down LLVM");
            LLVMSharp.LLVM.Shutdown();

            throw new Exception("Error writing bitcode to file");
        }

        // Shutdown LLVM
        _logger.Add("Shutting Down LLVM");
        LLVMSharp.LLVM.Shutdown();
    }
}