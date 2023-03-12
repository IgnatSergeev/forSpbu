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
    
    private static int EmitOperation(BinaryOperations operation, int firstArgument, int secondArgument)
    {
        switch (operation)
        {
            case BinaryOperations.Add:
                return firstArgument + secondArgument;
            case BinaryOperations.Sub:
                return firstArgument - secondArgument;
            case BinaryOperations.Div:
                return firstArgument / secondArgument;
            default:
                return firstArgument * secondArgument;
        }
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
                if (!int.TryParse(inputString, out var value))
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
        
        public int NumberValue { get; }

        public BinaryOperations OperationType { get; }
    }
    public static int Evaluate(string? inputString, Stack evaluationStack)
    {
        if (inputString == null)
        {
            throw new Exception("Cannot evaluate null string");
        }
        
        var splitedString = inputString.Split();

        var size = splitedString.Length;
        evaluationStack.Clear();
        for (var i = 0; i < size; i++)
        {
            var currentNode = new Node(splitedString[i]);
            if (currentNode.Kind == NodeKind.Number)
            {
                evaluationStack.Push(currentNode.NumberValue);
            }
            else
            {
                int secondArgument = evaluationStack.Top();
                evaluationStack.Pop();
                int firstArgument = evaluationStack.Top();
                evaluationStack.Pop();

                var result = EmitOperation(currentNode.OperationType, firstArgument, secondArgument);
                evaluationStack.Push(result);
            }
        }
        
        return evaluationStack.Top();
    }
}