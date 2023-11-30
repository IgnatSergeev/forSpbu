namespace MyNUnit;

/// <summary>
/// Represents method invoke result
/// </summary>
public abstract class RunResult
{
    private string _name;

    protected RunResult(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Creates specific run result
    /// </summary>
    /// <param name="name">Method name</param>
    /// <param name="exception">Exception if it was thrown</param>
    /// <param name="ignoreDesc">Ignore description if it`s ignored</param>
    /// <returns>RunResult implementation</returns>
    public static RunResult Create(string name, Exception? exception, string? ignoreDesc) => 
        ignoreDesc == null
        ? exception == null 
            ? new OkResult(name) 
            : new ExceptionResult(name, exception)
        : new IgnoreResult(name, ignoreDesc);
}