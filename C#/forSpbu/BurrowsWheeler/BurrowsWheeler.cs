// Copyright 2023 Ignatii Sergeev.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


namespace BurrowsWheeler;

public static class BurrowsWheeler 
{
    //returns 1 if first string is greater than second, -1 if first is less than second, 0 if they are equal
    private static int Compare(int firstStartPosition, int secondStartPosition, string str)
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
        if (stringToEncode.Length == 0)
        {
            throw new Exception("Cannot encode empty string");
        }
        
        int size = stringToEncode.Length;
        var indexArray = new int[size];
        for (int i = 0; i < size; i++)
        {
            indexArray[i] = i;
        }

        Array.Sort(indexArray, (x, y) => Compare(x, y, stringToEncode));

        int initialStringIndex = 0;
        var encodedCharArray = new char[size];
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
        if (stringToDecode.Length == 0)
        {
            throw new Exception("Cannot decode empty string");
        }
        if (indexOfInitialString < 0 || indexOfInitialString > stringToDecode.Length)
        {
            throw new Exception("Wrong index");
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

        return new string(decodedArray);
    }
}