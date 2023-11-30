using System.Reflection;

namespace MyNUnit;

/// <summary>
/// Represents test runner
/// </summary>
public static class Tester
{
    /// <summary>
    /// Tests assembly
    /// </summary>
    /// <param name="assembly">Assembly to test</param>
    /// <returns>Test results</returns>
    public static IEnumerable<TestResult> TestAssembly(Assembly assembly)
        => assembly.GetExportedTypes().AsParallel().SelectMany(TestType);

    private static IEnumerable<TestResult> TestType(Type type)
    {
        var methods = type.GetMethods();
        var befores = methods.Where(method => method.GetCustomAttributes<Before>().Any());
        var afters = methods.Where(method => method.GetCustomAttributes<Before>().Any());
        
        methods.Where(method => method.IsStatic && method.GetCustomAttributes<BeforeClass>().Any())
            .AsParallel()
            .ForAll(method => method.Invoke(null, null));

        var results = methods.Where(method => method.GetCustomAttributes<Test>().Any())
            .AsParallel()
            .Select(method =>
            {
                var testAttr = method.GetCustomAttributes<Test>().First();
                befores.AsParallel().ForAll(before => before.Invoke(null, null));

                var result = RunTest(method);

                afters.AsParallel().ForAll(after => after.Invoke(null, null));

                var expectedResult = RunResult.Create(method.DeclaringType?.Name, method.Name, testAttr.Expected);

                return new TestResult(result, expectedResult, testAttr.Ignore);
            });

        methods.Where(method => method.IsStatic && method.GetCustomAttributes<AfterClass>().Any())
            .AsParallel()
            .ForAll(method => method.Invoke(null, null));
        
        return results;
    }

    private static RunResult RunTest(MethodInfo method)
    {
        try
        {
            method.Invoke(null, null);
        }
        catch (AggregateException e)
        {
            return RunResult.Create(method.DeclaringType?.Name, method.Name, e.InnerException);
        }

        return RunResult.Create(method.DeclaringType?.Name, method.Name, null);
    }
}