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
    public OkResult(string? @class, string name) : base(@class, name)
    {
    }

    public override string ToString()
    {
        return $"{this.Class}.{this.Method} succeeded";
    }
}