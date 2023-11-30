using System.Reflection;

namespace MyNUnit;

internal static class Program
{
    public static void Main(string[] argv)
    {
        if (argv.Length != 1 || string.IsNullOrEmpty(argv[0]))
        {
            Console.WriteLine("Incorrect argument, should be project assembly(binary or dll) path");
        }

        var assembly = Assembly.LoadFile(argv[0]);

        var runResults = Tester.TestAssembly(assembly);
        var summary = runResults.Aggregate<TestResult, (int ok, int errors, int ignored)>((0, 0, 0), (summary, result) =>
        {
            if (result.Ignored)
            {
                summary.ignored++;
            }
            if (result.Passed)
            {
                summary.ok++;
            }
            else
            {
                summary.errors++;
            }
            
            Console.WriteLine(result.ToString());

            return summary;
        });
        
        Console.WriteLine($"Summary: {summary.ok} ok, {summary.errors} failed, {summary.ok} ignored");
    }
}