using System.Threading.Tasks.Dataflow;

namespace BurrowsWheeler;

public static class BurrowsWheeler 
{
    //returns 1 if first string is greater than second, -1 if first is less than second, 0 if they are equal
    private static int Compare(string str, int firstStartPosition, int secondStartPosition)
    {
        int size = str.Length;
        for (int i = 0; i < size; i++)
        {
            char firstStringChar = str[(firstStartPosition + i) % size];
            char secondStringChar = str[(secondStartPosition + i) % size];
            
            if (firstStringChar < secondStringChar)
            {
                return 1;
            } 
            if (firstStringChar > secondStringChar)
            {
                return -1;
            }
        }

        return 0;
    }

    private static void Sort(int[] array, string str)
    {
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size - 1; j++)
            {
                if (Compare(str, j, j + 1) < 0)
                {
                    int tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                }
            }
        }
    }
    
    public static KeyValuePair<string, int> Encode(string str)
    {
        if (str == null)
        {
            throw new Exception("Cannot convert null string");
        }

        int size = str.Length;
        int[] indexArray = new int[size];
        for (int i = 0; i < size; i++)
        {
            indexArray[i] = i;
        }

        int initialStringIndex = 0;
        Sort(indexArray, str);

        char[] encodedCharArray = new char[size];
        for (int i = 0; i < size; i++)
        {
            encodedCharArray[i] = str[(indexArray[i] - 1 + size) % size];
            if (indexArray[i] == 0)
            {
                initialStringIndex = i;
                break;
            }
        }


        string encodedString = new string(encodedCharArray);
        return new KeyValuePair<string, int>(encodedString, initialStringIndex);
    }
}

public static class Test
{
    public static void TestBurrowsWheeler()
    {
        string str = ".ANA";
        
        KeyValuePair<string, int> encodedValue = BurrowsWheeler.Encode(str);
        Console.Write(encodedValue.Key);
    }
}

public static class Program
{
    static void Main(string[] args)
    {
        Test.TestBurrowsWheeler();
    }
}