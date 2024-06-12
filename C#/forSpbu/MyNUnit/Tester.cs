using System.Reflection;
using MyNUnit.Attributes;

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
        var afters = methods.Where(method => method.GetCustomAttributes<After>().Any());
        
        methods.Where(method => method.IsStatic && method.GetCustomAttributes<BeforeClass>().Any())
            .AsParallel()
            .ForAll(method => method.Invoke(null, null));
        
        var results = methods.Where(method => method.GetCustomAttributes<Test>().Any())
            .AsParallel()
            .Select(method =>
            {
                var instance = Activator.CreateInstance(type);
                var testAttr = method.GetCustomAttributes<Test>().First();
                befores.AsParallel().ForAll(before => before.Invoke(instance, null));

                var result = RunTest(method, instance);

                afters.AsParallel().ForAll(after => after.Invoke(instance, null));

                var expectedResult = RunResult.Create(method.DeclaringType, method, testAttr.Expected);

                return new TestResult(result, expectedResult, testAttr.Ignore);
            });

        methods.Where(method => method.IsStatic && method.GetCustomAttributes<AfterClass>().Any())
            .AsParallel()
            .ForAll(method => method.Invoke(null, null));
        
        return results;
    }

    private static RunResult RunTest(MethodInfo method, object? instance)
    {
        try
        {
            method.Invoke(instance, null);
        }
        catch (TargetInvocationException e)
        {
            return RunResult.Create(method.DeclaringType, method, e.InnerException?.GetType());
        }

        return RunResult.Create(method.DeclaringType, method, null);
    }
}