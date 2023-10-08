namespace MyThreadPool;

public interface IMyTask<out TResult>
{
    public bool IsCompleted { get; }

    public TResult Result();

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate);
}