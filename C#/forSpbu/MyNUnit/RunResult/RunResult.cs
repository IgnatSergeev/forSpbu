namespace MyNUnit;

/// <summary>
/// Represents method invoke result
/// </summary>
public abstract class RunResult
{
    protected readonly string Method;
    protected readonly string? Class;

    protected RunResult(string? @class, string method)
    {
        this.Class = @class;
        this.Method = method;
    }

    /// <summary>
    /// Creates specific run result
    /// </summary>
    /// <param name="class">Class name(in which method defined in)</param>
    /// <param name="name">Method name</param>
    /// <param name="exception">Exception if it was thrown</param>
    /// <param name="ignoreDesc">Ignore description if it`s ignored</param>
    /// <returns>RunResult implementation</returns>
    public static RunResult Create(string? @class, string name, Exception? exception) => 
        exception == null 
            ? new OkResult(@class, name) 
            : new ExceptionResult(@class, name, exception);

    public abstract string ToString();
}