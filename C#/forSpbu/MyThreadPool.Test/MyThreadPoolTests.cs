namespace MyThreadPool.Test;

public class MyThreadPoolTests
{
    private class PoolTester
    {
        public int Fst()
        {
            Counter++;
            var result = 0;
            for (int i = 0; i < 1000; i++)
            {
                result += i;
            }

            return result;
        }

        public int Counter { get; private set; }
    }

    private static int TestFunc()
    {
        var result = 0;
        for (int i = 0; i < 1000; i++)
        {
            result += i;
        }

        return result;
    }

    private MyThreadPool? _pool;
    private int _procAmount;
    private int _threadsAmount;
    
    [SetUp]
    public void Setup()
    {
        this._threadsAmount = 4;
        this._procAmount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
        this._pool = new MyThreadPool(this._threadsAmount);
    }
    
    [Test]
    public void SubmitResultTest()
    {
        Assert.That(_pool!.Submit(TestFunc).Result(), Is.EqualTo(499500));
    }
    
    [Test]
    public void SubmitContinueWithResultTest()
    {
        Assert.That(_pool!.Submit(TestFunc).ContinueWith((int result) => result + 1).Result(), Is.EqualTo(499501));
    }
    
    [Test]
    public void DoubleContinueWithResultTest()
    {
        Assert.That(_pool!.Submit(TestFunc).ContinueWith((int result) => result + 1).ContinueWith((int result) => result.ToString()).Result(), Is.EqualTo("499501"));
    }
    
    [Test]
    public void SubmitResultContinueWithResultTest()
    {
        var task = _pool!.Submit(TestFunc);
        task.Result();
        
        Assert.That(task.ContinueWith((int result) => result + 1).Result(), Is.EqualTo(499501));
    }
    
    [Test]
    public void LazyResultTest()
    {
        var tester = new PoolTester();
        var task = _pool!.Submit(tester.Fst);
        
        task.Result();
        Assert.That(tester.Counter, Is.EqualTo(1));
        task.Result();
        Assert.That(tester.Counter, Is.EqualTo(1));
    }
    
    [Test]
    public void IsCompletedTest()
    {
        var startEvent = new ManualResetEvent(false);
        var task = _pool!.Submit(() =>
        {
            startEvent.WaitOne();
            return TestFunc();
        });
        
        Assert.That(task.IsCompleted, Is.False);
        startEvent.Set();
        task.Result();
        Assert.That(task.IsCompleted, Is.True);
        task.Result();
        Assert.That(task.IsCompleted, Is.True);
    }
    
    [Test]
    public void ShutdownTest()
    {
        var fstTask = _pool!.Submit(TestFunc);
        var secTask = _pool!.Submit(TestFunc);
        _pool!.Shutdown();
        Assert.That(System.Diagnostics.Process.GetCurrentProcess().Threads, Has.Count.EqualTo(_procAmount));
        
        Assert.Multiple(() =>
        {
            Assert.That(fstTask.IsCompleted, Is.True);
            Assert.That(secTask.IsCompleted, Is.True);
        });

        _pool!.Submit(TestFunc);
        fstTask.ContinueWith((int result) => result + 1);
        Assert.That(System.Diagnostics.Process.GetCurrentProcess().Threads, Has.Count.EqualTo(_procAmount));
    }
    
    [Test]
    public void DoubleContinueWithTest()
    {
        var fstTask = _pool!.Submit(TestFunc);
        var fstContinueTask = fstTask.ContinueWith((int result) => result + 1);
        var secContinueTask = fstTask.ContinueWith((int result) => result.ToString());
        
        Assert.Multiple(() =>
        {
            Assert.That(fstContinueTask.Result(), Is.EqualTo(499501));
            Assert.That(secContinueTask.Result(), Is.EqualTo("499500"));
        });
    }
    
    [Test]
    public void ThreadsCountTest()
    {
        Assert.That(System.Diagnostics.Process.GetCurrentProcess().Threads, Has.Count.EqualTo(_procAmount + 4));
    }
}