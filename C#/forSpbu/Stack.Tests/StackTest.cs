namespace Stack.Tests;

public class StackTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PopFromEmptyStackShouldThrowException()
    {
        var stack = new Stack<int>();
        
        Assert.Throws<Exception>(stack.Pop);
    }

    [Test]
    public void PushAndTopShouldReturnTheSameValue()
    {
        var stack = new Stack<int>();
        
        stack.Push(239);
        
        Assert.That(stack.Top(), Is.EqualTo(239));
    }

    [Test]
    public void PushAndPushAndPopAndTopShouldReturnTheFirstValue()
    {
        var stack = new Stack<int>();
        
        stack.Push(239);
        stack.Push(238);
        stack.Pop();
        
        Assert.That(stack.Top(), Is.EqualTo(239));
    }
    
    [Test]
    public void PushAndPushAndTopShouldReturnTheFirstValue()
    {
        var stack = new Stack<int>();
        
        stack.Push(239);
        stack.Push(238);

        Assert.That(stack.Top(), Is.EqualTo(238));
    }
}