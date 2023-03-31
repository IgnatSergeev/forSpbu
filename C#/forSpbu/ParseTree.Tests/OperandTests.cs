namespace ParseTree.Tests;

public static class OperandTests
{
    [Test]
    public static void NullExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operand(null, 1);
        });
    }
    
    [Test]
    public static void EmptyExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operand(Array.Empty<string>(), 1);
        });
    }
    
    [Test]
    public static void NegativeIndexShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operand("1".Split().ToArray(), -1);
        });
    }
    
    [Test]
    public static void IncorrectIndexShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operand("1".Split().ToArray(), 1);
        });
    }
    
    [Test]
    public static void IncorrectExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new Operand("a".Split().ToArray(), 0);
        });
    }
    
    [Test]
    public static void EvaluateOperandInSimpleExpressionShouldReturnCorrectValue()
    {
        Assert.That(() =>
            new Operand("1".Split().ToArray(), 0).Evaluate(), Is.EqualTo(1));
    }
    
    [Test]
    public static void EvaluateOperandInComplicatedExpressionShouldReturnCorrectValue()
    {
        Assert.That(() =>
            new Operand("+ 2 1".Split().ToArray(), 1).Evaluate(), Is.EqualTo(2));
    }
}