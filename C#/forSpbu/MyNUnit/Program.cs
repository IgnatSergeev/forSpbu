using System.Reflection;
using MyNUnit;

if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
{
    Console.WriteLine("Incorrect argument, should be project assembly(binary or dll) path");
}

var assemblies = Directory.EnumerateFiles(args[0]).Where(file => file.EndsWith(".dll")).Select(Assembly.LoadFile);

var results = assemblies.SelectMany(Tester.TestAssembly);
var summary = results.Aggregate<TestResult, (int ok, int errors, int ignored)>((0, 0, 0), (summary, result) =>
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
