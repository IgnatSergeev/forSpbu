namespace MyThreadPool;

internal class MyTask<TResult> : IMyTask<TResult>
{
    public MyTask(Func<TResult> func, MyThreadPool threadPool)
    {
        this._func = func;
        this._threadPool = threadPool;
    }
    
    public TResult Result()
    {
        this.Execute();

        if (this._threwException)
        {
            throw this._exception;
        }

        return this._result!;
    }

    public void Execute()
    {
        if (this.IsCompleted) return;
        lock (this._func!)
        {
            if (this.IsCompleted) return;
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
                this.IsCompleted = true;
                this._func = null;
            }
        }
    }
    

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate)
    {
        lock (this.NextTasks)
        {
            if (this.IsCompleted)
            {
                return _threadPool.Submit(() => nextDelegate(this._result!));
            }
            
            var nextTask = new MyTask<TNewResult>(() => nextDelegate(this._result!), _threadPool);
            this.NextTasks.Add(() =>
            {
                nextTask.Execute();
            });
            
            return nextTask;
        }
    }
    
    private Func<TResult>? _func;
    private readonly MyThreadPool _threadPool;
    
    private volatile Exception _exception = new AggregateException();
    private TResult? _result;
    private volatile bool _threwException;
    public bool IsCompleted { get; private set; }
    public readonly List<Action> NextTasks = new ();
}