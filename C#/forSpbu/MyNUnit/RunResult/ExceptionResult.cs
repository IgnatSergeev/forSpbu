namespace MyNUnit;

/// <summary>
/// Interrupted run result implementation
/// </summary>
public class ExceptionResult : RunResult
{
    private readonly Exception _exception;

    /// <summary>
    /// Creates exception result
    /// </summary>
    /// <param name="class">Class name(in which method defined in)</param>
    /// <param name="name">Method name</param>
    /// <param name="exception">Exception which interrupted method execution</param>
    public ExceptionResult(string? @class, string name, Exception exception) : base(@class, name)
    {
        this._exception = exception;
    }
    
    public override string ToString()
    {
        return $"{this.Class}.{this.Method} threw exception {this._exception}";
    }
}