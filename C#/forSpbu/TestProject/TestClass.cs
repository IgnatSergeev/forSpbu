using System.Collections.Concurrent;
using MyNUnit;

namespace TestProject;

public class TestClass
{
    public static readonly ConcurrentQueue<string> Queue = new ();
    int test
    
    [BeforeClass]
    public static void BeforeClass()
    {
        Queue.Enqueue(nameof(BeforeClass));
    }
    
    [AfterClass]
    public static void AfterClass()
    {
        Queue.Enqueue(nameof(AfterClass));
    }
    
    [Before]
    public static void Before1()
    {
        Queue.Enqueue(nameof(Before1));
    }
    
    [Before]
    public static void Before2()
    {
        Queue.Enqueue(nameof(Before2));
    }
    
    [After]
    public static void After1()
    {
        Queue.Enqueue(nameof(After1));
    }
    
    [After]
    public static void After2()
    {
        Queue.Enqueue(nameof(After2));
    }
    
    [Test(Expected = ArgumentNullException)]
    public static void TestExpected()
    {
        Queue.Enqueue(nameof(TestExpected));
        throw new ArgumentNullException();
    }
    
    [Test(Expected = ArgumentNullException)]
    public static void TestExpectedFails()
    {
        Queue.Enqueue(nameof(TestExpectedFails));
    }
    
    [Test]
    public static void TestFails()
    {
        Queue.Enqueue(nameof(TestExpectedFails));
        throw new ArgumentNullException();
    }
    
    [Test(Ignore = "")]
    public static void TestIgnore()
    {
        Queue.Enqueue(nameof(TestIgnore));
    }
}