namespace BubbleSort;
public static class Sort
{ 
    public static void BubbleSort(int[] array)
    {
        if (array == null)
        {
            throw new Exception("Cannot sort null array");
        }
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int tmp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = tmp;
                }
            }
        }
    }
} 

public static class Test
{
    public static void TestBubbleSort()
    {
        int[] array = { 12, 34, 5, 1 };
        Sort.BubbleSort(array);

        int[] sortedArray = { 1, 5, 12, 34 };
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != sortedArray[i])
            {
                throw new Exception("Tests failed");
            }
        }
    }
}
public static class Program
{
    static int Main(string[] args)
    {
        Test.TestBubbleSort();
        
        Console.WriteLine("Введите элементы массива, который хотите отсортировать, через пробел");
        string line = Console.ReadLine();
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