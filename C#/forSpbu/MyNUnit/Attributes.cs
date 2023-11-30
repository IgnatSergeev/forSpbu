namespace MyNUnit;

/// <summary>
/// Test method attribute
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class Test : Attribute
{
    /// <summary>
    /// Is exception should be thown
    /// </summary>
    public Exception? Expected { get; set; }
    
    /// <summary>
    /// Is test result ignored
    /// </summary>
    public string? Ignore { get; set; }
}

/// <summary>
/// Attribute on method, which should be ran before each test
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class Before : Attribute
{
}

/// <summary>
/// Attribute on method, which should be ran after each test
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class After : Attribute
{
}

/// <summary>
/// Attribute on static method, which should be ran before test class creation
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class BeforeClass : Attribute
{
}

/// <summary>
/// Attribute on static method, which should be ran after test class creation
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AfterClass : Attribute
{
}