namespace ParseTree;

/// <summary>
/// Class for working with tree nodes as operators
/// </summary>
public class Operator : ParseTree.Node
{
    /// <summary>
    /// Enum with possible operators
    /// </summary>
    public enum Operators
    {
        Sub,
        Add,
        Mul,
        Div
    }

    /// <summary>
    /// Creates operator node with given parameters
    /// </summary>
    /// <param name="operator">Node operator</param>
    /// <param name="firstOperand">First node`s operand</param>
    /// <param name="secondOperand">Second node`s operand</param>
    public Operator(Operators @operator, ParseTree.Node firstOperand, ParseTree.Node secondOperand)
    {
        _firstOperand = firstOperand;
        _secondOperand = secondOperand;
        _operator = @operator;
    }
    
    private static bool IsZero(double value)
    {
        return value is >= -0.00001 and <= 0.0001;
    }
    
    private static double EvaluateOperator(Operators @operator, double firstOperand, double secondOperand)
    {
        return @operator switch
        {
            Operators.Add => firstOperand + secondOperand,
            Operators.Sub => firstOperand - secondOperand,
            Operators.Mul => firstOperand * secondOperand,
            Operators.Div => !IsZero(secondOperand) ? firstOperand / secondOperand : throw new DivideByZeroException(),
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };
    }

    private static void PrintOperator(Operators @operator)
    {
        Console.Write(
            @operator switch
        {
            Operators.Add => '+',
            Operators.Sub => '-',
            Operators.Mul => '*',
            Operators.Div => '/',
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        });
    }

    public override double Evaluate()
    {
        var evaluatedFirstOperand = _firstOperand.Evaluate();
        var evaluatedSecondOperand = _secondOperand.Evaluate();
        return EvaluateOperator(_operator, evaluatedFirstOperand, evaluatedSecondOperand);
    }

    public override void Print()
    {
        Console.Write("(");
        PrintOperator(_operator);
        Console.Write(" ");
        _firstOperand.Print();
        Console.Write(" ");
        _secondOperand.Print();
        Console.Write(")");
    }
    
    private readonly Operators _operator;
    private readonly ParseTree.Node _firstOperand;
    private readonly ParseTree.Node _secondOperand;
}