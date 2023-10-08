namespace Lazy;

/// <summary>
/// Multi threaded implementation of iLazy interface
/// </summary>
/// <typeparam name="T">Result type</typeparam>
public class MultiThreadedLazy<T> : ILazy<T>
{
    /// <summary>
    /// Creates multi threaded lazy with given delegate
    /// </summary>
    /// <param name="supplier">Delegate to use</param>
    public MultiThreadedLazy(Func<T> supplier) => this._supplier = supplier;

    /// <summary>
    /// Calculates given function
    /// </summary>
    /// <returns>Function result</returns>
    /// <exception cref="Exception">If delegate threw exception</exception>
    public T? Get()
    {
        if (!this._isCalculated)
        {
            this.CalculateMonitor();
        }

        if (this._threwException)
        {
            throw this._resultException;
        }
        return this._result;
    }

    private void CalculateMonitor()
    {
        lock (this._supplier)
        {
            if (this._isCalculated) return;
            try
            {
                this._result = this._supplier();
            }
            catch (Exception e)
            {
                this._threwException = true;
                this._resultException = e;
            }
            finally
            {
                this._isCalculated = true;
            }
        }
        
    }

    /// <summary>
    /// Function result holder
    /// </summary>
    private T? _result;
    
    /// <summary>
    /// Function exception holder
    /// </summary>
    private Exception? _resultException;
    
    /// <summary>
    /// Was function calculated
    /// </summary>
    private volatile bool _isCalculated;
    
    /// <summary>
    /// If the function threw exception
    /// </summary>
    private volatile bool _threwException;
    
    /// <summary>
    /// Delegate defining the lazy
    /// </summary>
    private readonly Func<T> _supplier;
}