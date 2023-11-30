namespace Md5Calculator;

using System.Security.Cryptography;

public static class Md5Calculator
{
    public static async Task<byte[]> ComputeAsync(string path)
    {
        if (!Directory.Exists(path) && !File.Exists(path))
        {
            throw new CalculatorException("Path doesnt exist");
        }
        
        return !Directory.Exists(path) ? await ComputeFileAsync(path) : await ComputeDirAsync(path);
    }
    
    public static byte[] Compute(string path)
    {
        if (!Directory.Exists(path) && !File.Exists(path))
        {
            throw new CalculatorException("Path doesnt exist");
        }
        
        return !Directory.Exists(path) ? ComputeFile(path) : ComputeDir(path);
    }


    private static async Task<byte[]> ComputeDirAsync(string path)
    {
        Console.WriteLine("Async dir");
        foreach (var file in Directory.GetFiles(path))
        {
            Console.WriteLine(file);
        }
        var fileTasks = Directory.GetFiles(path).Select(ComputeFileAsync);

        var dirTasks = Directory.GetDirectories(path).Select(ComputeDirAsync);

        var tasks = fileTasks
            .Concat(dirTasks)
            .Append(new Task<byte[]>(() => ComputeString(Path.GetDirectoryName(path)!)));
        
        var overallBytes = (await Task.WhenAll(tasks)).SelectMany(byteArray => byteArray).ToArray();
        return MD5.HashData(overallBytes);
    }
    
    private static byte[] ComputeDir(string path)
    {
        var fileBytes = Directory.GetFiles(path).SelectMany(ComputeFile);

        var dirBytes = Directory.GetDirectories(path).SelectMany(ComputeDir);
        
        var overallBytes = fileBytes.Concat(dirBytes).Concat(ComputeString(Path.GetDirectoryName(path)!)).ToArray();
        
        return MD5.HashData(overallBytes);
    }
    
    private static async Task<byte[]> ComputeFileAsync(string path)
    {
        return await new Task<byte[]>(() =>
        {
            var fileStream = File.Open(path, FileMode.Open);
            return MD5.HashData(fileStream);
        });
    }
    
    private static byte[] ComputeFile(string path)
    {
        return MD5.HashData(File.Open(path, FileMode.Open));
    }
    
    private static byte[] ComputeString(string @string)
    {
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(@string);
        return MD5.HashData(inputBytes);
    }
}