namespace LZW;

public class BufferedFileStream : IDisposable
{
    public static BufferedFileStream Create(string path)
    {
        var result = new BufferedFileStream();
        result._fileStream = File.Create(path);

        return result;
    }
    
    public static BufferedFileStream Open(string path, FileMode mode)
    {
        var result = new BufferedFileStream();
        result._fileStream = File.Open(path, mode);

        return result;
    }
    
    public void Dispose()
    {
        if (_firstFreeWriteBufferIndex != 0)
        {
            _fileStream?.WriteByte(GetByte());
        }
        
        _fileStream?.Dispose();
    }

    private byte GetByte()
    {
        byte result = 0;
        const int bufferSize = 8;
        for (int i = 0; i < _firstFreeWriteBufferIndex; i++)
        {
            if (_writeBuffer[i])
            {
                result |= (byte)(1 << (bufferSize - i - 1));
            }
        }

        return result;
    }

    public (bool[], bool) ReadBits(int numOfBits)
    {
        if (numOfBits <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numOfBits));
        }
        
        var bits = new bool[numOfBits];

        int numOfAlreadyReadBits = _readBufferSize;
        if (_readBufferSize != 0)
        {
            for (int i = 0; i < _readBufferSize; i++)
            {
                bits[i] = _readBuffer[i];
            }

            _readBufferSize = 0;
        }

        int numOfBitsToRead = numOfBits - numOfAlreadyReadBits;
        int numbOfBytesToRead = numOfBitsToRead / byteSize;
        for (int i = 0; i < numbOfBytesToRead; i++)
        {
            var currentByte = _fileStream?.ReadByte();
            if (currentByte == -1)
            {
                return (bits, true);
            }

            for (int j = 0; j < byteSize; j++)
            {
                int index = numOfAlreadyReadBits + (i + 1) * byteSize - j - 1;
                bits[index] = (currentByte >> j) % 2 != 0;
            }
        }

        if (numOfBitsToRead % byteSize != 0)
        {
            var currentByte = _fileStream?.ReadByte();
            if (currentByte == -1)
            {
                return (bits, true);
            }

            for (int i = 0; i < numOfBitsToRead % byteSize; i++)
            {
                int index = bits.Length - numOfBitsToRead % byteSize + i;
                bits[index] = (currentByte >> (byteSize - 1 - i)) % 2 != 0;
            }

            _readBufferSize = byteSize - (numOfBitsToRead % byteSize);
            for (int i = 0; i < _readBufferSize; i++)
            {
                _readBuffer[i] = (currentByte >> (byteSize - 1 - numOfBitsToRead % byteSize - i)) % 2 != 0;
            }
        }
        return (bits, false);
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
        
        if (_firstFreeWriteBufferIndex < byteSize - 1)
        {
            _writeBuffer[_firstFreeWriteBufferIndex++] = b;
        }
        else
        {
            _writeBuffer[_firstFreeWriteBufferIndex++] = b;
            _fileStream?.WriteByte(GetByte());
            _firstFreeWriteBufferIndex = 0;
        }
    }

    private const int byteSize = 8;
    private FileStream? _fileStream;
    private readonly bool[] _writeBuffer = new bool[byteSize];
    private readonly bool[] _readBuffer = new bool[byteSize];
    private int _readBufferSize;
    private int _firstFreeWriteBufferIndex;
}