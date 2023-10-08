namespace Lazy;

/// <summary>
/// Single threaded implementation of iLazy interface
/// </summary>
/// <typeparam name="T">Result type</typeparam>
public class SingleThreadedLazy<T> : ILazy<T>
{
    /// <summary>
    /// Creates single threaded lazy with given delegate
    /// </summary>
    /// <param name="supplier">Delegate to use</param>
    public SingleThreadedLazy(Func<T> supplier) => this._supplier = supplier;

    /// <summary>
    /// Lazily calculates given function
    /// </summary>
    /// <returns>Function result</returns>
    /// <exception cref="Exception">If delegate threw exception</exception>
    public T Get()
    {
        if (!this._isCalculated)
        {
            this._isCalculated = true;
            try
            {
                this._result = this._supplier();
            }
            catch (Exception e)
            {
                this._threwException = true;
                this._resultException = e;
            }
        }

        if (_threwException)
        {
            throw this._resultException;
        }
        return this._result;
    }

    /// <summary>
    /// Function result holder
    /// </summary>
    private T? _result;
    
    /// <summary>
    /// Function exception holder
    /// </summary>
    private Exception _resultException = new();
    
    /// <summary>
    /// Was function calculated
    /// </summary>
    private bool _isCalculated;
    
    /// <summary>
    /// If the function threw exception
    /// </summary>
    private bool _threwException;
    
    /// <summary>
    /// Delegate defining the lazy
    /// </summary>
    private readonly Func<T> _supplier;
}