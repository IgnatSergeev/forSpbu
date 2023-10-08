﻿namespace Lazy;

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
        _mutex.WaitOne();
        if (!_isCalculated)
        {
            _mutex.Close();
            _isCalculated = true;
            try
            {
                _result = _supplier();
            }
            catch (Exception e)
            {
                _threwException = true;
                _resultException = e;
            }
            _mutex.ReleaseMutex();
        }

        if (_threwException)
        {
            throw _resultException;
        }
        return _result;
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
    private bool _isCalculated;
    
    /// <summary>
    /// If the function threw exception
    /// </summary>
    private bool _threwException;
    
    /// <summary>
    /// Delegate defining the lazy
    /// </summary>
    private readonly Func<T> _supplier;

    /// <summary>
    /// Mutex for holding threads while delegate is calculated
    /// </summary>
    private readonly Mutex _mutex = new Mutex();
}