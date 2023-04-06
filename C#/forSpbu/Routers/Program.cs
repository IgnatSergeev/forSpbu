using Routers;

if (args.Length != 1)
{
    Console.WriteLine("Невернные аргументы");
}

var path = args[0];
if (string.IsNullOrEmpty(path))
{
    Console.Error.WriteLine("Неверный путь");
    return 1;
}

try
{
    var graph = new Graph();
    graph.Parse(path);

    graph.TransformToMaximalWeightTree();
    graph.Print();
}
catch (IOException)
{
    Console.Error.WriteLine("Файл не найден");
    return 1;
}
catch (WrongGraphException e)
{
    Console.Error.WriteLine(e.Message);
    return 1;
}
catch (ParseException)
{
    Console.Error.WriteLine("Ошибка в парсинге графа");
    return 1;
}

return 0;


