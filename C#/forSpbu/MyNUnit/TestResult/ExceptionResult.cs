namespace MyNUnit;

/// <summary>
/// Interrupted run result implementation
/// </summary>
public class ExceptionResult : RunResult
{
    private Exception _exception;

    /// <summary>
    /// Creates exception result
    /// </summary>
    /// <param name="name">Method name</param>
    /// <param name="exception">Exception which interrupted method execution</param>
    public ExceptionResult(string name, Exception exception) : base(name)
    {
        _exception = exception;
    }
}