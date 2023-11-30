namespace Md5Calculator.Tests;

public class Md5CalculatorTests
{
    private static IEnumerable<Func<string, byte[]>> ComputeInstances()
    {
        yield return Md5Calculator.Compute;
        yield return path => Md5Calculator.ComputeAsync(path).Result;
    }
    
    [Test]
    public void AsyncEqualsSyncTest()
    {
        var dirNames = new []
        {
            "./AsyncEqualsSyncTest",
            "./AsyncEqualsSyncTest/Dir1",
        };
        (string path, string data)[] files = new []
        {
            ("./AsyncEqualsSyncTest/Dir1/file1.txt", "Asd"),
            ("./AsyncEqualsSyncTest/file1.txt", "Asd"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(Md5Calculator.Compute(dirNames[0]), Is.EqualTo(Md5Calculator.ComputeAsync(dirNames[0]).Result));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
        Directory.Delete(dirNames[1]);
    }
    
    [Test, TestCaseSource(nameof(ComputeInstances))]
    public void RecalculateEqualsTest(Func<string, byte[]> compute)
    {
        var dirNames = new []
        {
            "./RecalculateEqualsTest",
            "./RecalculateEqualsTest/Dir1",
        };
        (string path, string data)[] files = new []
        {
            ("./RecalculateEqualsTest/Dir1/file1.txt", "Asd"),
            ("./RecalculateEqualsTest/file1.txt", "Asd"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(compute(dirNames[0]), Is.EqualTo(compute(dirNames[0])));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
        Directory.Delete(dirNames[1]);
    }
    
    [Test, TestCaseSource(nameof(ComputeInstances))]
    public void FileNameMattersTest(Func<string, byte[]> compute)
    {
        var dirNames = new []
        {
            "./FileNameMattersTest",
        };
        (string path, string data)[] files = new []
        {
            ("./FileNameMattersTest/file2.txt", "Asd"),
            ("./FileNameMattersTest/file1.txt", "Asd"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(compute(files[0].path), Is.Not.EqualTo(compute(files[1].path)));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
    }
    
    [Test, TestCaseSource(nameof(ComputeInstances))]
    public void DirNameMattersTest(Func<string, byte[]> compute)
    {
        var dirNames = new []
        {
            "./DirNameMattersTest/Dir1",
            "./DirNameMattersTest/Dirs5",
        };
        (string path, string data)[] files = new []
        {
            ("./DirNameMattersTest/Dir1/file1.txt", "Asd"),
            ("./DirNameMattersTest/Dirs5/file1.txt", "Asd"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(compute(dirNames[0]), Is.Not.EqualTo(compute(dirNames[1])));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
        dirNames.AsParallel().ForAll(path => Directory.Delete(path));
    }
    
    [Test, TestCaseSource(nameof(ComputeInstances))]
    public void DirFilesMatterTest(Func<string, byte[]> compute)
    {
        var dirNames = new []
        {
            "./DirFilesMatterTest/Dir1",
            "./DirFilesMatterTest/Dir2",
        };
        (string path, string data)[] files = new []
        {
            ("./DirFilesMatterTest/Dir1/file1.txt", "Asd"),
            ("./DirFilesMatterTest/Dir2/file1.txt", "Asde"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(compute(dirNames[0]), Is.Not.EqualTo(compute(dirNames[1])));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
        dirNames.AsParallel().ForAll(path => Directory.Delete(path));
    }
    
    [Test, TestCaseSource(nameof(ComputeInstances))]
    public void SubDirsMatterTest(Func<string, byte[]> compute)
    {
        var dirNames = new []
        {
            "./SubDirsMatterTest/Dir1",
            "./SubDirsMatterTest/Dir2",
            "./SubDirsMatterTest/Dir1/Dir11",
            "./SubDirsMatterTest/Dir2/Dir21",
        };
        (string path, string data)[] files = new []
        {
            ("./SubDirsMatterTest/Dir1/Dir11/file1.txt", "Asd"),
            ("./SubDirsMatterTest/Dir2/Dir21/file1.txt", "Asde"),
        };
        dirNames.AsParallel().ForAll(path => Directory.CreateDirectory(path));
        files.AsParallel()
            .ForAll(file => File.WriteAllBytes(file.path, System.Text.Encoding.ASCII.GetBytes(file.data)));
        
        Assert.That(compute(dirNames[0]), Is.Not.EqualTo(compute(dirNames[1])));
        
        files.AsParallel()
            .ForAll(file => File.Delete(file.path));
        Directory.Delete(dirNames[3]);
        Directory.Delete(dirNames[2]);
        Directory.Delete(dirNames[1]);
        Directory.Delete(dirNames[0]);



    }
}