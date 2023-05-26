using ParseTree;

Console.WriteLine("Введите путь к файлу с бинарным выражением в префиксной форме (относительно exe)");
var path = Console.ReadLine();
if (path == null && File.Exists(path))
{
    Console.WriteLine("Некорректный ввод");
    return;
}

try
{
    var expression = File.ReadAllText(path);
    var tree = new ParseTree.ParseTree(expression);
    Console.WriteLine("Вот распечатанное дерево");
    tree.Print();
    Console.WriteLine("Результат выражения = " + tree.Evaluate());
}
catch (IOException)
{
    Console.WriteLine("Файл не найден");
}
catch (DivideByZeroException)
{
    Console.WriteLine("В выражении происходит деление на ноль(с некоторой точностью)");
}
catch (ParseErrorException e)
{
    Console.WriteLine("Ошибка в парсинге выражения, неверная структура выражения" + e.Message);
}
