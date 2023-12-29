using System.Reflection;

namespace MyNUnit;

/// <summary>
/// Successful run result implementation
/// </summary>
public class OkResult : RunResult
{
    /// <summary>
    /// Creates ok result
    /// </summary>
    /// <param name="class">Class name(in which method defined in)</param>
    /// <param name="name">Method name</param>
    public OkResult(Type? @class, MethodInfo name) : base(@class, name)
    {
    }

    public override string ToString()
    {
        return $"{this.Class} {this.Method} succeeded";
    }
    
    /// <summary>
    /// Compares two ok run results
    /// </summary>
    /// <param name="fst">First ok run result</param>
    /// <param name="sec">Second ok run result</param>
    /// <returns>True they are equal</returns>
    public static bool Equals(OkResult fst, OkResult sec)
    {
       
        return fst.Class == sec.Class && fst.Method == sec.Method;
    }
}