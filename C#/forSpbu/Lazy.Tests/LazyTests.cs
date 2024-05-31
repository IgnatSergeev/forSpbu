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
        public int CallCounter => this._callCounter;
        private int _callCounter;

        public int IntTest()
        {
            Interlocked.Increment(ref this._callCounter);
            return 1;
        }
    
        public int? NullTest()
        {
            Interlocked.Increment(ref this._callCounter);
            return null;
        }
    
        public int ExceptionTest()
        {
            Interlocked.Increment(ref this._callCounter);
            throw new TestException();
        }
    }

    private static IEnumerable<TestCaseData> IntLazyImplementations()
    {
        yield return new TestCaseData((Func<int> lazyDelegate) => new SingleThreadedLazy<int>(lazyDelegate));
        yield return new TestCaseData((Func<int> lazyDelegate) => new MultiThreadedLazy<int>(lazyDelegate));
    }

    private static IEnumerable<TestCaseData> NullableIntLazyImplementations()
    {
        yield return new TestCaseData((Func<int?> lazyDelegate) => new SingleThreadedLazy<int?>(lazyDelegate));
        yield return new TestCaseData((Func<int?> lazyDelegate) => new MultiThreadedLazy<int?>(lazyDelegate));
    }

    [Test, TestCaseSource(nameof(IntLazyImplementations))]
    public void GetTest(Func<Func<int>, ILazy<int>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.IntTest);
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
    
    [Test, TestCaseSource(nameof(NullableIntLazyImplementations))]
    public void GetNullTest(Func<Func<int?>, ILazy<int?>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.NullTest);
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
    
    [Test, TestCaseSource(nameof(IntLazyImplementations))]
    public void GetExceptionTest(Func<Func<int>, ILazy<int>> lazyCreator)
    {
        var tester = new LazyTester();
        var lazy = lazyCreator(tester.ExceptionTest);
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
        var lazy = new MultiThreadedLazy<int>(tester.IntTest);
        var startEvent = new ManualResetEvent(false);
        
        var threads = new Thread[10];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                startEvent.WaitOne();
                Assert.Multiple(() =>
                {
                    Assert.That(lazy.Get(), Is.EqualTo(1));
                    Assert.That(tester.CallCounter, Is.EqualTo(1));
                });
            });
            threads[i].Start();
        }

        startEvent.Set();
        
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}