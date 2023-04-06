using Routers;

if (args.Length != 2)
{
    Console.WriteLine("Невернные аргументы");
}

var inputPath = args[0];
if (string.IsNullOrEmpty(inputPath))
{
    Console.Error.WriteLine("Неверный путь");
    return 1;
}

var outputPath = args[1];
if (string.IsNullOrEmpty(outputPath))
{
    Console.Error.WriteLine("Неверный путь");
    return 1;
}

try
{
    var graph = new Graph();
    graph.Parse(inputPath);

    graph.TransformToMaximalWeightTree();
    graph.Print(outputPath);
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


