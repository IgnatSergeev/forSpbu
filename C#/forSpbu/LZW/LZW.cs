namespace LZW;

/// <summary>
/// Class for compressing and decompressing files
/// </summary>
public static class Lzw
{
    private static void WriteCode(BufferedFileStream outputFileStream, int code, int codeSize)
    {
        var codeBits = new bool[codeSize];
        int codeIndex = 0;
        while (code != 0)
        {
            codeBits[codeSize - codeIndex++ - 1] = code % 2 != 0;
            code >>= 1;
        }

        outputFileStream.WriteBits(codeBits);
    }
    
    /// <summary>
    /// Compress file using lzw algorithm
    /// </summary>
    /// <param name="inputFileStream">file stream with file to compress</param>
    /// <param name="outputFileStream">buffered file stream for compressed file</param>
    /// <param name="trie">trie structure used in algorithm</param>
    /// <exception cref="ArgumentNullException">if either of given arguments is null</exception>
    public static void Encode(FileStream inputFileStream, BufferedFileStream outputFileStream, Trie.Trie trie)
    {
        if (inputFileStream == null)
        {
            throw new ArgumentNullException(nameof(inputFileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }
        if (outputFileStream == null)
        {
            throw new ArgumentNullException(nameof(outputFileStream));
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
        int fileByte = inputFileStream.ReadByte();
        while (fileByte != -1)
        {
            var oldPhrase = phrase.ToArray();
            
            var symbol = (char)fileByte;
            phrase.Enqueue(symbol);

            if (!trie.Contains(phrase))
            {
                WriteCode(outputFileStream, trie.GetCode(oldPhrase), codeSize);
                
                if (code >= (1 << codeSize))
                {
                    ++codeSize;
                }
                trie.AddChar(oldPhrase, symbol, code++);
                phrase = new Queue<char>();
                phrase.Enqueue(symbol);
            }

            fileByte = inputFileStream.ReadByte();
        }

        if (phrase.Count > 0)
        {
            WriteCode(outputFileStream, trie.GetCode(phrase), codeSize);
        }
    }
    
    /// <summary>
    /// Decompress the file compressed with lzw algorithm
    /// </summary>
    /// <param name="inputFileStream">buffered file stream with file to decompress</param>
    /// <param name="outputFileStream">file stream for decompressed file</param>
    /// <param name="trie">trie structure used in algorithm</param>
    /// <exception cref="ArgumentNullException">if either of arguments is null</exception>
    /// <exception cref="UnexpectedBranchingException">if file wasn't correctly compressed(there is an unknown phrase code)</exception>
    public static void Decode(BufferedFileStream inputFileStream, FileStream outputFileStream, Trie.Trie trie)
    {
        if (inputFileStream == null)
        {
            throw new ArgumentNullException(nameof(inputFileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }
        if (outputFileStream == null)
        {
            throw new ArgumentNullException(nameof(outputFileStream));
        }
        
        const int byteSize = 8;
        const int maxByte = (1 << (byteSize)) - 1;
        for (int i = 0; i <= maxByte; i++)
        {
            trie.AddChar("", (char)i, i);
        }
        
        int codeSize = byteSize;
        int nextCode = maxByte + 1;
        var (phraseCodeArray, isTheEndOfFile) = inputFileStream.ReadBits(codeSize);
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
                outputFileStream.WriteByte((byte)symbol);
            }
            oldPhrase = phrase;
            
            if (nextCode >= (1 << codeSize))
            {
                ++codeSize;
            }
            
            (phraseCodeArray, isTheEndOfFile) = inputFileStream.ReadBits(codeSize);
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