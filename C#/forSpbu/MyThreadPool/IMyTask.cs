namespace MyThreadPool;

/// <summary>
/// Interface for my thread pool tasks
/// </summary>
/// <typeparam name="TResult">Task return value</typeparam>
public interface IMyTask<out TResult>
{
    /// <summary>
    /// Whether task is completed
    /// </summary>
    public bool IsCompleted { get; }

    /// <summary>
    /// Returns task result, blocks called thread
    /// </summary>
    /// <returns>task result</returns>
    public TResult Result();

    /// <summary>
    /// Continues task with given function, current task result is used as a parameter for the new one
    /// </summary>
    /// <param name="nextDelegate">New task delegate</param>
    /// <typeparam name="TNewResult">New task return type</typeparam>
    /// <returns>Returns created task</returns>
    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> nextDelegate);
}