using MatrixMult.Benchmark;
using BenchmarkDotNet.Running;

var table = BenchmarkRunner.Run<MatrixMultBenchmark>();
Console.WriteLine(table);