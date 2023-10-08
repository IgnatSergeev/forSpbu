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
        public int CallCounter = 0;

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

    [Test]
    public void SingleThreadGetTest()
    {
        var tester = new LazyTester();
        var lazy = new SingleThreadedLazy<int>(tester.FstTest);
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
    
    [Test]
    public void SingleThreadGetNullTest()
    {
        var tester = new LazyTester();
        var lazy = new SingleThreadedLazy<int?>(tester.SecTest);
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
    
    [Test]
    public void SingleThreadGetExceptionTest()
    {
        var tester = new LazyTester();
        var lazy = new SingleThreadedLazy<int>(tester.TrdTest);
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
    public void MultiThreadGetTest()
    {
        var tester = new LazyTester();
        var lazy = new MultiThreadedLazy<int>(tester.FstTest);
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
    
    [Test]
    public void MultiThreadGetNullTest()
    {
        var tester = new LazyTester();
        var lazy = new MultiThreadedLazy<int?>(tester.SecTest);
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
    
    [Test]
    public void MultiThreadGetExceptionTest()
    {
        var tester = new LazyTester();
        var lazy = new MultiThreadedLazy<int>(tester.TrdTest);
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