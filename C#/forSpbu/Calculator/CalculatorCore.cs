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
        Equal,
        None
    }

    private enum State
    {
        FirstOperandNumber,
        FirstOperandComma,
        SecondOperandNumber,
        SecondOperandComma,
        Operation,
        Error,
        None
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

    public void TakeNumberToken(string numberString)
    {
        if (numberString is not ("0" or "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9" or ","))
        {
            throw new ArgumentOutOfRangeException(nameof(numberString), numberString, null);
        }
        /// запятая после числа с запятой, равно  это очищение
        switch (_state)
        {
            case State.Error:
                if (numberString != ",")
                {
                    _number = numberString;
                    _expression = "";
                    _state = State.FirstOperandNumber;
                }
                break;
            case State.FirstOperandComma:
                if (numberString == ",")
                {
                    _state = State.Error;
                    break;
                }
                
                _number += numberString;
                _state = State.FirstOperandNumber;
                break;
            case State.FirstOperandNumber:
                if (hasComma && numberString == ",")
                {
                    _state = State.Error;
                    break;
                }

                hasComma = numberString == ",";
                _number += numberString;
                break;
            case State.SecondOperandComma:
                if (numberString == ",")
                {
                    _state = State.Error;
                    break;
                }
                
                _number += numberString;
                _state = State.SecondOperandNumber;
                break;
            case State.SecondOperandNumber:
                if (hasComma && numberString == ",")
                {
                    _state = State.Error;
                    break;
                }
                
                hasComma = numberString == ",";
                _number += numberString;
                break;
            case State.Operation:
                _number = numberString;
                if (_operation == Operation.Equal)
                {
                    _operation = Operation.None;
                    _expression = "";
                    _state = State.FirstOperandNumber;
                }
                
                _state = State.SecondOperandNumber;
                break;
            case State.None:
                _number = numberString;
                _expression = "";
                _state = State.FirstOperandNumber;
                break;
        }
    }

    private void ApplyOperation()
    {
        switch (_operation)
        {
            case Operation.Add:
                firstOperand += secondOperand;
                break;
            case Operation.Sub:
                firstOperand -= secondOperand;
                break;
            case Operation.Mul:
                firstOperand *= secondOperand;
                break;
            case Operation.Div:
                firstOperand /= secondOperand;
                break;
            case Operation.Mod:
                firstOperand %= secondOperand;
                break;
        }
    }
    
    public void TakeOperationToken(string operationString)
    {
        var operation = StringGetOperation(operationString);
        if (operation == Operation.Clear)
        {
            _state = State.None;
            _operation = Operation.None;
        }
        
        switch (_state)
        {
            case State.Error:
                break;
            case State.FirstOperandComma:
                _state = State.Operation;
                _number = _number.Remove(_number.Length - 1);
                
                if (!double.TryParse(_number, out firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                
                _operation = operation;
                _expression = firstOperand + " " + operationString;
                break;
            case State.FirstOperandNumber:
                _state = State.Operation;
                if (!double.TryParse(_number, out firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                
                _operation = operation;
                _expression = firstOperand + " " + operationString;
                break;
            case State.SecondOperandComma:
                _state = State.Operation;
                _number = _number.Remove(_number.Length - 1);
                if (!double.TryParse(_number, out secondOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);
                }

                ApplyOperation();
                _operation = operation;

                _expression = firstOperand + " " + operationString;
                _number = "";
                
                break;
            case State.SecondOperandNumber:
                _state = State.Operation;
                if (!double.TryParse(_number, out secondOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);
                }

                ApplyOperation();
                _operation = operation;

                _expression = firstOperand + " " + operationString;
                _number = "";
                
                break;
            case State.Operation:
                _operation = operation;
                _expression = firstOperand + " " + operationString;
                break;
            case State.None:
                _state = State.Operation;
                _operation = operation;
                _expression = "0 " + operationString;
                break;
        }
    }

    private double firstOperand = 0;
    private double secondOperand = 0;
    private Operation _operation = Operation.None;
    private State _state = State.None;
    private string _number = "0";
    private bool hasComma = false;
    private string _expression = "";
    private const double Delta = 0.0001;
}