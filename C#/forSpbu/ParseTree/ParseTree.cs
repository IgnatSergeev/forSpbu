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

    private static int ParseOperand(string[] expression, int index)
    {
        if (!int.TryParse(expression[index], out var value))
        {
            throw new ParseErrorException("Error in parsing int");
        }

        return value;
    }

    private static Operator.Operators GetOperator(char symbol)
    {
        return symbol switch
        {
            '-' => Operator.Operators.Sub,
            '/' => Operator.Operators.Div,
            '+' => Operator.Operators.Add,
            '*' => Operator.Operators.Mul,
            _ => throw new ParseErrorException("Unknown Operator")
        };
    }

    private static bool IsOperator(string str)
    {
        return str is ['+' or '-' or '*' or '/'];
    }
    
    private static (Operator.Operators, Node, Node) ParseOperator(string[] expression, int startIndex, int endIndex)
    {
        if (IsOperator(expression[startIndex]))
        {
            var @operator = GetOperator(expression[startIndex][0]);
            Node firstOperand;
            Node secondOperand;

            var firstOperandEndIndex = 0;
            var expectedNumOfValues = 1;
            for (var currentIndex = startIndex + 1; currentIndex < endIndex; currentIndex++)
            {
                if (expression[currentIndex] == null)
                {
                    throw new ParseErrorException("Null operand");
                }
                
                if (IsOperator(expression[currentIndex]))
                {
                    ++expectedNumOfValues;
                    continue;
                }

                if (!int.TryParse(expression[currentIndex], out _))
                {
                    throw new ParseErrorException("Expected int or operator given unknown entity");
                }
                --expectedNumOfValues;
                if (expectedNumOfValues == 0)
                {
                    firstOperandEndIndex = currentIndex + 1;
                    break;
                }
            }

            if (expectedNumOfValues != 0)
            {
                throw new ParseErrorException("Wrong expression construction(not enough operands)"); 
            }

            var firstOperandStartIndex = startIndex + 1;
            if (firstOperandStartIndex + 1 == firstOperandEndIndex)
            {
                firstOperand = new Operand(ParseOperand(expression, firstOperandStartIndex));
            }
            else
            {
                var parsedOperator = ParseOperator(expression, firstOperandStartIndex, firstOperandEndIndex);
                firstOperand = new Operator(parsedOperator.Item1, parsedOperator.Item2, parsedOperator.Item3);
            }

            var secondOperandStartIndex = firstOperandEndIndex;
            var secondOperandEndIndex = endIndex;
            if (secondOperandStartIndex + 1 == secondOperandEndIndex)
            {
                secondOperand = new Operand(ParseOperand(expression, secondOperandStartIndex));
            }
            else
            {
                var parsedOperator = ParseOperator(expression, secondOperandStartIndex, secondOperandEndIndex);
                secondOperand = new Operator(parsedOperator.Item1, parsedOperator.Item2, parsedOperator.Item3);
            }

            return (@operator, firstOperand, secondOperand);
        }
        
        throw new ParseErrorException("Expected operator given unknown entity");
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
            var headOperator = ParseOperator(expressionArrayWithoutParentheses, 0, expressionArrayWithoutParentheses.Length);
            _head = new Operator(headOperator.Item1, headOperator.Item2, headOperator.Item3);
        }
        else
        {
            _head = new Operand(ParseOperand(expressionArrayWithoutParentheses, 0));
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