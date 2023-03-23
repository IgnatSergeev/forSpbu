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
    var trie = new Trie.TrieRealisation();
    
    switch (key)
    {
        case "-c":
        {
            using var fileStream = File.Open(pathToFile, FileMode.Open);
            using var outFile = BufferedFileStream.Create(pathToFile + ".zipped");
            Lzw.Encode(fileStream, outFile, trie);
            break;
        }
        case "-u":
        {
            if (!pathToFile.EndsWith(".zipped"))
            {
                Console.WriteLine("Wrong file extension");
                break;
            }
            using var fileStream = BufferedFileStream.Open(pathToFile, FileMode.Open);
            using var outFile = File.Create(pathToFile.Remove(pathToFile.Length - 7).Insert(pathToFile.Length - 11, "1"));
            Lzw.Decode(fileStream, outFile, trie);
            break;
        }
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

