namespace ParseTree;

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

    private readonly double _value;
}