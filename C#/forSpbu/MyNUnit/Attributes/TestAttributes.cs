namespace MyNUnit.Attributes;

/// <summary>
/// Test method attribute
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class Test : Attribute
{
    /// <summary>
    /// Is exception should be thown
    /// </summary>
    public Type? Expected { get; set; }
    
    /// <summary>
    /// Is test result ignored
    /// </summary>
    public string? Ignore { get; set; }
}
