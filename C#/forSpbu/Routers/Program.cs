// Copyright 2023 Ignatii Sergeev.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


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


