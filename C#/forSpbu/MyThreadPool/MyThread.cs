namespace MyThreadPool;

public class MyThread
{
    public MyThread()
    {
        this._thread = new Thread(LifeCycle);
    }
    
    private void LifeCycle()
    {
        while (!_isTerminated)
        {
            
        }
    }

    /// <summary>
    /// Stops the process sharply
    /// </summary>
    public void Terminate()
    {
        this._isTerminated = true;
    }

    public void Execute<TResult>(Func<TResult> task)
    {
        
    }

    private volatile bool _isTerminated;
    private volatile bool _isKilled;
    
    private Thread _thread;
}