using System.Globalization;

namespace StackCalculator;

public static class StackCalculator
{
    private enum NodeKind
    {
        Operation,
        Number
    }
    
    private enum BinaryOperators
    {
        Mul,
        Add,
        Sub,
        Div
    }
    
    private static double EmitOperation(BinaryOperators @operator, double firstArgument, double secondArgument)
    {
        if (secondArgument is >= -0.0001 and <= 0.0001 && @operator == BinaryOperators.Div)
        {
            throw new DivideByZeroException();
        }
        
        return @operator switch
        {
            BinaryOperators.Add => firstArgument + secondArgument,
            BinaryOperators.Sub => firstArgument - secondArgument,
            BinaryOperators.Div => firstArgument / secondArgument,
            _ => firstArgument * secondArgument
        };
    }
    
    private class Node
    {
        public Node(string inputString)
        {
            if (IsBinOperator(inputString))
            {
                Kind = NodeKind.Operation;
                _operatorType = GetOperator(inputString);
            }
            else
            {
                if (!double.TryParse(inputString, new CultureInfo(2), out var value))
                {
                    throw new ParseException();
                }

                Kind = NodeKind.Number;
                _numberValue = value;
            }
        }
        
        private static bool IsBinOperator(string str)
        {
            return (str.Length == 1) && (str[0] == '-' || str[0] == '+' || str[0] == '/' || str[0] == '*');
        }

        private static BinaryOperators GetOperator(string str)
        {
            return str[0] switch
            {
                '-' => BinaryOperators.Sub,
                '+' => BinaryOperators.Add,
                '/' => BinaryOperators.Div,
                _ => BinaryOperators.Mul
            };
        }
        
        private readonly double _numberValue;
        private readonly BinaryOperators _operatorType;
        public NodeKind Kind { get; }
        public double NumberValue => 
            (Kind == NodeKind.Operation) ? throw new FieldException(nameof(NumberValue)) : _numberValue;
        public BinaryOperators OperatorType =>
            (Kind == NodeKind.Number) ? throw new FieldException(nameof(OperatorType)) : _operatorType;
    }
    
    public static (double, bool) Evaluate(string inputString, IStack<double> evaluationStack)
    {
        if (evaluationStack == null)
        {
            throw new ArgumentNullException(nameof(evaluationStack));
        }
        
        if (inputString == null)
        {
            throw new ArgumentNullException(nameof(inputString));
        }
        
        var splittedString = inputString.Split();

        var size = splittedString.Length;
        evaluationStack.Clear();
        for (var i = 0; i < size; i++)
        {
            var currentNode = new Node(splittedString[i]);
            if (currentNode.Kind == NodeKind.Number)
            {
                evaluationStack.Push(currentNode.NumberValue);
            }
            else
            {
                try
                {
                    var secondArgument = evaluationStack.Top();
                    evaluationStack.Pop();
                    var firstArgument = evaluationStack.Top();
                    evaluationStack.Pop();
                    
                    var result = EmitOperation(currentNode.OperatorType, firstArgument, secondArgument);
                    evaluationStack.Push(result);
                }
                catch (ArgumentNullException)
                {
                    return (0, false);
                }
            }
        }

        var evaluationResult = evaluationStack.Top();
        evaluationStack.Pop();
        return !evaluationStack.IsEmpty() ? (0, false) : (evaluationResult, true);
    }
}