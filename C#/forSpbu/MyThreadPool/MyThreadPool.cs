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
                while (!this._isTerminated)
                {
                    this._functions.TryDequeue(out var func);
                    func?.Invoke();
                }
            });
        }
    }

    public IMyTask<TResult> Submit<TResult>(Func<TResult> func)
    {
        var task = new MyTask<TResult>(func, this);
        this._functions.Enqueue(() =>
        {
            task.Execute();
            lock (task.NextTasks)
            {
                foreach (var taskDelegate in task.NextTasks)
                {
                    this._functions.Enqueue(taskDelegate);
                }
            }
        });
        return task;
    }

    public void Shutdown()
    {
        _isTerminated = true;
        foreach (var thread in _threads)
        {
            thread.Join();
        }
    }
    
    private readonly ConcurrentQueue<Action> _functions = new ();
    private readonly Thread[] _threads;
    private volatile bool _isTerminated;
}