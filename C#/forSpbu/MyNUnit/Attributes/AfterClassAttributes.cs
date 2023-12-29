namespace MyNUnit.Attributes;

/// <summary>
/// Attribute on static method, which should be ran after test class creation
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AfterClass : Attribute
{
}