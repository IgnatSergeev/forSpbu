using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using MyNUnit;
using MyNUnit.Attributes;

namespace TestProject;

public class TestClass
{
    public static readonly ConcurrentDictionary<string, int> Dictionary = new ();
    private static int _counter;
    
    [BeforeClass]
    public static void BeforeClass()
    {
        Dictionary[nameof(BeforeClass)] = _counter++;
    }
    
    [AfterClass]
    public static void AfterClass()
    {
        Dictionary[nameof(AfterClass)] = _counter++;
    }
    
    [Before]
    public static void Before1()
    {
        Dictionary[nameof(Before1)] = _counter++;
    }
    
    [Before]
    public static void Before2()
    {
        Dictionary[nameof(Before2)] = _counter++;
    }
    
    [After]
    public static void After1()
    {
        Dictionary[nameof(After1)] = _counter++;
    }
    
    [After]
    public static void After2()
    {
        Dictionary[nameof(After2)] = _counter++;
    }
    
    [Test(Expected = typeof(ArgumentNullException))]
    public static void TestExpected()
    {
        Dictionary[nameof(TestExpected)] = _counter++;
        throw new ArgumentNullException();
    }
    
    [Test(Expected = typeof(ArgumentNullException))]
    public static void TestExpectedFails()
    {
        Dictionary[nameof(TestExpectedFails)] = _counter++;
    }
    
    [Test]
    public static void Test()
    {
        Dictionary[nameof(Test)] = _counter++;
    }
    
    [Test]
    public static void TestFails()
    {
        Dictionary[nameof(TestFails)] = _counter++;
        throw new ArgumentNullException();
    }
    
    [Test(Ignore = "")]
    public static void TestIgnore()
    {
        Dictionary[nameof(TestIgnore)] = _counter++;
    }
}