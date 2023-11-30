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
        runResults.Aggregate((result, (int ok, int errors, int ignored))) =>  =>
        {
            
        });
    }
}