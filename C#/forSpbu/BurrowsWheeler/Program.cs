namespace BurrowsWheeler;

public static class BurrowsWheeler 
{
    //returns 1 if first string is greater than second, -1 if first is less than second, 0 if they are equal
    private static int Compare(int firstStartPosition, int secondStartPosition)
    {
        int size = str.Length;
        for (int i = 0; i < size; i++)
        {
            char firstStringChar = str[(firstStartPosition + i) % size];
            char secondStringChar = str[(secondStartPosition + i) % size];
            
            if (firstStringChar < secondStringChar)
            {
                return -1;
            } 
            if (firstStringChar > secondStringChar)
            {
                return 1;
            }
        }

        return 0;
    }

    public static Tuple<string, int> Encode(string stringToEncode)
    {
        if (stringToEncode == null)
        {
            throw new Exception("Cannot encode null string");
        }

        int size = stringToEncode.Length;
        str = stringToEncode;
        int[] indexArray = new int[size];
        for (int i = 0; i < size; i++)
        {
            indexArray[i] = i;
        }

        Compare(3, 1);
        int initialStringIndex = 0;
        Array.Sort(indexArray, Compare);

        char[] encodedCharArray = new char[size];
        for (int i = 0; i < size; i++)
        {
            encodedCharArray[i] = stringToEncode[(indexArray[i] - 1 + size) % size];
            if (indexArray[i] == 0)
            {
                initialStringIndex = i;
            }
        }
        
        string encodedString = new string(encodedCharArray);
        return new Tuple<string, int>(encodedString, initialStringIndex);
    }
    
    public static string Decode(string stringToDecode, int indexOfInitialString)
    {
        if (stringToDecode == null)
        {
            throw new Exception("Cannot decode null string");
        }
        
        int size = stringToDecode.Length;
        
        char[] sortedString = new char[size];
        for (int i = 0; i < size; i++)
        {
            sortedString[i] = stringToDecode[i];
        }
        Array.Sort(sortedString);

        Dictionary<char, int> numOfSymbols = new Dictionary<char, int>();
        for (int i = 0; i < size; i++)
        {
            if (numOfSymbols.ContainsKey(stringToDecode[i]))
            {
                ++numOfSymbols[stringToDecode[i]];
            }
            else
            {
                numOfSymbols.Add(stringToDecode[i], 1);
            }
        }
        
        Dictionary<char, int> indexesOfSymbols = new Dictionary<char, int>();
        for (int indexOfFirstSymbol = 0; indexOfFirstSymbol < size; indexOfFirstSymbol += numOfSymbols[sortedString[indexOfFirstSymbol]])
        {
            indexesOfSymbols.Add(sortedString[indexOfFirstSymbol], indexOfFirstSymbol);
        }

        int[] reverseBwtVector = new int[size];
        for (int i = 0; i < size; i++)
        {
            reverseBwtVector[indexesOfSymbols[stringToDecode[i]]] = i;
            ++indexesOfSymbols[stringToDecode[i]];
        }

        char[] decodedArray = new char[size];
        int j = reverseBwtVector[indexOfInitialString];
        for (int i = 0; i < size; i++)
        {
            decodedArray[i] = stringToDecode[j];
            j = reverseBwtVector[j];
        }

        return new string(decodedArray);
    }
    
    private static string str;
}

public static class Test
{
    public static void TestBurrowsWheeler()
    {
        Tuple<string, int> encodedValue = BurrowsWheeler.Encode("BANANA");
        if (string.Compare(encodedValue.Item1, "NNBAAA") != 0 || encodedValue.Item2 != 3)
        {
            throw new Exception("Tests failed");
        }

        string decodedString = BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
        if (string.Compare(decodedString, "BANANA") != 0)
        {
            throw new Exception("Tests failed");
        }
    }
}

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