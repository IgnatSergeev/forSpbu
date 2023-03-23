BurrowsWheeler.Tests.BurrowsWheelerTest();
        
Console.WriteLine("Введите строку, которую хотите трансформировать с помощью преобразования Барроуза-Уилера");
var line = Console.ReadLine();
if (string.IsNullOrEmpty(line))
{
    Console.WriteLine("Неправильный ввод");
    return;
}

var encodedValue = BurrowsWheeler.BurrowsWheeler.Encode(line.ToArray());
Console.WriteLine("Вот закодированная строка и позиция конца строки в ней: " + encodedValue.Item1 + ", " + encodedValue.Item2);

var decodedValue = BurrowsWheeler.BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
Console.WriteLine("Вот строка получившаяся после декодировки: " + decodedValue);
