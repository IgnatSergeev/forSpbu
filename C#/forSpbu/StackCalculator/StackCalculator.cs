namespace StackCalculator;

public static class StackCalculator
{
    private enum NodeKind
    {
        Operation,
        Number
    }
    
    private enum BinaryOperations
    {
        Mul,
        Add,
        Sub,
        Div
    }
    
    private static float EmitOperation(BinaryOperations operation, float firstArgument, float secondArgument)
    {
        return operation switch
        {
            BinaryOperations.Add => firstArgument + secondArgument,
            BinaryOperations.Sub => firstArgument - secondArgument,
            BinaryOperations.Div => firstArgument / secondArgument,
            _ => firstArgument * secondArgument
        };
    }
    
    private class Node
    {
        public Node(string inputString)
        {
            if (IsBinOperation(inputString))
            {
                Kind = NodeKind.Operation;
                OperationType = GetOperation(inputString);
            }
            else
            {
                if (!float.TryParse(inputString, out var value))
                {
                    throw new Exception("Error in parsing number, wrong input");
                }

                Kind = NodeKind.Number;
                NumberValue = value;
            }
        }
        
        private static bool IsBinOperation(string str)
        {
            return (str.Length == 1) && (str[0] == '-' || str[0] == '+' || str[0] == '/' || str[0] == '*');
        }

        private static BinaryOperations GetOperation(string str)
        {
            return (str[0] == '-')
                ? BinaryOperations.Sub
                : (str[0] == '+')
                    ? BinaryOperations.Add
                    : (str[0] == '/')
                        ? BinaryOperations.Div
                        : BinaryOperations.Mul;
        }
        
        public NodeKind Kind { get; }
        
        public float NumberValue { get; }

        public BinaryOperations OperationType { get; }
    }
    public static float Evaluate(string? inputString, Stack<float>? evaluationStack)
    {
        if (evaluationStack == null)
        {
            throw new Exception("Cannot evaluate using null stack");
        }
        
        if (inputString == null)
        {
            throw new Exception("Cannot evaluate null string");
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
                var secondArgument = evaluationStack.Top();
                evaluationStack.Pop();
                var firstArgument = evaluationStack.Top();
                evaluationStack.Pop();

                var result = EmitOperation(currentNode.OperationType, firstArgument, secondArgument);
                evaluationStack.Push(result);
            }
        }
        
        return evaluationStack.Top();
    }
}