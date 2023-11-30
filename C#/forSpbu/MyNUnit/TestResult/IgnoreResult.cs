namespace MyNUnit;

/// <summary>
/// Ignored run result implementation
/// </summary>
public class IgnoreResult : RunResult
{
    private string _description;

    /// <summary>
    /// Creates ignored result
    /// </summary>
    /// <param name="name">Method name</param>
    /// <param name="description">Ignore description</param>
    public IgnoreResult(string name, string description) : base(name)
    {
        _description = description;
    }
}