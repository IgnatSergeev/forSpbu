namespace MyNUnit;

/// <summary>
/// Represents test result
/// </summary>
public class TestResult
{
    private readonly RunResult _expected;
    private readonly RunResult _real;
    private readonly string? _ignoreDesc;

    /// <summary>
    /// Creates test result of real and expected
    /// </summary>
    /// <param name="real">Real run result</param>
    /// <param name="expected">Expected run result</param>
    /// <param name="ignoreDesc">Description of ignore or null</param>
    public TestResult(RunResult real, RunResult expected, string? ignoreDesc)
    {
        this._expected = expected;
        this._real = real;
        this._ignoreDesc = ignoreDesc;
    }
    
    public override string ToString() =>
        this.Ignored 
            ? $"Test ignored: {this._ignoreDesc}"
            : this._real == this._expected
                ? $"Test passed: {this._real.ToString()}"
                : $"Test failed: expected {this._expected.ToString()}; was {this._real.ToString()}";

    /// <summary>
    /// Is test ignored
    /// </summary>
    public bool Ignored => this._ignoreDesc != null;
    
    /// <summary>
    /// Is test passed
    /// </summary>
    public bool Passed => this._real == this._expected;
}