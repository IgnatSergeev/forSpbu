namespace ParseTree.Tests;

public static class OperatorTests
{
    [Test]
    public static void NullExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator(null, 1, 2);
        });
    }
    
    [Test]
    public static void EmptyExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator(Array.Empty<string>(), 1, 2);
        });
    }
    
    [Test]
    public static void NegativeIndexShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("+ 1 2".Split().ToArray(), -1, 0);
        });
    }
    
    [Test]
    public static void FirstIndexIsGreaterThanOtherShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("+ 1 2".Split().ToArray(), 1, 0);
        });
    }
    
    [Test]
    public static void IncorrectIndexShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("+ 1 2".Split().ToArray(), 1, 4);
        });
    }
    
    [Test]
    public static void IncorrectOrderExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("1 + 2".Split().ToArray(), 0, 3);
        });
    }
    
    [Test]
    public static void IncorrectOperandExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("+ a 2".Split().ToArray(), 0, 3);
        });
    }
    
    [Test]
    public static void IncorrectOperatorExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operator("% 1 2".Split().ToArray(), 0, 3);
        });
    }
    
    [Test]
    public static void EvaluateSimpleOperatorExpressionShouldReturn3()
    {
        Assert.That(new Operator("+ 1 2".Split().ToArray(), 0, 3).Evaluate(), Is.InRange(2.999, 3.001));
    }
    
    [Test]
    public static void EvaluateOperatorInsideComplicatedExpressionShouldReturn3()
    {
        Assert.That(new Operator("* + 1 2 + 1 1".Split().ToArray(), 1, 4).Evaluate(), Is.InRange(2.999, 3.001));
    }
    
    [Test]
    public static void EvaluateOperatorWithOperatorAndOperandChildrenExpressionShouldReturn6()
    {
        Assert.That(new Operator("* + 1 2 2".Split().ToArray(), 0, 5).Evaluate(), Is.InRange(5.999, 6.001));
    }
    
    [Test]
    public static void EvaluateOperatorWith2OperatorChildrenExpressionShouldReturn6()
    {
        Assert.That(new Operator("* + 1 2 + 1 1".Split().ToArray(), 0, 7).Evaluate(), Is.InRange(5.999, 6.001));
    }
}