using System;
using System.Runtime.InteropServices.JavaScript;

namespace LZW;

public static class Lzw
{
    public static void Encode(FileStream fileStream, FileStream outFile, Trie.Trie trie)
    {
        if (fileStream == null)
        {
            throw new ArgumentNullException(nameof(fileStream));
        }
        if (trie == null)
        {
            throw new ArgumentNullException(nameof(trie));
        }

        const int maxByte = (2 << (sizeof(byte) * 8 - 1)) - 1;
        for (int i = 0; i <= maxByte; i++)
        {
            char symbol = (char)i;
            trie.AddChar("", symbol, i);
        }

        int code = maxByte + 1;
        var stack = new Stack<char>();
        int fileByte = fileStream.ReadByte();
        while (fileByte != -1)
        {
            var symbol = (char)fileByte;
            
            var phrase = stack.ToArray();
            if (!trie.Contains(phrase + symbol))
            {
                var byteArray = BitConverter.GetBytes(trie.GetCode(phrase));
                outFile.Write(byteArray);
                trie.AddChar(phrase, symbol, code++);
                stack = new Stack<char>();
                stack.Push(symbol);
            }

            fileByte = fileStream.ReadByte();
        }

        if (stack.Count <= 0) return;
        {
            var phrase = new string(stack.ToArray());
            var byteArray = BitConverter.GetBytes(trie.GetCode(phrase));
            outFile.Write(byteArray);
        }
    }
    
    public static void Decode(FileStream fileStream, FileStream outFile, Trie.Trie trie)
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