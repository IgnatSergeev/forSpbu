namespace StackCalculator;

public static class Program
{
    static int Main(string[] args)
    {
        Console.WriteLine("Введите арифметическое выражения в постфиксной форме");
        var line = Console.ReadLine();

        var expressionResult = StackCalculator.Evaluate(line, new ListStack());
        Console.WriteLine("Результат выражения = " + expressionResult);

        return 0;
    }
}