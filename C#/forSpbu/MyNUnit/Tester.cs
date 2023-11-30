using System.Reflection;

namespace MyNUnit;


public static class Tester
{
    public static IEnumerable<RunResult> TestAssembly(Assembly assembly)
        => assembly.GetExportedTypes().AsParallel().SelectMany(TestType);

    private static IEnumerable<RunResult> TestType(Type type)
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
                befores.AsParallel().ForAll(before => before.Invoke(null, null));

                var result = RunTest(method);

                afters.AsParallel().ForAll(after => after.Invoke(null, null));

                return result;
            });

        methods.Where(method => method.IsStatic && method.GetCustomAttributes<AfterClass>().Any())
            .AsParallel()
            .ForAll(method => method.Invoke(null, null));

        return results;
    }

    private static RunResult RunTest(MethodInfo method)
    {
        var ignore = method.GetCustomAttributes<Test>().First().Ignore;
        try
        {
            method.Invoke(null, null);
        }
        catch (AggregateException e)
        {
            return RunResult.Create(method.Name, e.InnerException, ignore);
        }

        return RunResult.Create(method.Name, null, ignore);
    }
}