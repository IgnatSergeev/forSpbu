using System.Diagnostics;

namespace ParseTree;

public class Operator : ParseTree.Node
{
    private enum Operators
    {
        Sub,
        Add,
        Mul,
        Div
    }

    private static Operators GetOperator(char symbol)
    {
        return symbol switch
        {
            '-' => Operators.Sub,
            '/' => Operators.Div,
            '+' => Operators.Add,
            '*' => Operators.Mul,
            _ => throw new ParseErrorException("Unknown Operator")
        };
    }

    private static double EvaluateOperator(Operators @operator, double firstOperand, double secondOperand)
    {
        return @operator switch
        {
            Operators.Add => firstOperand + secondOperand,
            Operators.Sub => firstOperand - secondOperand,
            Operators.Mul => firstOperand * secondOperand,
            _ => firstOperand / secondOperand,
        };
    }

    public Operator(string[] expression, int startIndex, int endIndex)
    {
        
        
        
    }
    public Operator(char @operator, ParseTree.Node firstOperand, ParseTree.Node secondOperand)
    {
        _operator = GetOperator(@operator);
        _firstOperand = firstOperand;
        _secondOperand = secondOperand;
    }

    public override double Evaluate()
    {
        var evaluatedFirstOperand = _firstOperand.Evaluate();
        var evaluatedSecondOperand = _secondOperand.Evaluate();
        return EvaluateOperator(_operator, evaluatedFirstOperand, evaluatedSecondOperand);
    }

    private readonly Operators _operator;
    private readonly ParseTree.Node _firstOperand;
    private readonly ParseTree.Node _secondOperand;
}