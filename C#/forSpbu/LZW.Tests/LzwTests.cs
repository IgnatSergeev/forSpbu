using Trie;

namespace LZW.Tests;

public class LzwTests
{
    private static bool AreFilesEqual(string firstFilePath, string secondFilePath)
    {
        using var firstFileStream = File.Open(firstFilePath, FileMode.Open);
        using var secondFileStream = File.Open(secondFilePath, FileMode.Open);
        
        var firstFileByte = firstFileStream.ReadByte();
        var secondFileByte = secondFileStream.ReadByte();
        
        while (firstFileByte != -1)
        {
            if (firstFileByte != secondFileByte)
            {
                return false;
            }
            
            firstFileByte = firstFileStream.ReadByte();
            secondFileByte = secondFileStream.ReadByte();
        }

        return firstFileByte == secondFileByte;
    }
    
    private static IEnumerable<TestCaseData> FileNames()
    {
        yield return new TestCaseData("smallTest.txt");
        yield return new TestCaseData("bigTest.txt");
        yield return new TestCaseData("ChromeSetup.exe");
    }
    
    [Test, TestCaseSource(nameof(FileNames))]
    public void FileTests(string fileName)
    {
        using (var inputFileStream = File.Open(FilesFolderPath + fileName, FileMode.Open)) 
        {
            using var outputFileStream = BufferedFileStream.Create(FilesFolderPath + fileName + ".zipped");
            Lzw.Encode(inputFileStream, outputFileStream, new TrieRealisation());
        }

        using (var encodedFileStream = BufferedFileStream.Open(FilesFolderPath + fileName + ".zipped", FileMode.Open))
        {
            using var decodedFileStream = File.Create(FilesFolderPath + fileName.Insert(0, "(decoded)"));
            Lzw.Decode(encodedFileStream, decodedFileStream, new TrieRealisation());
        }
        
        Assert.That(AreFilesEqual(FilesFolderPath + fileName, FilesFolderPath + fileName.Insert(0, "(decoded)")));
        
        File.Delete(FilesFolderPath + fileName.Insert(0, "(decoded)"));
        File.Delete(FilesFolderPath + fileName + ".zipped");
    }

    [Test]
    public void EncodeNullFileStreamShouldThrowArgumentNullException()
    {
        using (var outputFileStream =
               BufferedFileStream.Create(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            Assert.Throws<ArgumentNullException>(
                () => Lzw.Encode(null, outputFileStream, new TrieRealisation()), "inputFileStream");
        }

        File.Delete(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    [Test]
    public void EncodeIntoNullBufferedFileStreamShouldThrowArgumentNullException()
    {
        using (var inputFileStream =
               File.Create(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            Assert.Throws<ArgumentNullException>(
                () => Lzw.Encode(inputFileStream, null, new TrieRealisation()), "outputFileStream");
        }

        File.Delete(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    [Test]
    public void EncodeUsingNullTrieShouldThrowArgumentNullException()
    {
        using (var inputFileStream =
               File.Create(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            using var outputFileStream = BufferedFileStream.Create(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
            Assert.Throws<ArgumentNullException>(() => Lzw.Encode(inputFileStream, outputFileStream, null), "trie");
        }

        File.Delete(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
        File.Delete(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    [Test]
    public void DecodeUsingNullTrieShouldThrowArgumentNullException()
    {
        using (var outputFileStream =
               File.Create(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            using var inputFileStream = BufferedFileStream.Create(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
            Assert.Throws<ArgumentNullException>(() => Lzw.Decode(inputFileStream, outputFileStream, null), "trie");
        }

        File.Delete(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
        File.Delete(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    [Test]
    public void DecodeNullFileStreamShouldThrowArgumentNullException()
    {
        using (var outputFileStream =
               File.Create(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            Assert.Throws<ArgumentNullException>(() => Lzw.Decode(null, outputFileStream, new TrieRealisation()), "inputFileStream");
        }

        File.Delete(FilesFolderPath + "emptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    [Test]
    public void DecodeIntoNullBufferedFileStreamShouldThrowArgumentNullException()
    {
        using (var inputFileStream = BufferedFileStream.Create(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt"))
        {
            Assert.Throws<ArgumentNullException>(() => Lzw.Decode(inputFileStream, null, new TrieRealisation()), "outputFileStream");
        }
        
        File.Delete(FilesFolderPath + "secondEmptyFileForCheckingExceptionsAndNotBreakRealTests.txt");
    }
    
    private const string FilesFolderPath = "../../../Tests/";
}