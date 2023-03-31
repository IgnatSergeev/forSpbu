namespace ParseTree.Tests;

public static class ParseTreeTests
{
    [Test]
    public static void NullExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new ParseTree(null);
        });
    }
    
    [Test]
    public static void EmptyExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new ParseTree("");
        });
    }

    [Test]
    public static void IncorrectOrderExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new ParseTree("1 + 2");
        });
    }
    
    [Test]
    public static void IncorrectOperandExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new ParseTree("+ * 2");
        });
    }
    
    [Test]
    public static void IncorrectOperatorExpressionShouldThrowParseErrorException()
    {
        Assert.Throws<ParseErrorException>(() =>
        {
            var _ = new ParseTree("% 1 2");
        });
    }
    
    [Test]
    public static void EvaluateSimpleExpressionShouldReturn3()
    {
        Assert.That(new ParseTree("+ 1 2").Evaluate(), Is.InRange(2.999, 3.001));
    }
    
    [Test]
    public static void EvaluateComplicatedExpressionShouldReturn3()
    {
        Assert.That(new ParseTree("+ * 1 2 1").Evaluate(), Is.InRange(2.999, 3.001));
    }
    
    [Test]
    public static void EvaluateAnotherComplicatedExpressionShouldReturn6()
    {
        Assert.That(new ParseTree("* + 1 2 2").Evaluate(), Is.InRange(5.999, 6.001));
    }
    
    [Test]
    public static void Evaluate5LevelExpressionShouldReturn6()
    {
        Assert.That(new ParseTree("* (- (+ (* (+ 1 2) 2) 10) 10) -1").Evaluate(), Is.InRange(-6.001, -5.999));
    }
    
    [Test]
    public static void DivideByZeroExpressionShouldThrowDivideByZeroExpression()
    {
        Assert.Throws<DivideByZeroException>(() =>
        {
            var _ = new ParseTree("/ 1 - 1 1").Evaluate();
        });
    }
}