namespace Lazy;

/// <summary>
/// Interface for lazy function calculation
/// </summary>
/// <typeparam name="T">Function return value</typeparam>
public interface ILazy<out T>
{
    /// <summary>
    /// Lazily calculates given function
    /// </summary>
    /// <returns>Function result</returns>
    /// <exception cref="Exception">If delegate threw exception</exception>
    public T Get();
}