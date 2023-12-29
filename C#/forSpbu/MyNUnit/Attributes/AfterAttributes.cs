namespace MyNUnit.Attributes;

/// <summary>
/// Attribute on method, which should be ran after each test
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class After : Attribute
{
}
