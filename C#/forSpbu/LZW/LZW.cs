namespace LZW;

public static class Lzw
{
    private static void WriteCode(BufferedFileStream outFile, int code, int codeSize)
    {
        var codeBits = new bool[codeSize];
        int codeIndex = 0;
        while (code != 0)
        {
            codeBits[codeSize - codeIndex++ - 1] = code % 2 != 0;
            code >>= 1;
        }

        outFile.WriteBits(codeBits);
    }
    
    public static void Encode(FileStream fileStream, BufferedFileStream outFile, Trie.Trie trie)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException(nameof(fileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }
        if (outFile == null)
        {
            throw new ArgumentNullException(nameof(outFile));
        }

        const int byteSize = 8;
        const int maxByte = (1 << (byteSize)) - 1;
        for (int i = 0; i <= maxByte; i++)
        {
            trie.AddChar("", (char)i, i);
        }
        
        int codeSize = byteSize;
        int code = maxByte + 1;
        
        var phrase = new Queue<char>();
        int fileByte = fileStream.ReadByte();
        while (fileByte != -1)
        {
            var oldPhrase = phrase.ToArray();
            
            var symbol = (char)fileByte;
            phrase.Enqueue(symbol);

            if (!trie.Contains(phrase))
            {
                WriteCode(outFile, trie.GetCode(oldPhrase), codeSize);
                
                if (code >= (1 << codeSize))
                {
                    ++codeSize;
                }
                trie.AddChar(oldPhrase, symbol, code++);
                phrase = new Queue<char>();
                phrase.Enqueue(symbol);
            }

            fileByte = fileStream.ReadByte();
        }

        if (phrase.Count > 0)
        {
            WriteCode(outFile, trie.GetCode(phrase), codeSize);
        }
    }
    
    public static void Decode(BufferedFileStream fileStream, FileStream outFile, Trie.Trie trie)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException(nameof(fileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }
        if (outFile == null)
        {
            throw new ArgumentNullException(nameof(outFile));
        }
        
        const int byteSize = 8;
        const int maxByte = (1 << (byteSize)) - 1;
        for (int i = 0; i <= maxByte; i++)
        {
            trie.AddChar("", (char)i, i);
        }
        
        int codeSize = byteSize;
        int nextCode = maxByte + 1;
        var (phraseCodeArray, isTheEndOfFile) = fileStream.ReadBits(codeSize);
        char[]? oldPhrase = null;

        while (!isTheEndOfFile)
        {
            int phraseCode = GetIntFromBoolArray(phraseCodeArray);
            char[]? phrase;

            if (trie.ContainsCode(phraseCode))
            {
                phrase = trie.GetString(phraseCode);
                if (phrase == null)
                {
                    throw new UnexpectedBranchingException("Missing phrase code in trie");
                }
                
                if (oldPhrase != null)
                {
                    trie.AddChar(oldPhrase, phrase[0], nextCode++);
                }
            }
            else
            {
                if (oldPhrase != null)
                {
                    trie.AddChar(oldPhrase, oldPhrase[0], nextCode++);
                }
                else
                {
                    throw new UnexpectedBranchingException("Missing first phrase code in trie");
                }
                
                phrase = trie.GetString(phraseCode);
                if (phrase == null)
                {
                    throw new UnexpectedBranchingException("Missing phrase code in trie");
                }
            }
            
            foreach (var symbol in phrase)
            {
                outFile.WriteByte((byte)symbol);
            }
            oldPhrase = phrase;
            
            if (nextCode >= (1 << codeSize))
            {
                ++codeSize;
            }
            
            (phraseCodeArray, isTheEndOfFile) = fileStream.ReadBits(codeSize);
        }
        
        
    }

    private static int GetIntFromBoolArray(bool[] phraseCodeArray)
    {
        int size = phraseCodeArray.Length;
        int result = 0;
        for (int i = 0; i < size; i++)
        {
            result <<= 1;
            result += phraseCodeArray[i] ? 1 : 0;
        }

        return result;
    }
}