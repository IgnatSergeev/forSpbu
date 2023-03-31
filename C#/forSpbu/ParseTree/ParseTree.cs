namespace ParseTree;

/// <summary>
/// Class for parsing text of prefix expression into tree and evaluating it
/// </summary>
public class ParseTree
{
    public abstract class Node
    {
        /// <summary>
        /// Calculates number of undertree expression
        /// </summary>
        /// <returns>Calculated number</returns>
        public abstract double Evaluate();
        /// <summary>
        /// Prints undertree expression
        /// </summary>
        public abstract void Print();
    }

    private static bool IsParentheses(char str)
    {
        return str is '(' or ')';
    }
    
    public ParseTree(string expression)
    {
        if (expression == null)
        {
            throw new ParseErrorException("Null expression");
        }
        
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

    /// <summary>
    /// Prints tree as prefix expression
    /// </summary>
    public void Print()
    {
        _head.Print();
        Console.WriteLine();
    }

    /// <summary>
    /// Evaluates expression stored in tree
    /// </summary>
    /// <returns>evaluated value</returns>
    public double Evaluate() => _head.Evaluate();

    private readonly Node _head;
}