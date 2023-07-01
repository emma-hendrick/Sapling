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
    /// The global scope of the AST.
    /// </summary>
    private SlScope _global;

    /// <summary>
    /// The logger to use.
    /// </summary>
    private Logger _logger;

    /// <summary>
    /// The module to use.
    /// </summary>
    private LLVMSharp.LLVMModuleRef _module;

    /// <summary>
    /// The builder to use.
    /// </summary>
    private LLVMSharp.LLVMBuilderRef _builder;

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
    public AST(SlMethod root, SlScope global, Logger logger)
    {
        _logger = logger;
        _global = global;
        _root = root;

        Initialize();
    }

    private void Initialize()
    {
        // Open new section to generate LLVM
        _logger.Add("Initializing AST");

        // Create our module
        _module = LLVMSharp.LLVM.ModuleCreateWithName("root");
        LLVMSharp.LLVM.SetTarget(_module, Environment.Target);

        // Declare the printf function
        LLVMSharp.LLVMTypeRef[] printfParamTypes = {LLVMSharp.LLVMTypeRef.PointerType(LLVMSharp.LLVMTypeRef.Int8Type(), 0)};
        LLVMSharp.LLVMTypeRef printfType = LLVMSharp.LLVMTypeRef.FunctionType(LLVMSharp.LLVMTypeRef.Int32Type(), printfParamTypes, true);
        string printfName = "printf";
        LLVMSharp.LLVMValueRef printfFunc = LLVMSharp.LLVM.AddFunction(_module, printfName, printfType);
        _global.AddFunctionType(_logger, printfName, printfType);
        _global.AddFunction(_logger, printfName, printfFunc);

        // Declare the getchar function
        LLVMSharp.LLVMTypeRef[] getcharParamTypes = {};
        LLVMSharp.LLVMTypeRef getcharType = LLVMSharp.LLVMTypeRef.FunctionType(LLVMSharp.LLVMTypeRef.Int32Type(), getcharParamTypes, false);
        string getcharName = "getchar";
        LLVMSharp.LLVMValueRef getcharFunc = LLVMSharp.LLVM.AddFunction(_module, getcharName, getcharType);
        _global.AddFunctionType(_logger, getcharName, getcharType);
        _global.AddFunction(_logger, getcharName, getcharFunc);

        // We use this to add instructions to the functions block
        _builder = LLVMSharp.LLVM.CreateBuilder();
    }

    /// <summary>
    /// Generate the LLVM bitcode for the nodes in the tree.
    /// <example>
    public void GenerateCode(string filename)
    {
        // Open new section to generate LLVM
        _logger.NewSection();
        _logger.Add("Generating LLVM");

        // Entry / Exit point for MAIN
        LLVMSharp.LLVMTypeRef[] main_param_types = { };
        LLVMSharp.LLVMTypeRef main_fn_type = LLVMSharp.LLVM.FunctionType(LLVMSharp.LLVM.Int32Type(), main_param_types, false);
        LLVMSharp.LLVMValueRef main = LLVMSharp.LLVM.AddFunction(_module, "main", main_fn_type);

        // Create the main entry point  
        _logger.IncreaseIndent();
        _logger.Add("Adding Basic Block: \"main_entry\"");
        LLVMSharp.LLVMBasicBlockRef main_entry = LLVMSharp.LLVM.AppendBasicBlock(main, "main_entry");

        // We use this to add instructions to the functions block
        LLVMSharp.LLVM.PositionBuilderAtEnd(_builder, main_entry);
        
        // Now we will execute the code of the root node
        _root.GenerateCode(_module, _builder, main_entry, main);

        // Ensure we didnt screw up...
        _logger.Add("Verifying Module");
        LLVMSharp.LLVM.VerifyModule(_module, LLVMSharp.LLVMVerifierFailureAction.LLVMAbortProcessAction, out string error);
        if (error is not null) _logger.Add($"Module Verification Error: {error}");

        // Set the data layout
        LLVMSharp.LLVM.SetDataLayout(_module, "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128");

        // Dispose of our builder
        _logger.Add("Disposing of our builder");
        LLVMSharp.LLVM.DisposeBuilder(_builder);

        // Compile it!
        _logger.Add($"Outputting bitcode to {filename}");
        if (LLVMSharp.LLVM.WriteBitcodeToFile(_module, filename) != 0) {

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