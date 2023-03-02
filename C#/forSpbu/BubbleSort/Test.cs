namespace BubbleSort;

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