namespace MyNUnit.Attributes;

/// <summary>
/// Attribute on static method, which should be ran before test class creation
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class BeforeClass : Attribute
{
}
