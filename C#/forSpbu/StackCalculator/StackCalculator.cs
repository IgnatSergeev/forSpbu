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
    
    private static float EmitOperation(BinaryOperators @operator, float firstArgument, float secondArgument)
    {
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
                if (!float.TryParse(inputString, out var value))
                {
                    throw new Exception("Error in parsing number, wrong input");
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
        
        private readonly float _numberValue;
        private readonly BinaryOperators _operatorType;
        public NodeKind Kind { get; }
        public float NumberValue => 
            (Kind == NodeKind.Operation) ? throw new FieldException(nameof(NumberValue)) : _numberValue;
        public BinaryOperators OperatorType =>
            (Kind == NodeKind.Number) ? throw new FieldException(nameof(OperatorType)) : _operatorType;
    }
    
    public static (float, bool) Evaluate(string inputString, IStack<float> evaluationStack)
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
        return evaluationStack.IsEmpty() ? (0, false) : (evaluationResult, true);
    }
}