namespace Sapling.Nodes;
using Sapling.Logging;

/// <summary>
/// </summary>
internal class SlOperator: SlNode, IShuntingYardable
{
    private string _optype;
    public string OpType => _optype;

    // Link a list of which types return which type to each operator
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
    };

    public SlOperator(Logger logger, string optype, SlScope scope): base(logger, scope)
    {
        // Ensure that operator is valid
        if (!OpTypeReturns.ContainsKey(optype)) throw new Exception($"Invalid OpType {optype}");
        _optype = optype;
    }

    public string GetReturnType(string op1_type, string op2_type)
    {
        // Get the return type by operator and operand types
        Tuple<string, string> operators = Tuple.Create(Constants.EquivalentTypes[op1_type], Constants.EquivalentTypes[op2_type]);

        if (!OpTypeReturns[_optype].ContainsKey(operators)) throw new Exception($"Invalid operand types {operators.ToString()}");
        return OpTypeReturns[_optype][operators];
    }
}