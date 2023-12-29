using System.Reflection;

namespace MyNUnit;

/// <summary>
/// Represents method invoke result
/// </summary>
public abstract class RunResult
{
    protected readonly MethodInfo Method;
    protected readonly Type? Class;

    protected RunResult(Type? @class, MethodInfo method)
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
    /// <returns>RunResult implementation</returns>
    public static RunResult Create(Type? @class, MethodInfo name, Type? exception) => 
        exception == null 
            ? new OkResult(@class, name) 
            : new ExceptionResult(@class, name, exception);

    public abstract string ToString();

    /// <summary>
    /// Get run method
    /// </summary>
    /// <returns>Method of run result</returns>
    public MethodInfo GetMethod() => Method;
    
    /// <summary>
    /// Get run class
    /// </summary>
    /// <returns>Class of run result</returns>
    public Type? GetClass() => Class;

    /// <summary>
    /// Compares two run results
    /// </summary>
    /// <param name="fst">First run result</param>
    /// <param name="sec">Second run result</param>
    /// <returns>True they are equal</returns>
    public static bool Equals(RunResult fst, RunResult sec)
    {
        return fst switch
        {
            OkResult fstOk when sec is OkResult secOk => OkResult.Equals(fstOk, secOk),
            ExceptionResult fstExc when sec is ExceptionResult secExc => ExceptionResult.Equals(fstExc, secExc),
            _ => false
        };
    }
}