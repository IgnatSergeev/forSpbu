﻿using System.Diagnostics;
using System.Net;

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

    private static bool IsOperator(string str)
    {
        return str is ['+' or '-' or '*' or '/'];
    }

    private static bool IsZero(double value)
    {
        return value is >= -0.00001 and <= 0.0001;
    }
    
    private static double EvaluateOperator(Operators @operator, double firstOperand, double secondOperand)
    {
        return @operator switch
        {
            Operators.Add => firstOperand + secondOperand,
            Operators.Sub => firstOperand - secondOperand,
            Operators.Mul => firstOperand * secondOperand,
            Operators.Div => !IsZero(secondOperand) ? firstOperand / secondOperand : throw new DivideByZeroException(),
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };
    }

    private static void PrintOperator(Operators @operator)
    {
        Console.Write(
            @operator switch
        {
            Operators.Add => '+',
            Operators.Sub => '-',
            Operators.Mul => '*',
            Operators.Div => '/',
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        });
    }
    
    public Operator(string[] expression, int startIndex, int endIndex)
    {
        if (expression == null || expression.Length == 0)
        {
            throw new ParseErrorException("Null expression");
        }
        if (startIndex >= endIndex)
        {
            throw new ParseErrorException("Null operand");
        }
        if (expression.Length < endIndex)
        {
            throw new ParseErrorException("Wrong index");
        }
        if (expression[startIndex] == null)
        {
            throw new ParseErrorException("Null operand");
        }

        if (IsOperator(expression[startIndex]))
        {
            _operator = GetOperator(expression[startIndex][0]);
            
            int firstOperandEndIndex = 0;
            int expectedNumOfValues = 1;
            for (int currentIndex = startIndex + 1; currentIndex < endIndex; currentIndex++)
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

            int firstOperandStartIndex = startIndex + 1;
            if (firstOperandStartIndex + 1 == firstOperandEndIndex)
            {
                _firstOperand = new Operand(expression, firstOperandStartIndex);
            }
            else
            {
                _firstOperand = new Operator(expression, firstOperandStartIndex, firstOperandEndIndex);
            }

            int secondOperandStartIndex = firstOperandEndIndex;
            int secondOperandEndIndex = endIndex;
            if (secondOperandStartIndex + 1 == secondOperandEndIndex)
            {
                _secondOperand = new Operand(expression, secondOperandStartIndex);
            }
            else
            {
                _secondOperand = new Operator(expression, secondOperandStartIndex, secondOperandEndIndex);
            }
        }
        else
        {
            throw new ParseErrorException("Expected int or operator given unknown entity"); 
        }
    }

    public override double Evaluate()
    {
        var evaluatedFirstOperand = _firstOperand.Evaluate();
        var evaluatedSecondOperand = _secondOperand.Evaluate();
        return EvaluateOperator(_operator, evaluatedFirstOperand, evaluatedSecondOperand);
    }

    public override void Print()
    {
        Console.Write("(");
        PrintOperator(_operator);
        Console.Write(" ");
        _firstOperand.Print();
        Console.Write(" ");
        _secondOperand.Print();
        Console.Write(")");
    }
    
    private readonly Operators _operator;
    private readonly ParseTree.Node _firstOperand;
    private readonly ParseTree.Node _secondOperand;
}