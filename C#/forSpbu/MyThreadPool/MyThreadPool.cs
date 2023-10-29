using System.Collections.Concurrent;

namespace MyThreadPool;

/// <summary>
/// Thread pool for concurrent delegate calculation
/// </summary>
public class MyThreadPool : IDisposable
{
    private readonly ConcurrentQueue<Action> _functions = new ();
    private readonly Thread[] _threads;
    private volatile bool _isTerminated;
    
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
                    if (func != null)
                    {
                        func.Invoke();
                    }
                }
            });
            this._threads[i].Start();
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
    /// Softly shuts down the pool, blocks called thread until all running tasks finished
    /// </summary>
    public void Shutdown()
    {
        _isTerminated = true;
        foreach (var thread in _threads)
        {
            thread.Join();
        }
    }

    /// <summary>
    /// Shuts down the pool
    /// </summary>
    public void Dispose() => this.Shutdown();
    
    private class MyTask<TResult> : IMyTask<TResult>
    {
        private volatile Func<TResult>? _func;
        private readonly MyThreadPool _threadPool;
    
        private volatile Exception _exception = new AggregateException();
        private TResult? _result;
        private volatile bool _threwException;
        private bool _isCompleted;

        public bool IsCompleted => this._isCompleted;
        public readonly ConcurrentBag<Action> NextTasks = new ();
        
        public MyTask(Func<TResult> func, MyThreadPool threadPool)
        {
            this._func = func;
            this._threadPool = threadPool;
        }
    
        public TResult Result()
        {
            while (!Volatile.Read(ref this._isCompleted))
            {
            }

            if (this._threwException)
            {
                throw this._exception;
            }

            return this._result!;
        }

        public void Execute()
        {
            if (Volatile.Read(ref this._isCompleted)) return;
            lock (this._func!)
            {
                if (Volatile.Read(ref this._isCompleted)) return;
                try
                {
                    var result = this._func();
                    this._threwException = false;
                    this._result = result;
                }
                catch (Exception e)
                {
                    this._threwException = true;
                    this._exception = new AggregateException(e);
                }
                finally
                {
                    Volatile.Write(ref this._isCompleted, true);
                    this._func = null;
                }
            }
        }
    

        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate)
        {
            lock (this.NextTasks)
            {
                if (Volatile.Read(ref this._isCompleted))
                {
                    return this._threadPool.Submit(() => nextDelegate(this._result!));
                }

                var nextTask = new MyTask<TNewResult>(() => nextDelegate(this._result!), this._threadPool);
                this.NextTasks.Add(nextTask.Execute);
            
                return nextTask;
            }
        }
    }
}