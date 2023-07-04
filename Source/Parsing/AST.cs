namespace Sapling;
using Sapling.Logging;
using Sapling.Nodes;
using System.Runtime.InteropServices;

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
    /// The function names which correspond to each bif, in case the names change, and we don't want to refactor everything.
    /// </summary>
    private Dictionary<string, string> _BIFNames = new Dictionary<string, string>{
        {"printf", "printf"},
        {"getchar", "getchar"},
        {"flush_stdin", "flush_stdin"},
        {"getstr", "getstr"},
    };

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
    public AST(SlMethod root, LLVMSharp.LLVMModuleRef module, SlScope global, Logger logger)
    {
        _logger = logger;
        _global = global;
        _root = root;
        _module = module;

        Initialize();
    }

    /// <summary>
    /// Initialize the ast. This includes the module, target, bifs, and the builder.
    /// </summary>
    private void Initialize()
    {
        // Open new section to generate LLVM
        _logger.Add("Initializing AST");

        // Create our module
        LLVMSharp.LLVM.SetTarget(_module, Environment.Target);

        // Initialize the LLVM builder
        _builder = LLVMSharp.LLVM.CreateBuilder();

        // Include Saplings Built in Functions
        IncludeDefaultBIFs();
    }

    /// <summary>
    /// Include the default built in functions.
    /// </summary>
    private void IncludeDefaultBIFs()
    {
        AddPrintf();
        AddGetChar();
        AddGetStr();
        AddFlushStdin();
    }

    /// <summary>
    /// Add printf to our module and the global scope
    /// </summary>
    private void AddPrintf()
    {
        // Declare the printf function
        LLVMSharp.LLVMTypeRef[] printfParamTypes = {LLVMSharp.LLVMTypeRef.PointerType(LLVMSharp.LLVMTypeRef.Int8Type(), 0)};
        LLVMSharp.LLVMTypeRef printfType = LLVMSharp.LLVMTypeRef.FunctionType(LLVMSharp.LLVMTypeRef.Int32Type(), printfParamTypes, true);
        string printfName = _BIFNames["printf"];
        LLVMSharp.LLVMValueRef printfFunc = LLVMSharp.LLVM.AddFunction(_module, printfName, printfType);

        // Add printf to the global scope
        _global.AddFunctionType(_logger, printfName, printfType);
        _global.AddFunction(_logger, printfName, printfFunc);
    }

    /// <summary>
    /// Add getchar to our module and the global scope
    /// </summary>
    private void AddGetChar()
    {
        // Declare the getchar function
        LLVMSharp.LLVMTypeRef[] getcharParamTypes = {};
        LLVMSharp.LLVMTypeRef getcharType = LLVMSharp.LLVMTypeRef.FunctionType(LLVMSharp.LLVMTypeRef.Int32Type(), getcharParamTypes, false);
        string getcharName = _BIFNames["getchar"];
        LLVMSharp.LLVMValueRef getcharFunc = LLVMSharp.LLVM.AddFunction(_module, getcharName, getcharType);

        // Add getchar to the global scope
        _global.AddFunctionType(_logger, getcharName, getcharType);
        _global.AddFunction(_logger, getcharName, getcharFunc);
    }

    /// <summary>
    /// Add getstr to our module and the global scope
    /// </summary>
    private void AddGetStr()
    {
        // TODO
        // // Declare the getstr function
        // LLVMSharp.LLVMTypeRef[] getstrParamTypes = {};
        // LLVMSharp.LLVMTypeRef getstrType = LLVMSharp.LLVMTypeRef.FunctionType(_global.FindType(_logger, "str"), getstrParamTypes, false);
        // string getstrName = _BIFNames["getstr"];
        // LLVMSharp.LLVMValueRef getstrFunc = LLVMSharp.LLVM.AddFunction(_module, getstrName, getstrType);

        // // Add the getstr function to the global scope
        // _global.AddFunctionType(_logger, getstrName, getstrType);
        // _global.AddFunction(_logger, getstrName, getstrFunc);
    }

    /// <summary>
    /// Add flush_stdin to our module and the global scope
    /// </summary>
    private void AddFlushStdin()
    {
        // Declare the flush_stdin function
        LLVMSharp.LLVMTypeRef[] flushStdinParamTypes = {};
        LLVMSharp.LLVMTypeRef flushStdinType = LLVMSharp.LLVMTypeRef.FunctionType(LLVMSharp.LLVMTypeRef.VoidType(), flushStdinParamTypes, false);
        string flushStdinName = _BIFNames["flush_stdin"];
        LLVMSharp.LLVMValueRef flushStdinFunc = LLVMSharp.LLVM.AddFunction(_module, flushStdinName, flushStdinType);

        // The functions blocks
        LLVMSharp.LLVMBasicBlockRef entry = LLVMSharp.LLVM.AppendBasicBlock(flushStdinFunc, "entry");
        LLVMSharp.LLVMBasicBlockRef loop = LLVMSharp.LLVM.AppendBasicBlock(flushStdinFunc, "loop");
        LLVMSharp.LLVMBasicBlockRef done = LLVMSharp.LLVM.AppendBasicBlock(flushStdinFunc, "done");

        // The entry block
        LLVMSharp.LLVM.PositionBuilderAtEnd(_builder, entry);
        LLVMSharp.LLVM.BuildBr(_builder, loop);

        // The loop block
        LLVMSharp.LLVM.PositionBuilderAtEnd(_builder, loop);
        LLVMSharp.LLVMValueRef ch = LLVMSharp.LLVM.BuildCall(_builder, _global.GetFunction(_logger, _BIFNames["getchar"]), new LLVMSharp.LLVMValueRef [0], "ch");
        LLVMSharp.LLVMValueRef isEof = LLVMSharp.LLVM.BuildICmp(_builder, LLVMSharp.LLVMIntPredicate.LLVMIntEQ, ch, LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), unchecked((ulong)-1), false), "isEof");
        LLVMSharp.LLVMValueRef isNewline = LLVMSharp.LLVM.BuildICmp(_builder, LLVMSharp.LLVMIntPredicate.LLVMIntEQ, ch, LLVMSharp.LLVM.ConstInt(LLVMSharp.LLVM.Int32Type(), (ulong)10, false), "isNewline");

        // Check whether or not we should loop depending on what the last character we got from the stdin buffer was
        LLVMSharp.LLVMValueRef shouldLoop = LLVMSharp.LLVM.BuildOr(_builder, isEof, isNewline, "shouldLoop");
        LLVMSharp.LLVM.BuildCondBr(_builder, shouldLoop, done, loop);

        // The done block
        LLVMSharp.LLVM.PositionBuilderAtEnd(_builder, done);
        LLVMSharp.LLVM.BuildRetVoid(_builder);

        // Add the flush_stdin function to the global scope
        _global.AddFunctionType(_logger, flushStdinName, flushStdinType);
        _global.AddFunction(_logger, flushStdinName, flushStdinFunc);
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

        _logger.NewSection();
        IntPtr moduleIntPointer = LLVMSharp.LLVM.PrintModuleToString(_module);
        string moduleString = Marshal.PtrToStringAnsi(moduleIntPointer) ?? "";
        foreach (string line in moduleString.Split('\n')) _logger.Add(line);
        _logger.NewSection();

        // Ensure we didnt screw up...
        _logger.Add("Verifying Module");
        LLVMSharp.LLVM.VerifyModule(_module, LLVMSharp.LLVMVerifierFailureAction.LLVMReturnStatusAction, out string error);
        if (error is not null) throw new Exception($"Module Verification Error: {error}");

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