namespace StackCalculator.Tests;

public class FloatStackTest
{
    private static IEnumerable<TestCaseData> StackImplementations()
    {
        var listStack = new ListStack<float>();
        var arrayStack = new ArrayStack<float>();
        yield return new TestCaseData(listStack);
        yield return new TestCaseData(arrayStack);
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void PopFromEmptyStackShouldThrowException(IStack<float> stack)
    {
        Assert.Throws<ArgumentNullException>(stack.Pop);
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public void PushAndTopShouldReturnTheSameValue(IStack<float> stack)
    {
        stack.Push(239);
        
        Assert.That(stack.Top(), Is.InRange(238.99, 239.01));
    }

    [Test, TestCaseSource(nameof(StackImplementations))]
    public void PushAndPushAndPopAndTopShouldReturnTheFirstValue(IStack<float> stack)
    {
        stack.Push(239);
        stack.Push(238);
        stack.Pop();
        
        Assert.That(stack.Top(), Is.InRange(238.99, 239.01));
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void PushAndPushAndTopShouldReturnTheSecondValue(IStack<float> stack)
    {
        stack.Push(239);
        stack.Push(238);

        Assert.That(stack.Top(), Is.InRange(237.99, 238.01));
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void TopFromEmptyStackShouldThrowException(IStack<float> stack)
    {
        Assert.Throws<ArgumentNullException>(stack.Pop, "Trying to top from empty stack");
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void AddElementIsEmptyShouldReturnFalse(IStack<float> stack)
    {
        stack.Push(1);
        Assert.That(!stack.IsEmpty());
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void AddElementRemoveElementIsEmptyShouldReturnTrue(IStack<float> stack)
    {
        stack.Push(1);
        stack.Pop();
        Assert.That(stack.IsEmpty());
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void IsEmptyShouldReturnTrue(IStack<float> stack)
    {
        Assert.That(stack.IsEmpty());
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void AddElementClearIsEmptyShouldReturnTrue(IStack<float> stack)
    {
        stack.Push(1);
        stack.Clear();
        Assert.That(stack.IsEmpty());
    }
    
    [Test, TestCaseSource(nameof(StackImplementations))]
    public void ClearEmptyStackIsEmptyShouldReturnTrue(IStack<float> stack)
    {
        stack.Clear();
        Assert.That(stack.IsEmpty());
    }
}