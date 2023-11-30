namespace MyNUnit;

/// <summary>
/// Successful run result implementation
/// </summary>
public class OkResult : RunResult
{
    /// <summary>
    /// Creates ok result
    /// </summary>
    /// <param name="name">Method name</param>
    public OkResult(string name) : base(name) {}
}