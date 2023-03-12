namespace StackCalculator.Tests;

public class StackTest
{
    [SetUp]
    public void Setup()
    {
    }
    
    private static IEnumerable<TestCaseData> AddRealisations()
    {
        var listStack = new ListStack();
        var arrayStack = new Stack<int>();
        yield return new TestCaseData(listStack);
        yield return new TestCaseData(arrayStack);
    }
    
    [Test, TestCaseSource(nameof(AddRealisations))]
    public void PopFromEmptyStackShouldThrowException(Stack stack)
    {
        Assert.Throws<Exception>(stack.Pop);
    }

    [Test, TestCaseSource(nameof(AddRealisations))]
    public void PushAndTopShouldReturnTheSameValue(Stack stack)
    {
        stack.Push(239);
        
        Assert.That(stack.Top(), Is.EqualTo(239));
    }

    [Test, TestCaseSource(nameof(AddRealisations))]
    public void PushAndPushAndPopAndTopShouldReturnTheFirstValue(Stack stack)
    {
        stack.Push(239);
        stack.Push(238);
        stack.Pop();
        
        Assert.That(stack.Top(), Is.EqualTo(239));
    }
    
    [Test, TestCaseSource(nameof(AddRealisations))]
    public void PushAndPushAndTopShouldReturnTheFirstValue(Stack stack)
    {
        stack.Push(239);
        stack.Push(238);

        Assert.That(stack.Top(), Is.EqualTo(238));
    }
}