using System.Reflection;

namespace MyNUnit;

/// <summary>
/// Interrupted run result implementation
/// </summary>
public class ExceptionResult : RunResult
{
    private readonly Type? _exception;

    /// <summary>
    /// Creates exception result
    /// </summary>
    /// <param name="class">Class name(in which method defined in)</param>
    /// <param name="name">Method name</param>
    /// <param name="exception">Exception which interrupted method execution</param>
    public ExceptionResult(Type? @class, MethodInfo name, Type? exception) : base(@class, name)
    {
        this._exception = exception;
    }
    
    public override string ToString()
    {
        return $"{this.Class} {this.Method} threw exception {this._exception?.FullName}";
    }
    
    public static bool operator !=(ExceptionResult fst, ExceptionResult sec)
    {
        return !(fst == sec);
    }
    
    public static bool operator ==(ExceptionResult fst, ExceptionResult sec)
    {
        return fst.Class == sec.Class && fst.Method == sec.Method && fst._exception == sec._exception;
    }
}