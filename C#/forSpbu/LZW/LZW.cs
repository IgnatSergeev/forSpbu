namespace LZW;

public static class Lzw
{
    private static void WriteCode(BufferedFileStream outFile, int code, int codeSize)
    {
        if (code >= (1 << codeSize))
        {
            throw new ArgumentOutOfRangeException(nameof(code));
        }
        
        bool[] codeBits = new bool[codeSize];
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

        const int maxByte = (1 << (sizeof(byte) * 8)) - 1;
        for (int i = 0; i <= maxByte; i++)
        {
            trie.AddChar("", (char)i, i);
        }
        
        int codeSize = 8;
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
    
    public static void Decode(FileStream fileStream, BufferedFileStream outFile, Trie.Trie trie)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException(nameof(fileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }
        
        
    }
}