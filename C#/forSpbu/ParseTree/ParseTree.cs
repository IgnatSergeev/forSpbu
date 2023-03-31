namespace ParseTree;

public class ParseTree
{
    public abstract class Node
    {
        public abstract double Evaluate();
        public abstract void Print();
    }

    private static bool IsParentheses(string str)
    {
        return str.Length == 1 && (str[0] == '(' || str[0] == ')');
    }
    
    public ParseTree(string expression)
    {
        var splitExpression = expression.Split();
        var expressionList = new List<string>(splitExpression);
        expressionList.RemoveAll(IsParentheses);
        var expressionWithoutParentheses = expressionList.ToArray();

        _head = new Operator(expressionWithoutParentheses, 0, expression.Length);
    }
    
    private Node _head;
}