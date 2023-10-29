using System.Collections.Concurrent;

namespace MyThreadPool;

/// <summary>
/// Thread pool for concurrent delegate calculation
/// </summary>
public class MyThreadPool
{
    /// <summary>
    /// Creates my thread pool with given number of threads
    /// </summary>
    /// <param name="size">Thread count</param>
    /// <exception cref="MyThreadCreationException">If size is invalid</exception>
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

    /// <summary>
    /// Add new func to a pool queue
    /// </summary>
    /// <param name="func">Func to add</param>
    /// <typeparam name="TResult">Func return type</typeparam>
    /// <returns>I my task instance of one added to pool</returns>
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

    /// <summary>
    /// Softly shuts down a pool, blocks called thread until all running tasks finished
    /// </summary>
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