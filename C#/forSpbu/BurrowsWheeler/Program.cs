namespace BurrowsWheeler;

public static class Program
{
    static void Main(string[] args)
    {
        Test.TestBurrowsWheeler();
        
        Console.WriteLine("Введите строку, которую хотите трансформировать с помощью преобразования Барроуза-Уилера");
        var line = Console.ReadLine();

        var encodedValue = BurrowsWheeler.Encode(line);
        Console.WriteLine("Вот закодированная строка и позиция конца строки в ней: " + encodedValue.Item1 + ", " + encodedValue.Item2);

        var decodedValue = BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
        Console.WriteLine("Вот строка получившаяся после декодировки: " + decodedValue);
    }
}