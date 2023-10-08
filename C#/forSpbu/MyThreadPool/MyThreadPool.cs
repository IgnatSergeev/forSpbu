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
       this._semaphore = new Semaphore(0, size);
    }

    public IMyTask<TResult> Submit<TResult>(Func<TResult> func)
    {
        _semaphore.WaitOne();
        
        
    }
    
    public int Size => this._threads.Length;

    private Semaphore _semaphore;
    private I[] _mutexes;
    private Thread[] _threads;
}