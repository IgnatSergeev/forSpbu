namespace ParseTree;

public class Operand : ParseTree.Node
{
    public Operand(string[] expression, int index)
    {
        if (expression == null || expression.Length == 0)
        {
            throw new ParseErrorException("Null expression");
        }
        if (expression.Length <= index)
        {
            throw new ParseErrorException("Wrong expression index");
        }

        if (!int.TryParse(expression[index], out var value))
        {
            throw new ParseErrorException("Error in parsing int");
        }
        _value = value;
    }
    
    public override double Evaluate()
    {
        return _value;
    }

    public override void Print() => Console.Write(_value);

    private readonly int _value;
}