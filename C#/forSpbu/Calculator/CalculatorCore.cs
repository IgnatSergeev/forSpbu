namespace Calculator;

public class CalculatorCore
{
    private enum Operation
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod,
        Clear,
        Equal
    }

    private enum State
    {
        FirstOperandNumber,
        FirstOperandComma,
        SecondOperandNumber,
        SecondOperandComma,
        Operation,
        Error
    }

    private Operation StringGetOperation(string operationString)
    {
        return operationString switch
        {
            "+" => Operation.Add,
            "-" => Operation.Sub,
            "*" => Operation.Mul,
            "/" => Operation.Div,
            "mod" => Operation.Mod,
            "CE" => Operation.Clear,
            "=" => Operation.Equal,
            _ => throw new ArgumentOutOfRangeException(nameof(operationString), operationString, null)
        };
    }
    
    private double StringGetNumber(string numberString)
    {
        return numberString switch
        {
            "0" => 0,
            "1" => 1,
            "2" => 2,
            "3" => 3,
            "4" => 4,
            "5" => 5,
            "6" => 6,
            "7" => 7,
            "8" => 8,
            "9" => 9,
            _ => throw new ArgumentOutOfRangeException(nameof(numberString), numberString, null)
        };
    }
    
    public void TakeNumberToken(string numberString)
    {
        switch (_state)
        {
            case State.Error:
                _number = numberString;
                _expression = "";
                _state = State.FirstOperandNumber;
                break;
            case State.FirstOperandComma:
                _number += numberString;
                _state = State.FirstOperandNumber;
                break;
            case State.FirstOperandNumber:
                _number += numberString;
                break;
            case State.SecondOperandComma:
                _number += numberString;
                _state = State.SecondOperandNumber;
                break;
            case State.SecondOperandNumber:
                _number += numberString;
                break;
            case State.Operation:
                _number = numberString;
                _state = State.FirstOperandNumber;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_state), _state, null);
        }
    }
    
    public void TakeOperationToken(string operationString)
    {
        var operation = StringGetOperation(operationString);
        switch (_state)
        {
            case State.Error:
                break;
            case State.FirstOperandComma:
                _state = State.Operation;
                if (operation == Operation.Clear)
                {
                    
                }
                _number = _number.Remove(_number.Length - 1);
                if (!double.TryParse(_number, out firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                _expression += _number + operationString;
                break;
            case State.FirstOperandNumber:
                _state = State.Operation;
                _expression += _number + operationString;
                if (!double.TryParse(_number, out firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                break;
            case State.SecondOperandComma:
                _state = State.Operation;
                _number = _number.Remove(_number.Length - 1);
                if (!double.TryParse(_number, out firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                
        }
    }

    private double firstOperand = 0;
    private double secondOperand = 0;
    private State _state = State.Error;
    private string _number = "0";
    private string _expression = "";
    private const double Delta = 0.0001;
}