namespace Sapling.Nodes;
using Sapling.Logging;
using System.Linq;

/// <summary>
/// A valid method call statement within the sapling programming language
/// </summary>
internal class SlMethodCall: SlStatement
{
    /// <summary>
    /// The identifier of the method call
    /// </summary>
    private string _identifier;

    /// <summary>
    /// The args of the method call
    /// </summary>
    private List<SlExpression> _args;

    /// <summary>
    /// The result of the method call
    /// </summary>
    private LLVMSharp.LLVMValueRef? _result;

    /// <summary>
    /// Construct a new SlMethodCall
    /// </summary>
    public SlMethodCall(Logger logger, string identifier, List<SlExpression> args, SlScope scope): base(logger, scope)
    {
        _identifier = identifier;
        _args = args;
    }

    /// <summary>
    /// Get the value returned by the code generation
    /// </summary>
    public LLVMSharp.LLVMValueRef GetResult(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        GenerateCode(module, builder, entry);

        if (_result is null) throw new Exception();
        return _result ?? default(LLVMSharp.LLVMValueRef);
    }

    /// <summary>
    /// Generate code for a LLVM method call
    /// <example>
    public override void GenerateCode(LLVMSharp.LLVMModuleRef module, LLVMSharp.LLVMBuilderRef builder, LLVMSharp.LLVMBasicBlockRef entry)
    {
        Logger.Add("Generating code for SlMethodCall");

        // Ensure that we have the right args, and convert them to the right type for llvm
        LLVMSharp.LLVMTypeRef fType = Scope.GetFunctionType(Logger, _identifier);

        // Retrieve the argument types
        LLVMSharp.LLVMTypeRef[] argTypes = LLVMSharp.LLVM.GetParamTypes(fType);

        // Check for wrong arg count
        if (argTypes.Length != _args.Count) throw new Exception($"Wrong number of args supplied to method {_identifier}.");

        // Get the type of all suppliedArgTypes
        LLVMSharp.LLVMTypeRef[] suppliedArgTypes = _args
            .Select(element => Scope.FindType(Logger, element.ExType))
            .ToArray();

        // Get a list of pairs of elements with a type mismatch
        List<Tuple<LLVMSharp.LLVMTypeRef, LLVMSharp.LLVMTypeRef>> unequalElements = argTypes
            .Zip(suppliedArgTypes, (e, s) => Tuple.Create(e, s))
            .Where(pair => !pair.Item1.Equals(pair.Item2))
            .ToList();
        
        // If there are any elements which do not match the expected type log the difference and then throw and exception
        if (unequalElements.Any()) {
            foreach (Tuple<LLVMSharp.LLVMTypeRef, LLVMSharp.LLVMTypeRef> pair in unequalElements) Logger.Add($"Supplied type {pair.Item2.ToString()} did not match expected type {pair.Item1.ToString()}");
            throw new Exception($"Type mismatch of parameters supplied to method {_identifier}");
        }

        // Call the method and get the result (if it is not a void return type)
        LLVMSharp.LLVMValueRef[] suppliedArgs = _args.Select(element => element.GenerateValue(builder, module, entry)).ToArray();

        if (fType.GetReturnType().Equals(LLVMSharp.LLVM.VoidType()))
        {
            LLVMSharp.LLVM.BuildCall(builder, Scope.GetFunction(Logger, _identifier), suppliedArgs, string.Empty);
            return;
        }

        LLVMSharp.LLVMValueRef result = LLVMSharp.LLVM.BuildCall(builder, Scope.GetFunction(Logger, _identifier), suppliedArgs, $"{_identifier}_result");

        // Store the result so we can use it if we are parsing the method call as an expression
        _result = result;
    }
}