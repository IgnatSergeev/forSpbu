using Md5Calculator;
using System.Diagnostics;

if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
{
    Console.WriteLine("Should be 1 argument with path");
    return;
}

try
{
    var startTime = Stopwatch.GetTimestamp();
    var hash = Md5Calculator.Md5Calculator.Compute(args[0]);
    var endTime = Stopwatch.GetTimestamp();
    Console.WriteLine($"Single thread: {Convert.ToBase64String(hash)}, {Stopwatch.GetElapsedTime(startTime, endTime)}");

    startTime = Stopwatch.GetTimestamp();
    hash = await Md5Calculator.Md5Calculator.ComputeAsync(args[0]);
    endTime = Stopwatch.GetTimestamp();
    Console.WriteLine($"Multi thread: {Convert.ToBase64String(hash)}, {Stopwatch.GetElapsedTime(startTime, endTime)}");
}
catch (CalculatorException e)
{
    Console.WriteLine(e.Message);
}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}