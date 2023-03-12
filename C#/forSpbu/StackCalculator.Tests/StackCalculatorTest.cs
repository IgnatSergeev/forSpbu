namespace StackCalculator.Tests;

public class StackCalculatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MaxDigitsSum()
    {
        Assert.That(StackCalculator.Evaluate("9 9 +", new ListStack<float>()), Is.InRange(17.9, 18.1));
    }

    [Test] public void MaxDigitsMul()
    {
        Assert.That(StackCalculator.Evaluate("9 9 *", new ListStack<float>()), Is.InRange(80.9, 81.1));
    }
    
    [Test] public void MulOfPositiveAndNegativeFirstDigitIsNegativeShouldReturnNegative()
    {
        Assert.That(StackCalculator.Evaluate("-9 9 *", new ListStack<float>()), Is.InRange(-81.1, -80.9));
    }
    
    [Test] public void MulOfPositiveAndNegativeSecondDigitIsNegativeShouldReturnNegative()
    {
        Assert.That(StackCalculator.Evaluate("9 -9 *", new ListStack<float>()), Is.InRange(-81.1, -80.9));
    }
    
    [Test] public void MulOfNegativeAndNegativeShouldReturnPositive()
    {
        Assert.That(StackCalculator.Evaluate("-99,9 -999,9 *", new ListStack<float>()), Is.InRange(99890, 99890.02));
    }
    
    [Test] public void SumOfNegativeAndPositiveShouldReturnZero()
    {
        Assert.That(StackCalculator.Evaluate("-10 10 +", new ListStack<float>()), Is.InRange(-0.1, 0.1));
    }

    [Test] public void MulByZeroZeroIsSecondShouldReturnZero()
    {
        Assert.That(StackCalculator.Evaluate("-10 0 *", new ListStack<float>()), Is.InRange(-0.01, 0.01));
    }
    
    [Test] public void MulByZeroZeroIsFirstShouldReturnZero()
    {
        Assert.That(StackCalculator.Evaluate("0 999,9 *", new ListStack<float>()), Is.InRange(-0.01, 0.01));
    }
    
    [Test] public void SumByZeroZeroIsSecondShouldReturnTheFirstValue()
    {
        Assert.That(StackCalculator.Evaluate("-10 0 +", new ListStack<float>()), Is.InRange(-10.01, -9.99));
    }
    
    [Test] public void SumByZeroZeroIsFirstShouldReturnTheFirstValue()
    {
        Assert.That(StackCalculator.Evaluate("0 -10 +", new ListStack<float>()), Is.InRange(-10.01, -9.99));
    }
    
    [Test] public void NullStringShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate(null, new ListStack<float>()), "Cannot evaluate null string");
    }
    
    [Test] public void NullStackShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("null", null), "Cannot evaluate using null stack");
    }
    
    [Test] public void WrongOrderShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("1 - 2", new ListStack<float>()), "Trying to pop from empty stack");
    }
    
    [Test] public void WrongSymbolsShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("/ 2 -", new ListStack<float>()), "Error in parsing number, wrong input");
    }
    
    [Test] public void WrongSymbols2ShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("2 2 %", new ListStack<float>()), "Error in parsing number, wrong input");
    }
    
    [Test] public void TwoSignsInSequence()
    {
        Assert.That(StackCalculator.Evaluate("2 0 * 1 2 + -", new ListStack<float>()), Is.InRange(-3.01, -2.99));
    }
    
    [Test] public void EmptyStringShouldThrowException()
    {
        Assert.Throws<Exception>(() => StackCalculator.Evaluate("", new ListStack<float>()), "Error in parsing number, wrong input");
    }
    
    [Test] public void NotEmptyStackShouldWorkFine()
    {
        var stack = new ListStack<float>();
        stack.Push(22);
        
        Assert.That(StackCalculator.Evaluate("0 -10 +", stack), Is.InRange(-10.01, -9.99));
    }
}