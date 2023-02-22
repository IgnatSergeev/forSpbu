using System.Runtime.CompilerServices;

namespace BubbleSort
{
    class Program
    {
        static void BubbleSort(int[] array)
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
        
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            BubbleSort(array);
            
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                Console.Write(" ");
            }
        }
    }
}


