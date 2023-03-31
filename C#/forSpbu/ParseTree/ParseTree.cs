namespace ParseTree;

public class ParseTree
{
    public abstract class Node
    {
        public abstract double Evaluate();
        public abstract void Print();
    }

    private static bool IsParentheses(char str)
    {
        return str is '(' or ')';
    }
    
    public ParseTree(string expression)
    {
        var expressionList = new List<char>(expression.ToArray());
        expressionList.RemoveAll(IsParentheses);
        var expressionArrayWithoutParentheses = new string(expressionList.ToArray()).Split();

        if (expressionArrayWithoutParentheses.Length > 1)
        {
            _head = new Operator(expressionArrayWithoutParentheses, 0, expressionArrayWithoutParentheses.Length);
        }
        else
        {
            _head = new Operand(expressionArrayWithoutParentheses, 0);
        }
    }

    public void Print()
    {
        _head.Print();
        Console.WriteLine();
    }

    public double Evaluate() => _head.Evaluate();

    private readonly Node _head;
}