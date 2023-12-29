namespace MyNUnit.Attributes;

/// <summary>
/// Attribute on method, which should be ran before each test
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class Before : Attribute
{
}
