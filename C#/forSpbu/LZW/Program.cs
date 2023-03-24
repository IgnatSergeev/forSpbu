using LZW;
using BurrowsWheeler;

if (args.Length != 2)
{
    Console.WriteLine("Wrong number of arguments");
    return 0;
}

var pathToFile = args[0];
var key = args[1];
if (string.IsNullOrEmpty(pathToFile) || string.IsNullOrEmpty(key))
{
    Console.WriteLine("Wrong(null or empty) arguments");
    return 0;
}

try
{
    var trie = new Trie.TrieRealisation();
    
    switch (key)
    {
        case "-c":
        {
            {
                using var inputFileStream = File.Open(pathToFile, FileMode.Open);
                using var outputFileStream = BufferedFileStream.Create(pathToFile + ".zipped");
                Lzw.Encode(inputFileStream, outputFileStream, trie);
            }
            
            var inputFileInfo = new FileInfo(pathToFile);
            var outputFileInfo = new FileInfo(pathToFile + ".zipped");
            var inputFileSize = inputFileInfo.Length;
            var outputFileSize = outputFileInfo.Length;
            var compressionRatio = (double)inputFileSize / (double)outputFileSize;
            Console.WriteLine("Compression ratio = " + compressionRatio);
            break;
        }
        case "-u":
        {
            if (!pathToFile.EndsWith(".zipped"))
            {
                Console.WriteLine("Wrong file extension");
                break;
            }
            using var inputFileStream = BufferedFileStream.Open(pathToFile, FileMode.Open);
            using var outputFileStream = File.Create(pathToFile.Remove(pathToFile.Length - 7).Insert(pathToFile.Length - 11, "(decoded)"));
            Lzw.Decode(inputFileStream, outputFileStream, trie);
            break;
        }
        default:
        {
            Console.WriteLine("Wrong key argument");
            break;
        }
    }
}
catch (IOException)
{
    Console.WriteLine("File not found");
}

return 0;

