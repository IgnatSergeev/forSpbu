using BenchmarkDotNet.Attributes;
namespace MatrixMult.Benchmark;

public class MatrixMultBenchmark
{
    [Params(16, 32, 64, 128, 256, 512, 1024, 2048)]
    private int _size;
    private Matrix _fst;
    private Matrix _sec;
    
    [GlobalSetup]
    public void GenerateRandomMatrix(int size)
    {
        var random = new Random();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                _fst.SetElement(random.Next(), i, j);
                _sec.SetElement(random.Next(), i, j);
            }
        }
    }
    
    [Benchmark]
    public void MultiThreaded()
    {
        MatrixMultiplier.MultiThreaded(_fst, _sec);
    }
    
    [Benchmark]
    public void SingleThreaded()
    {
        MatrixMultiplier.SingleThreaded(_fst, _sec);
    }
}