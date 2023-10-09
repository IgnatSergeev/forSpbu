namespace MyThreadPool;

public class MyTask<TResult> : IMyTask<TResult>
{
    public TResult Result()
    {
        //while (!this.IsCompleted) {} should be blocked with lock

        if (this._threwException)
        {
            throw this._exception;
        }

        return this._result;
    }

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate)
    {
        var nextTask = new MyTask<TNewResult>();

        void NextAction()
        {
            try
            {
                var nextResult = nextDelegate(this._result);
                nextTask.FuncFinished(nextResult);
            }
            catch (Exception e)
            {
                nextTask.FuncFinished(e);
            }
        }
        this.NextActions.Add(NextAction);
        
        return nextTask;
    }

    public void FuncFinished(TResult result)
    {
        if (this.IsCompleted) return;
        this.IsCompleted = true;
        this._threwException = false;
        this._result = result;
    }
    
    public void FuncFinished(Exception exception)
    {
        if (this.IsCompleted) return;
        this.IsCompleted = true;
        this._threwException = true;
        this._exception = new AggregateException(this._exception);
    }
    
    private volatile Exception _exception = new AggregateException();
    private TResult? _result;
    private volatile bool _threwException;
    public bool IsCompleted { get; private set; }
    public readonly List<Action> NextActions = new ();
}