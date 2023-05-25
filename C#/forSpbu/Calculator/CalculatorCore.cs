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
            "C" => Operation.Clear,
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
        
        switch (_state)
        {
            case State.Error:
                if (numberString != ",")
                {
                    _number = numberString;
                    Expression = "";
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
                if (_hasComma && numberString == ",")
                {
                    _state = State.Error;
                    break;
                }

                if (numberString == ",")
                {
                    _hasComma = true;
                }
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
                if (_hasComma && numberString == ",")
                {
                    _state = State.Error;
                    break;
                }
                
                _hasComma = numberString == ",";
                _number += numberString;
                break;
            case State.Operation:
                if (numberString == ",")
                {
                    _state = State.Error;
                    break;
                }
                
                _number = numberString;
                if (_operation == Operation.Equal)
                {
                    _operation = Operation.None;
                    Expression = "";
                    _state = State.FirstOperandNumber;
                    break;
                }
                
                _state = State.SecondOperandNumber;
                break;
            case State.None:
                if (numberString == ",")
                {
                    _number += numberString;
                    _state = State.FirstOperandComma;
                    _hasComma = true;
                }
                else
                {
                    _number = numberString;
                    _state = State.FirstOperandNumber;
                }
                
                Expression = "";
                
                break;
        }

        if (_state == State.Error)
        {
            Expression = "";
            _number = "Error";
            _hasComma = false;
        }
    }

    private void ApplyOperation()
    {
        switch (_operation)
        {
            case Operation.Add:
                _firstOperand += _secondOperand;
                break;
            case Operation.Sub:
                _firstOperand -= _secondOperand;
                break;
            case Operation.Mul:
                _firstOperand *= _secondOperand;
                break;
            case Operation.Div:
                if (_secondOperand is < Delta and > -Delta)
                {
                    _state = State.Error;
                    break; // Можно как фишку оставить, будет бесконечность
                }
                _firstOperand /= _secondOperand;
                break;
            case Operation.Mod:
                if (_secondOperand is < Delta and > -Delta)
                {
                    _state = State.Error;
                    break;
                }
                _firstOperand %= _secondOperand;
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
            Expression = "";
            _number = "0";
            _hasComma = false;
            return;
        }
        
        switch (_state)
        {
            case State.Error:
                break;
            case State.FirstOperandComma:
                _state = State.Operation;
                _number = _number.Remove(_number.Length - 1);
                
                if (!double.TryParse(_number, out _firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                
                _operation = operation;
                Expression = _firstOperand + " " + operationString;
                break;
            case State.FirstOperandNumber:
                _state = State.Operation;
                if (!double.TryParse(_number, out _firstOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);;
                }
                
                _operation = operation;
                Expression = _firstOperand + " " + operationString;
                break;
            case State.SecondOperandComma:
                _state = State.Operation;
                _number = _number.Remove(_number.Length - 1);
                if (!double.TryParse(_number, out _secondOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);
                }

                ApplyOperation();
                _operation = operation;

                Expression = _firstOperand + " " + operationString;
                _hasComma = false;
                _number = "";
                
                break;
            case State.SecondOperandNumber:
                _state = State.Operation;
                if (!double.TryParse(_number, out _secondOperand))
                {
                    throw new ArgumentOutOfRangeException(nameof(_number), _number, null);
                }

                ApplyOperation();
                _operation = operation;

                Expression = _firstOperand + " " + operationString;
                _hasComma = false;
                _number = "";
                
                break;
            case State.Operation:
                _operation = operation;
                Expression = _firstOperand + " " + operationString;
                break;
            case State.None:
                _state = State.Operation;
                _operation = operation;
                Expression = "0 " + operationString;
                break;
        }
        
        if (_state == State.Error)
        {
            Expression = "";
            _number = "Error";
        }
    }

    private double _firstOperand = 0;
    private double _secondOperand = 0;
    private Operation _operation = Operation.None;
    private State _state = State.None;
    private string _number = "0";
    private bool _hasComma = false;
    private const double Delta = 0.0001;

    public string Number => _number;
    public string Expression { get; private set; } = "";
}