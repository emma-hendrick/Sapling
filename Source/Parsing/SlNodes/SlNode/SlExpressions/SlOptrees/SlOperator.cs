namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// A valid operator in the sapling programming language
/// </summary>
internal class SlOperator: SlNode, IShuntingYardable
{
    /// <summary>
    /// The operator type
    /// </summary>
    private string _optype;

    /// <summary>
    /// The operator type
    /// </summary>
    public string OpType => _optype;

    /// <summary>
    /// A dictionary of valid operators and their input and return types
    /// </summary>
    public Dictionary<string, Dictionary<Tuple<string, string>, string>> OpTypeReturns = new Dictionary<string, Dictionary<Tuple<string, string>, string>> 
    {
        {"+", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "int"},
        }},
        {"*", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "int"},
        }},
        {"-", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "int"},
        }},
        {"/", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "int"},
        }},
        {"&&", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("bool", "bool"), "bool"},
        }},
        {"||", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("bool", "bool"), "bool"},
        }},
        {"^", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("bool", "bool"), "bool"},
        }},
        {"==", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
        {"!=", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
        {"<", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
        {"<=", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
        {">", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
        {">=", new Dictionary<Tuple<string, string>, string> {
            {Tuple.Create("int", "int"), "bool"},
        }},
    };

    /// <summary>
    /// Construct a new SlOperator
    /// </summary>
    public SlOperator(Logger logger, LLVMSharp.LLVMModuleRef module, string optype, SlScope scope): base(logger, module, scope)
    {
        // Ensure that operator is valid
        if (!OpTypeReturns.ContainsKey(optype)) throw new Exception($"Invalid OpType {optype}");
        _optype = optype;
    }

    /// <summary>
    /// Get the return type of an SlOperator given the operand types
    /// </summary>
    public string GetReturnType(string op1_type, string op2_type)
    {
        // Get the return type by operator and operand types
        Tuple<string, string> operators = Tuple.Create(Constants.EquivalentExpressionTypes[op1_type], Constants.EquivalentExpressionTypes[op2_type]);

        if (!OpTypeReturns[_optype].ContainsKey(operators)) throw new Exception($"Invalid operand types {operators.ToString()}");
        return OpTypeReturns[_optype][operators];
    }
}