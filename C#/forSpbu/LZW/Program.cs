using LZW;
using Trie;

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
    switch (key)
    {
        case "-c":
        {
            using (var inputFileStream = File.Open(pathToFile, FileMode.Open))
            {
                using var outputFileStream = BufferedFileStream.Create(pathToFile + ".zipped");
                Lzw.Encode(inputFileStream, outputFileStream, new TrieRealisation());
            }
            
            var inputFileSize = new FileInfo(pathToFile).Length;
            var encodedFileSize = new FileInfo(pathToFile + ".zipped").Length;
            if (encodedFileSize == 0)
            {
                Console.WriteLine("Size of the file encoded with lzw = 0");
            }
            else
            {
                var compressionRatioWithoutBwt = (double)inputFileSize / (double)encodedFileSize;
                Console.WriteLine("Compression ratio without bwt = " + compressionRatioWithoutBwt);
            }

            var fileByteArray = File.ReadAllBytes(pathToFile);
            var (burrowsEncodedCharArray, indexOfInitialArray) = BurrowsWheeler.BurrowsWheeler.Encode(fileByteArray.Select(x => (char)x).ToArray());
            var burrowsEncodedByteArray = burrowsEncodedCharArray.Select(x => (byte)x).ToArray();
            var indexByteArray = BitConverter.GetBytes(indexOfInitialArray);

            using (var bwtFileStream = File.Create(pathToFile + ".bwt"))
            {
                foreach (var element in indexByteArray)
                {
                    bwtFileStream.WriteByte(element);
                }
                
                foreach (var element in burrowsEncodedByteArray)
                {
                    bwtFileStream.WriteByte(element);
                }

                bwtFileStream.Seek(0, SeekOrigin.Begin);
                using var encodedBwtFileStream = BufferedFileStream.Create(pathToFile + ".bwt.zipped");
                Lzw.Encode(bwtFileStream, encodedBwtFileStream, new TrieRealisation());
            }
            File.Delete(pathToFile + ".bwt");
            
            var bwtEncodedFileSize = new FileInfo(pathToFile + ".bwt.zipped").Length;
            if (bwtEncodedFileSize == 0)
            {
                Console.WriteLine("Size of the file encoded with lzw using bwt = 0");
            }
            else
            {
                var compressionRatioWithBwt = (double)inputFileSize / (double)bwtEncodedFileSize;
                Console.WriteLine("Compression ratio with bwt = " + compressionRatioWithBwt);
            }

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
            using var outputFileStream = File.Create(pathToFile.Remove(pathToFile.Length - 7));
            Lzw.Decode(inputFileStream, outputFileStream, new TrieRealisation());
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

