namespace LZW;

/// <summary>
/// Class for working with file stream with buffered input and output
/// </summary>
public class BufferedFileStream : IDisposable
{
    /// <summary>
    /// Creates buffered file stream with creating a new file in the specified path
    /// </summary>
    /// <param name="path">path of the file</param>
    /// <returns>buffered file stream object</returns>
    /// <exception cref="ArgumentNullException">if given path is null or empty</exception>
    public static BufferedFileStream Create(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        
        var result = new BufferedFileStream();
        result._fileStream = File.Create(path);

        return result;
    }
    
    /// <summary>
    /// Creates buffered file stream using file in the specified path
    /// </summary>
    /// <param name="path">path of the file</param>
    /// <param name="mode">FileMode of working with file</param>
    /// <returns>buffered file stream object</returns>
    /// <exception cref="ArgumentNullException">if given path is null or empty</exception>
    public static BufferedFileStream Open(string path, FileMode mode)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

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

    /// <summary>
    /// Read given number of bits from file stream
    /// </summary>
    /// <param name="numOfBits">number of bit to read</param>
    /// <returns>Tuple of array of bits and bool defining was the reading interrupted by the end of the file</returns>
    /// <exception cref="ArgumentOutOfRangeException">if given number of bits less or equal to zero</exception>
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
        int numbOfBytesToRead = numOfBitsToRead / ByteSize;
        for (int i = 0; i < numbOfBytesToRead; i++)
        {
            var currentByte = _fileStream?.ReadByte();
            if (currentByte == -1)
            {
                return (bits, true);
            }

            for (int j = 0; j < ByteSize; j++)
            {
                int index = numOfAlreadyReadBits + (i + 1) * ByteSize - j - 1;
                bits[index] = (currentByte >> j) % 2 != 0;
            }
        }

        if (numOfBitsToRead % ByteSize != 0)
        {
            var currentByte = _fileStream?.ReadByte();
            if (currentByte == -1)
            {
                return (bits, true);
            }

            for (int i = 0; i < numOfBitsToRead % ByteSize; i++)
            {
                int index = bits.Length - numOfBitsToRead % ByteSize + i;
                bits[index] = (currentByte >> (ByteSize - 1 - i)) % 2 != 0;
            }

            _readBufferSize = ByteSize - (numOfBitsToRead % ByteSize);
            for (int i = 0; i < _readBufferSize; i++)
            {
                _readBuffer[i] = (currentByte >> (ByteSize - 1 - numOfBitsToRead % ByteSize - i)) % 2 != 0;
            }
        }
        return (bits, false);
    }

    /// <summary>
    /// Writes given bits to file stream
    /// </summary>
    /// <param name="bitsToWrite">array of bits to write</param>
    /// <exception cref="ArgumentNullException">if given array is null</exception>
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
        if (_firstFreeWriteBufferIndex < ByteSize - 1)
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

    private const int ByteSize = 8;
    private FileStream? _fileStream;
    private readonly bool[] _writeBuffer = new bool[ByteSize];
    private readonly bool[] _readBuffer = new bool[ByteSize];
    private int _readBufferSize;
    private int _firstFreeWriteBufferIndex;
}