namespace MyNUnit;

[AttributeUsage(AttributeTargets.Method)]
public class Test : Attribute
{
    public Exception? Expected { get; set; }
    
    public string? Ignore { get; set; }
}

[AttributeUsage(AttributeTargets.Method)]
public class Before : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class After : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class BeforeClass : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class AfterClass : Attribute
{
}