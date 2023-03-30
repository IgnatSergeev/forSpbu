namespace ParseTree;

public class ParseTree
{
    public abstract class Node
    {
        public abstract double Evaluate();
    }

    public ParseTree(string expression)
    {
        var splitString = expression.Split();
        
    }
    
    private Node _head;
}