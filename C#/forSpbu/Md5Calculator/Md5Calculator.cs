namespace Md5Calculator;

using System.Security.Cryptography;

/// <summary>
/// Represents md5 hash calculator
/// </summary>
public static class Md5Calculator
{
    /// <summary>
    /// Asynchronously computes md5 hash of given path
    /// </summary>
    /// <param name="path">Path to compute</param>
    /// <returns>Hash as byte array</returns>
    /// <exception cref="CalculatorException">If given does not exist</exception>
    public static async Task<byte[]> ComputeAsync(string path)
    {
        if (!Directory.Exists(path) && !File.Exists(path))
        {
            throw new CalculatorException("Path doesnt exist");
        }
        
        return !Directory.Exists(path) ? await ComputeFileAsync(path) : await ComputeDirAsync(path);
    }
    
    /// <summary>
    /// Synchronously computes md5 hash of given path
    /// </summary>
    /// <param name="path">Path to compute</param>
    /// <returns>Hash as byte array</returns>
    /// <exception cref="CalculatorException">If given does not exist</exception>
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
        var fileTasks = Directory.GetFiles(path).Select(ComputeFileAsync);

        var dirTasks = Directory.GetDirectories(path).Select(ComputeDirAsync);

        var tasks = fileTasks.Concat(dirTasks).ToArray();
        
        var overallBytes = (await Task.WhenAll(tasks))
            .SelectMany(byteArray => byteArray)
            .Concat(ComputeString(new DirectoryInfo(path).Name))
            .ToArray();
        return MD5.HashData(overallBytes);
    }
    
    private static byte[] ComputeDir(string path)
    {
        var fileBytes = Directory.GetFiles(path).SelectMany(ComputeFile);

        var dirBytes = Directory.GetDirectories(path).SelectMany(ComputeDir);
        
        var overallBytes = fileBytes.Concat(dirBytes).Concat(ComputeString(new DirectoryInfo(path).Name)).ToArray();
        
        return MD5.HashData(overallBytes);
    }

    private static async Task<byte[]> ComputeFileAsync(string path)
    {
        var byteArray = (await MD5.HashDataAsync(new FileInfo(path).OpenRead())).Concat(ComputeString(new FileInfo(path).Name)).ToArray();
        return MD5.HashData(byteArray);
    }

    private static byte[] ComputeFile(string path)
    {
        var byteArray = MD5.HashData(new FileInfo(path).OpenRead()).Concat(ComputeString(new FileInfo(path).Name)).ToArray();
        return MD5.HashData(byteArray);
    }
    
    private static byte[] ComputeString(string @string) => 
        MD5.HashData(System.Text.Encoding.ASCII.GetBytes(@string));
}