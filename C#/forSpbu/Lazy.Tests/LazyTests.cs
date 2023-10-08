using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Lazy.Tests;

public class Tests
{
    private class TestException : Exception
    {
        public TestException()
        {
        }
    }
    
    private class LazyTester
    {
        public int CallCounter;

        public int FstTest()
        {
            CallCounter++;
            return 1;
        }
    
        public int? SecTest()
        {
            CallCounter++;
            return null;
        }
    
        public int TrdTest()
        {
            CallCounter++;
            throw new TestException();
        }
    }

    private static IEnumerable<TestCaseData> IntLazyImpl()
    {
        yield return new TestCaseData((Func<int> lazyDelegate) => new SingleThreadedLazy<int>(lazyDelegate));
        yield return new TestCaseData((Func<int> lazyDelegate) => new MultiThreadedLazy<int>(lazyDelegate));
    }

    private static IEnumerable<TestCaseData> NullableIntLazyImpl()
    {
        yield return new TestCaseData((Func<int?> lazyDelegate) => new SingleThreadedLazy<int?>(lazyDelegate));
        yield return new TestCaseData((Func<int?> lazyDelegate) => new MultiThreadedLazy<int?>(lazyDelegate));
    }

    [Test, TestCaseSource(nameof(IntLazyImpl))]
    public void GetTest(Func<Func<int>, ILazy<int>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.FstTest);
        Assert.Multiple(() =>
        {
            Assert.That(tester.CallCounter, Is.EqualTo(0));
            Assert.That(lazy.Get(), Is.EqualTo(1));
            Assert.That(tester.CallCounter, Is.EqualTo(1));
            
            Assert.That(lazy.Get(), Is.EqualTo(1));
            Assert.That(tester.CallCounter, Is.EqualTo(1));

            Assert.That(lazy.Get(), Is.EqualTo(1));
            Assert.That(tester.CallCounter, Is.EqualTo(1));
        });
    }
    
    [Test, TestCaseSource(nameof(NullableIntLazyImpl))]
    public void GetNullTest(Func<Func<int?>, ILazy<int?>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.SecTest);
        Assert.Multiple(() =>
        {
            Assert.That(tester.CallCounter, Is.EqualTo(0));
            Assert.That(lazy.Get(), Is.EqualTo(null));
            Assert.That(tester.CallCounter, Is.EqualTo(1));
            
            Assert.That(lazy.Get(), Is.EqualTo(null));
            Assert.That(tester.CallCounter, Is.EqualTo(1));

            Assert.That(lazy.Get(), Is.EqualTo(null));
            Assert.That(tester.CallCounter, Is.EqualTo(1));
        });
    }
    
    [Test, TestCaseSource(nameof(IntLazyImpl))]
    public void GetExceptionTest(Func<Func<int>, ILazy<int>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.TrdTest);
        Assert.Multiple(() =>
        {
            Assert.That(tester.CallCounter, Is.EqualTo(0));
            Assert.Throws<TestException>(() => lazy.Get());
            Assert.That(tester.CallCounter, Is.EqualTo(1));
            
            Assert.Throws<TestException>(() => lazy.Get());
            Assert.That(tester.CallCounter, Is.EqualTo(1));

            Assert.Throws<TestException>(() => lazy.Get());
            Assert.That(tester.CallCounter, Is.EqualTo(1));
        });
    }

    [Test]
    public void MultiThreadMultiGetTest()
    {
        var tester = new LazyTester();
        var lazy = new MultiThreadedLazy<int>(tester.FstTest);
        
        var threads = new Thread[4];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() => {
                Assert.Multiple(() =>
                {
                    Assert.That(lazy.Get(), Is.EqualTo(1));
                    Assert.That(tester.CallCounter, Is.EqualTo(1));
                });
            });
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }
        
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}