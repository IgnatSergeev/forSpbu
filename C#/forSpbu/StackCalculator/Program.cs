Console.WriteLine("Введите арифметическое выражения в постфиксной форме(Не целые числа вводятся с использованием запятой)");
var line = Console.ReadLine();
if (line == null)
{
    Console.WriteLine("Неправильный ввод");
    return;
}

try
{
    var (expressionResult, expressionWasCorrect) =
        StackCalculator.StackCalculator.Evaluate(line, new StackCalculator.ListStack<double>());
    if (expressionWasCorrect)
    {
        Console.WriteLine("Результат выражения = " + expressionResult);
    }
    else
    {
        Console.WriteLine("Выражение записано неправильно, его невозможно посчитать");
    }
}
catch (DivideByZeroException)
{
    Console.WriteLine("Происходит деление на 0, выражение невозможно посчитать");
}
catch (StackCalculator.ParseException)
{
    Console.WriteLine("Проблемы с парсингом числа, неправильный ввод");
}