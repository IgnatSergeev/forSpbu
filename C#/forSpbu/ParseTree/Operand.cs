namespace ParseTree;

/// <summary>
/// Class for working with tree nodes as operands(numbers)
/// </summary>
public class Operand : ParseTree.Node
{
    public Operand(int value)
    {
        _value = value;
    }
    
    public override double Evaluate()
    {
        return _value;
    }

    public override void Print() => Console.Write(_value);

    private readonly int _value;
}