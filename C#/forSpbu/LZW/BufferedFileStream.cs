using System.ComponentModel;
using System.Xml;
using Microsoft.VisualBasic;

namespace LZW;

public class BufferedFileStream : IDisposable
{
    public static BufferedFileStream Create(string path)
    {
        var result = new BufferedFileStream();
        result._fileStream = File.Create(path);

        return result;
    }
    
    public void Dispose()
    {
        if (_firstFreeBufferIndex != 0)
        {
            _fileStream?.WriteByte(GetByte());
        }
        
        _fileStream?.Dispose();
    }

    private byte GetByte()
    {
        byte result = 0;
        const int bufferSize = 8;
        for (int i = 0; i < _firstFreeBufferIndex; i++)
        {
            if (_buffer[i])
            {
                result |= (byte)(1 << (bufferSize - i - 1));
            }
        }

        return result;
    }

    public void WriteBits(bool[] bitsToWrite)
    {
        if (bitsToWrite == null)
        {
            throw new ArgumentNullException(nameof(bitsToWrite));
        }

        int numberOfBits = bitsToWrite.Length;
        for (int i = 0; i < numberOfBits; i++)
        {
            WriteBit(bitsToWrite[i]);
        }
    }

    private void WriteBit(bool b)
    {
        if (_firstFreeBufferIndex < 7)
        {
            _buffer[_firstFreeBufferIndex++] = b;
        }
        else
        {
            _buffer[_firstFreeBufferIndex++] = b;
            _fileStream?.WriteByte(GetByte());
            _firstFreeBufferIndex = 0;
        }
    }

    private FileStream? _fileStream;
    private readonly bool[] _buffer = new bool[8];
    private int _firstFreeBufferIndex;
}