using BenchmarkDotNet.Attributes;
namespace MatrixMult.Benchmark;

public class MatrixMultBenchmark
{
    [Params(12, 24, 48, 96, 192, 384, 768, 1536)]
    public int Size;
    private Matrix _fst = new Matrix();
    private Matrix _sec = new Matrix();
    
    [GlobalSetup]
    public void GenerateRandomMatrix()
    {
        var random = new Random();
        var fstElements = new int[Size, Size];
        var secElements = new int[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                fstElements[i, j] = random.Next();
                secElements[i, j] = random.Next();
            }
        }

        _fst = new Matrix(fstElements);
        _sec = new Matrix(secElements);
    }
    
    [Benchmark]
    public void MultiThreaded()
    {
        MatrixMultiplier.MultiThreadedMultiply(_fst, _sec);
    }
    
    [Benchmark]
    public void SingleThreaded()
    {
        MatrixMultiplier.SingleThreadedMultiply(_fst, _sec);
    }
}