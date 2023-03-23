namespace BubbleSort;

public static class Program
{
    static int Main(string[] args)
    {
        Test.TestBubbleSort();
        
        Console.WriteLine("Введите элементы массива, который хотите отсортировать, через пробел");
        var line = Console.ReadLine();
        if (line == null)
        {
            throw new Exception("Cannot sort this type of array, probably wrong input");
        }
        
        int[] array = line.Split().Select(x => int.TryParse(x, out var y) ? y: throw new Exception("Wrong array element")).ToArray();
        Sort.BubbleSort(array);
        
        Console.WriteLine("Вот отсортированный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            Console.Write(" ");
        }

        return 0;
    }
}