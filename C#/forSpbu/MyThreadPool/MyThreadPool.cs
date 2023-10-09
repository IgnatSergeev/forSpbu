using System.Collections.Concurrent;

namespace MyThreadPool;

public class MyThreadPool
{
    public MyThreadPool(int size)
    {
        if (size <= 0)
        {
            throw new MyThreadCreationException();
        }
        
        this._threads = new Thread[size];
        for (int i = 0; i < size; i++)
        {
            this._threads[i] = new Thread(() =>
            {
                while (true)
                {
                    this._functions.TryDequeue(out var func);
                    func?.Invoke();
                }
            });
        }
    }

    public IMyTask<TResult> Submit<TResult>(Func<TResult> func)
    {
        var finishEvent = new ManualResetEvent(false);
        var task = new MyTask<TResult>();
        this._functions.Enqueue((() =>
        {
            lock(task)
            {
                try
                {
                    var result = func();
                    task.FuncFinished(result);
                }
                catch (Exception e)
                {
                    task.FuncFinished(e);
                }
                finally
                {
                    finishEvent.Set();
                }
            }
        }, finishEvent));
        return task;
    }

    public void Shutdown()
    {
        _isTerminated = true;
    }
    
    private readonly ConcurrentQueue<(Action action, ManualResetEvent finishEvent)> _functions = new ();
    private readonly Thread[] _threads;
    private Semaphore stop;
    private bool _isTerminated;
}