using System.Runtime.CompilerServices;

namespace BubbleSort
{
    class Program
    {
        static bool test()
        {
            int[] array = { 12, 34, 5, 1 };
            BubbleSort(array);

            int[] sortedArray = { 1, 5, 12, 34 };
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != sortedArray[i])
                {
                    return false;
                }
            }

            return true;
        }
        public static void BubbleSort(int[] array)
        {
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
        
        static int Main(string[] args)
        {
            if (!test())
            {
                Console.WriteLine("Тесты провалены");
                return -1;
            }
            Console.WriteLine("Тесты пройдены\n");
            
            Console.WriteLine("Введите элементы массива, который хотите отсортировать, через пробел");
            int[] array = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            BubbleSort(array);
            
            Console.WriteLine("Вот отсортированный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                Console.Write(" ");
            }

            return 0;
        }
    }
}


