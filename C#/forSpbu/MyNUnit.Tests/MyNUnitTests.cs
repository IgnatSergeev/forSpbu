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
    }
}