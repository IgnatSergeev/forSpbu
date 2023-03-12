namespace StackCalculator.Tests;

public class FloatStackTest
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
    public void PopFromEmptyStackShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(stack.Pop, "Trying to pop from empty stack");
    }

    [Test, TestCaseSource(nameof(StackRealisations))]
    public void PushAndTopShouldReturnTheSameValue(Stack<float> stack)
    {
        stack.Push(239);
        
        Assert.That(stack.Top(), Is.InRange(238.99, 239.01));
    }

    [Test, TestCaseSource(nameof(StackRealisations))]
    public void PushAndPushAndPopAndTopShouldReturnTheFirstValue(Stack<float> stack)
    {
        stack.Push(239);
        stack.Push(238);
        stack.Pop();
        
        Assert.That(stack.Top(), Is.InRange(238.99, 239.01));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void PushAndPushAndTopShouldReturnTheSecondValue(Stack<float> stack)
    {
        stack.Push(239);
        stack.Push(238);

        Assert.That(stack.Top(), Is.InRange(237.99, 238.01));
    }
    
    [Test, TestCaseSource(nameof(StackRealisations))]
    public void TopFromEmptyStackShouldThrowException(Stack<float> stack)
    {
        Assert.Throws<Exception>(stack.Pop, "Trying to top from empty stack");
    }
}