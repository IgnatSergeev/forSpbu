namespace StackCalculator.Tests;

public class StackCalculatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    private static IEnumerable<TestCaseData> StackRealisations()
    {
        var listStack = new ListStack<float>();
        var arrayStack = new ArrayStack<float>();
        yield return new TestCaseData(listStack);
        yield return new TestCaseData(arrayStack);
    }

    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MaxDigitsSum(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("9 9 +", stack), Is.InRange(17.9, 18.1));
    }

    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MaxDigitsMul(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("9 9 *", stack), Is.InRange(80.9, 81.1));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MulOfPositiveAndNegativeFirstDigitIsNegativeShouldReturnNegative(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("-9 9 *", stack), Is.InRange(-81.1, -80.9));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MulOfPositiveAndNegativeSecondDigitIsNegativeShouldReturnNegative(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("9 -9 *", stack), Is.InRange(-81.1, -80.9));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MulOfNegativeAndNegativeShouldReturnPositive(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("-99,9 -999,9 *", stack), Is.InRange(99890, 99890.02));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void SumOfNegativeAndPositiveShouldReturnZero(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("-10 10 +", stack), Is.InRange(-0.1, 0.1));
    }

    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MulByZeroZeroIsSecondShouldReturnZero(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("-10 0 *", stack), Is.InRange(-0.01, 0.01));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void MulByZeroZeroIsFirstShouldReturnZero(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("0 999,9 *", stack), Is.InRange(-0.01, 0.01));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void SumByZeroZeroIsSecondShouldReturnTheFirstValue(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("-10 0 +", stack), Is.InRange(-10.01, -9.99));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void SumByZeroZeroIsFirstShouldReturnTheFirstValue(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("0 -10 +", stack), Is.InRange(-10.01, -9.99));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void NullStringShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate(null, stack), "Cannot evaluate null string");
    }
    
    [Test]
    public void NullStackShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("null", null), "Cannot evaluate using null stack");
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void WrongOrderShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("1 - 2", stack), "Trying to pop from empty stack");
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void WrongSymbolsShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("/ 2 -", stack), "Error in parsing number, wrong input");
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void WrongSymbols2ShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("2 2 %", stack), "Error in parsing number, wrong input");
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void TwoSignsInSequence(Stack<float> stack)
    {
        Assert.That(StackCalculator.Evaluate("2 0 * 1 2 + -", stack), Is.InRange(-3.01, -2.99));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void EmptyStringShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("", stack), "Error in parsing number, wrong input");
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void NotEmptyStackShouldWorkFine(Stack<float> stack)
    {
        stack.Push(22);
        
        Assert.That(StackCalculator.Evaluate("0 -10 +", stack), Is.InRange(-10.01, -9.99));
    }
}