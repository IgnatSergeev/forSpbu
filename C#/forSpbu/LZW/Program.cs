using System.Text;
using LZW;

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
    using var fileStream = File.Open(pathToFile, FileMode.Open);
    using var outFile = BufferedFileStream.Create(pathToFile + ".zipped");
    var trie = new Trie.TrieRealisation();
    
    switch (key)
    {
        case "-c":
            LZW.Lzw.Encode(fileStream, outFile, trie);
            break;
        case "-u":
            //LZW.Lzw.Decode(fileStream, outFile, trie);
            break;
        default:
            Console.WriteLine("Wrong key argument");
            break;
    }
}
catch (IOException)
{
    Console.WriteLine("File not found");
}

return 0;

