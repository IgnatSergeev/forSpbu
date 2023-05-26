namespace StackCalculator.Tests;

public static class StackCalculatorTest
{
    private static IEnumerable<TestCaseData> StackImplementations()
    {
        var listStack = new ListStack<double>();
        var arrayStack = new ArrayStack<double>();
        yield return new TestCaseData(listStack);
        yield return new TestCaseData(arrayStack);
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MaxDigitsSum(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("9 9 +", stack).Item1, Is.InRange(17.9, 18.1));
            Assert.That(StackCalculator.Evaluate("9 9 +", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MaxDigitsMul(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("9 9 *", stack).Item1, Is.InRange(80.9, 81.1));
            Assert.That(StackCalculator.Evaluate("9 9 *", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MulOfPositiveAndNegativeFirstDigitIsNegativeShouldReturnNegative(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("-9 9 *", stack).Item1, Is.InRange(-81.1, -80.9));
            Assert.That(StackCalculator.Evaluate("-9 9 *", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MulOfPositiveAndNegativeSecondDigitIsNegativeShouldReturnNegative(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("9 -9 *", stack).Item1, Is.InRange(-81.1, -80.9));
            Assert.That(StackCalculator.Evaluate("9 -9 *", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MulOfNegativeAndNegativeShouldReturnPositive(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            var evaluationResult = StackCalculator.Evaluate("-99,9 -99,9 *", stack);
            Assert.That(evaluationResult.Item1, Is.InRange(9980, 9980.02));
            Assert.That(evaluationResult.Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void SumOfNegativeAndPositiveShouldReturnZero(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("-10 10 +", stack).Item1, Is.InRange(-0.1, 0.1));
            Assert.That(StackCalculator.Evaluate("-10 10 +", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MulByZeroZeroIsSecondShouldReturnZero(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("-10 0 *", stack).Item1, Is.InRange(-0.01, 0.01));
            Assert.That(StackCalculator.Evaluate("-10 0 *", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void MulByZeroZeroIsFirstShouldReturnZero(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("0 999,9 *", stack).Item1, Is.InRange(-0.01, 0.01));
            Assert.That(StackCalculator.Evaluate("0 999,9 *", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void SumByZeroZeroIsSecondShouldReturnTheFirstValue(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("-10 0 +", stack).Item1, Is.InRange(-10.01, -9.99));
            Assert.That(StackCalculator.Evaluate("-10 0 +", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void SumByZeroZeroIsFirstShouldReturnTheFirstValue(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("0 -10 +", stack).Item1, Is.InRange(-10.01, -9.99));
            Assert.That(StackCalculator.Evaluate("0 -10 +", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void NullStringShouldThrowException(IStack<double> stack)
    {
        Assert.Throws<ArgumentNullException>(() => StackCalculator.Evaluate(null, stack), "inputString");
    }
    
    [Test]
    public static void NullStackShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => StackCalculator.Evaluate("null", null), "evaluationStack");
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void WrongOrderShouldReturnFalse(IStack<double> stack)
    {
        Assert.That(!StackCalculator.Evaluate("1 - 2", stack).Item2);
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void WrongOrder2ShouldReturnFalse(IStack<double> stack)
    {
        Assert.That(!StackCalculator.Evaluate("/ 2 -", stack).Item2);
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void WrongSymbolsShouldThrowParseException(IStack<double> stack)
    {
        Assert.Throws<ParseException>(() => StackCalculator.Evaluate("2 2 %", stack));
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void TwoSignsInSequence(IStack<double> stack)
    {
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("2 0 * 1 2 + -", stack).Item1, Is.InRange(-3.01, -2.99));
            Assert.That(StackCalculator.Evaluate("2 0 * 1 2 + -", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void EmptyStringShouldThrowParseException(IStack<double> stack)
    {
        Assert.Throws<ParseException>(() => StackCalculator.Evaluate("", stack));
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void NotEmptyStackShouldWorkFine(IStack<double> stack)
    {
        stack.Push(22);
        Assert.Multiple(() =>
        {
            Assert.That(StackCalculator.Evaluate("0 -10 +", stack).Item1, Is.InRange(-10.01, -9.99));
            Assert.That(StackCalculator.Evaluate("0 -10 +", stack).Item2);
        });
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public static void DivisionByZeroShouldThrowDivideByZeroException(IStack<double> stack)
    {
        Assert.Throws<DivideByZeroException>(() => StackCalculator.Evaluate("2 10 -10 + /", stack));
    }
}