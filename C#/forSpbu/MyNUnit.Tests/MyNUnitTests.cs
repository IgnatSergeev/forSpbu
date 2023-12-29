using System.Reflection;
using MyNUnit.Attributes;
using TestProject;

namespace MyNUnit.Tests;

public class Tests
{
    [NUnit.Framework.Test]
    public void TestResultContent()
    {
        Console.WriteLine(typeof(ArgumentNullException));
        var assembly = Assembly.GetAssembly(typeof(TestClass));
        var results = Tester.TestAssembly(assembly!).ToArray();
        Assert.Multiple(() =>
        {
            var ignoreTest = results.First(result => result.GetMethod().Name == "TestIgnore");
            Assert.That(ignoreTest.Ignored, Is.True);
            
            var testFails = results.First(result => result.GetMethod().Name == "TestFails");
            Assert.That(testFails.ExpectedException, Is.False);
            Assert.That(testFails is { Ignored: false, Passed: false }, Is.True);
            
            var test = results.First(result => result.GetMethod().Name == "Test");
            Assert.That(test.ExpectedException, Is.False);
            Assert.That(test is { Ignored: false, Passed: true }, Is.True);
            
            var testExpected = results.First(result => result.GetMethod().Name == "TestExpected");
            Assert.That(testExpected.ExpectedException, Is.True);
            Assert.That(testExpected is { Ignored: false, Passed: true }, Is.True);
            
            var testExpectedFails = results.First(result => result.GetMethod().Name == "TestExpectedFails");
            Assert.That(testExpectedFails.ExpectedException, Is.True);
            Assert.That(testExpectedFails is { Ignored: false, Passed: false }, Is.True);
        });
        
        var methodsOrder = TestClass.Dictionary;
        var befores = assembly!.GetType("TestProject.TestClass")!.GetMethods()
            .Where(method => method.GetCustomAttributes<Before>().Any())
            .ToArray();
        var afters = assembly!.GetType("TestProject.TestClass")!.GetMethods()
            .Where(method => method.GetCustomAttributes<After>().Any())
            .ToArray();
        var tests = assembly!.GetType("TestProject.TestClass")!.GetMethods()
            .Where(method => method.GetCustomAttributes<Test>().Any())
            .ToArray();
        var afterClasses = assembly!.GetType("TestProject.TestClass")!.GetMethods()
            .Where(method => method.IsStatic && method.GetCustomAttributes<AfterClass>().Any())
            .ToArray();
        var beforeClasses = assembly!.GetType("TestProject.TestClass")!.GetMethods()
            .Where(method => method.IsStatic && method.GetCustomAttributes<BeforeClass>().Any())
            .ToArray();
        foreach (var method in assembly!.GetType("TestProject.TestClass")!.GetMethods())
        {
            if (method.IsStatic && method.GetCustomAttributes<BeforeClass>().Any())
            {
                foreach (var before in befores)
                {
                    Assert.That(methodsOrder[before.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }
                foreach (var after in afters)
                {
                    Assert.That(methodsOrder[after.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }
                foreach (var test in tests)
                {
                    Assert.That(methodsOrder[test.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }
                foreach (var afterClass in afterClasses)
                {
                    Assert.That(methodsOrder[afterClass.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }

                continue;
            }
            
            if (method.IsStatic && method.GetCustomAttributes<AfterClass>().Any())
            {
                foreach (var before in befores)
                {
                    Assert.That(methodsOrder[before.Name], Is.LessThan(methodsOrder[method.Name])); 
                }
                foreach (var after in afters)
                {
                    Assert.That(methodsOrder[after.Name], Is.LessThan(methodsOrder[method.Name])); 
                }
                foreach (var test in tests)
                {
                    Assert.That(methodsOrder[test.Name], Is.LessThan(methodsOrder[method.Name])); 
                }
                continue;
            }
            
            if (method.GetCustomAttributes<Before>().Any())
            {
                foreach (var after in afters)
                {
                    Assert.That(methodsOrder[after.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }
                foreach (var test in tests)
                {
                    Assert.That(methodsOrder[test.Name], Is.GreaterThan(methodsOrder[method.Name])); 
                }
                continue;
            }
            
            if (method.GetCustomAttributes<After>().Any())
            {
                foreach (var test in tests)
                {
                    Assert.That(methodsOrder[test.Name], Is.LessThan(methodsOrder[method.Name])); 
                }
            }
        }
    }
}