namespace StackCalculator;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите арифметическое выражения в постфиксной форме(Не целые числа вводятся с использованием запятой)");
        var line = Console.ReadLine();

        var expressionResult = StackCalculator.Evaluate(line, new ListStack<float>());
        Console.WriteLine("Результат выражения = " + expressionResult);
    }
}