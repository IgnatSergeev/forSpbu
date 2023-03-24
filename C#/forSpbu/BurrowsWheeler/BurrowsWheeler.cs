namespace BurrowsWheeler;

/// <summary>
/// Class for encoding and decoding a string(increases compression rates of some algorithms)
/// </summary>
public static class BurrowsWheeler 
{
    //returns 1 if first string is greater than second, -1 if first is less than second, 0 if they are equal
    private static int Compare(int firstStartPosition, int secondStartPosition, char[] str)
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

    /// <summary>
    /// Encodes string using bwt algorithm
    /// </summary>
    /// <param name="stringToEncode">array of chars to encode</param>
    /// <returns>tuple of encoded array of chars and index of the end of the string needed for decoding</returns>
    /// <exception cref="ArgumentNullException">if given string is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">if given string is empty</exception>
    public static Tuple<char[], long> Encode(char[] stringToEncode)
    {
        if (stringToEncode == null)
        {
            throw new ArgumentNullException(nameof(stringToEncode));
        }
        if (stringToEncode.Length == 0)
        {
            throw new ArgumentOutOfRangeException("Empty string: " + nameof(stringToEncode));
        }
        
        int size = stringToEncode.Length;
        var indexArray = new int[size];
        for (int i = 0; i < size; i++)
        {
            indexArray[i] = i;
        }

        Array.Sort(indexArray, (x, y) => Compare(x, y, stringToEncode));

        long initialStringIndex = 0;
        var encodedCharArray = new char[size];
        for (int i = 0; i < size; i++)
        {
            encodedCharArray[i] = stringToEncode[(indexArray[i] - 1 + size) % size];
            if (indexArray[i] == 0)
            {
                initialStringIndex = i;
            }
        }
        
        return new Tuple<char[], long>(encodedCharArray, initialStringIndex);
    }
    
    /// <summary>
    /// Decodes string using bwt algorithm
    /// </summary>
    /// <param name="stringToDecode">array of chars to decode</param>
    /// <param name="indexOfInitialString">index of the end of the string in encoded string</param>
    /// <returns>decoded array of chars</returns>
    /// <exception cref="ArgumentNullException">if given array is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">if given array is empty or index is out of range</exception>
    public static char[] Decode(char[] stringToDecode, long indexOfInitialString)
    {
        if (stringToDecode == null)
        {
            throw new ArgumentNullException(nameof(stringToDecode));
        }
        if (stringToDecode.Length == 0)
        {
            throw new ArgumentOutOfRangeException("Empty string: " + nameof(stringToDecode));
        }
        if (indexOfInitialString < 0 || indexOfInitialString >= stringToDecode.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(indexOfInitialString));
        }
        
        int size = stringToDecode.Length;
        
        var sortedString = new char[size];
        for (int i = 0; i < size; i++)
        {
            sortedString[i] = stringToDecode[i];
        }
        Array.Sort(sortedString);

        var numOfSymbols = new Dictionary<char, int>();
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
        
        var indexesOfSymbols = new Dictionary<char, int>();
        for (int indexOfFirstSymbol = 0; indexOfFirstSymbol < size; indexOfFirstSymbol += numOfSymbols[sortedString[indexOfFirstSymbol]])
        {
            indexesOfSymbols.Add(sortedString[indexOfFirstSymbol], indexOfFirstSymbol);
        }

        var reverseBwtVector = new int[size];
        for (int i = 0; i < size; i++)
        {
            reverseBwtVector[indexesOfSymbols[stringToDecode[i]]] = i;
            ++indexesOfSymbols[stringToDecode[i]];
        }

        var decodedArray = new char[size];
        int j = reverseBwtVector[indexOfInitialString];
        for (int i = 0; i < size; i++)
        {
            decodedArray[i] = stringToDecode[j];
            j = reverseBwtVector[j];
        }

        return decodedArray;
    }
}