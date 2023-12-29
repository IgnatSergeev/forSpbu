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
    
    /// <summary>
    /// Compares two exception run results
    /// </summary>
    /// <param name="fst">First exception run result</param>
    /// <param name="sec">Second exception run result</param>
    /// <returns>True they are equal</returns>
    public static bool Equals(ExceptionResult fst, ExceptionResult sec)
    {
        return fst.Class == sec.Class && fst.Method == sec.Method && fst._exception == sec._exception;
    }
}