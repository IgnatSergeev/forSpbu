using System.Collections.Concurrent;

namespace MyThreadPool;

/// <summary>
/// Thread pool for concurrent delegate calculation
/// </summary>
public class MyThreadPool : IDisposable
{
    private readonly ConcurrentQueue<Action> _taskActions = new ();
    private readonly Thread[] _threads;
    private readonly Semaphore _taskBlocker = new (0, int.MaxValue);
    private readonly CancellationTokenSource _cancellation = new ();
    
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
        var threadStart = new ManualResetEvent(false);
        for (int i = 0; i < size; i++)
        {
            this._threads[i] = new Thread(() =>
            {
                threadStart.WaitOne();
                while (true)
                {
                    this._taskBlocker.WaitOne();
                    if (this._taskActions.TryDequeue(out var taskAction))
                    {
                        taskAction.Invoke();
                    }

                    if (this._cancellation.IsCancellationRequested)
                    {
                        break;
                    }
                }
            });
            this._threads[i].Start();
        }

        threadStart.Set();
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
        lock (this._cancellation)
        {
            if (!this._cancellation.IsCancellationRequested)
            {
                this._taskActions.Enqueue(task.Execute);
                this._taskBlocker.Release();
            }
        }

        return task;
    }

    /// <summary>
    /// Softly shuts down the pool, blocks called thread until all running tasks finished
    /// </summary>
    public void Shutdown()
    {
        lock (this._cancellation)
        {
            this._cancellation.Cancel();
        }
        
        for (var i = 0; i < this._threads.Length; i++)
        {
            this._taskBlocker.Release();
        }
        
        foreach (var thread in this._threads)
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
        
        private TResult? _result;
        private volatile Exception _exception = new AggregateException();
        private volatile bool _threwException;
        private readonly ManualResetEvent _completeEvent = new (false);
        private readonly ConcurrentBag<Action> _nextTasks = new ();

        public bool IsCompleted { get; private set; }
        
        public MyTask(Func<TResult> func, MyThreadPool threadPool)
        {
            this._func = func;
            this._threadPool = threadPool;
        }
    
        public TResult Result => 
            this._completeEvent.WaitOne() && this._threwException ? throw this._exception : this._result!;


        public void Execute()
        {
            try
            {
                var result = this._func!();
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
                this._func = null;
                this.IsCompleted = true;
                this._completeEvent.Set();
            }
            Console.WriteLine("Completed");
            lock(this._nextTasks)
            {
                foreach (var task in this._nextTasks)
                {
                    this._threadPool._taskActions.Enqueue(task);
                    Console.WriteLine("Added in execute");
                }    
            }
        }
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate)
        {
            lock (this._nextTasks)
            {
                if (this.IsCompleted)
                {
                    return this._threadPool.Submit(() => nextDelegate(this._result!));
                    Console.WriteLine("Added after Completed");
                }

                var nextTask = new MyTask<TNewResult>(() => nextDelegate(this._result!), this._threadPool);
                lock (this._threadPool._cancellation)
                {
                    if (!this._threadPool._cancellation.IsCancellationRequested)
                    {
                        this._nextTasks.Add(nextTask.Execute);
                        Console.WriteLine("Queued");
                    }
                }
                    
                return nextTask;
            }
        }
    }
}