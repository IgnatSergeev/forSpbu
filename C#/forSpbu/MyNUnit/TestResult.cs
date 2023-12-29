using System.Reflection;

namespace MyNUnit;

/// <summary>
/// Represents test result
/// </summary>
public class TestResult
{
    private readonly RunResult _expected;
    private readonly RunResult _real;
    private readonly string? _ignoreDescription;

    /// <summary>
    /// Creates test result of real and expected
    /// </summary>
    /// <param name="real">Real run result</param>
    /// <param name="expected">Expected run result</param>
    /// <param name="ignoreDesc">Description of ignore or null</param>
    public TestResult(RunResult real, RunResult expected, string? ignoreDesc)
    {
        if (real.GetMethod() != expected.GetMethod() || real.GetClass() != expected.GetClass())
        {
            throw new TestResultCreationException();
        }
        this._expected = expected;
        this._real = real;
        this._ignoreDescription = ignoreDesc;
    }
    
    public override string ToString() =>
        this.Ignored 
            ? $"Test ignored: {this._ignoreDescription}"
            : RunResult.Equals(this._real,this._expected)
                ? $"Test passed: {this._real.ToString()}"
                : $"Test failed: expected {this._expected.ToString()}; was {this._real.ToString()}";

    /// <summary>
    /// Is test ignored
    /// </summary>
    public bool Ignored => this._ignoreDescription != null;
    
    /// <summary>
    /// Is test passed
    /// </summary>
    public bool Passed => RunResult.Equals(this._real,this._expected);
    
    /// <summary>
    /// Is expected result was exception
    /// </summary>
    public bool ExpectedException  => this._expected is ExceptionResult;
    
    /// <summary>
    /// Is expected result was ok
    /// </summary>
    public bool ExpectedOk  => this._expected is OkResult;
    
    /// <summary>
    /// Return test method name
    /// </summary>
    public MethodInfo GetMethod()  => this._expected.GetMethod();
    
    /// <summary>
    /// Return test class name
    /// </summary>
    public Type? GetClass()  => this._expected.GetClass();
}