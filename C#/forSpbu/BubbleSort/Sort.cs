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