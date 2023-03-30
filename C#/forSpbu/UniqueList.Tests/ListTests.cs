namespace UniqueList.Tests;

public static class IntListTests
{
    private static IEnumerable<TestCaseData> ListImplementations()
    {
        yield return new TestCaseData(new List<int>());
        yield return new TestCaseData(new UniqueList<int>());
    }

    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void AddValueGetValueShouldReturnTheSameValue(List<int> list)
    {
        list.Add(1, 0);
        Assert.That(list.GetValue(0), Is.EqualTo(1));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void AddValueNotInTheBeginningShouldThrowArgumentOutOfRangeException(List<int> list)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => list.Add(1, 4));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void Add2ValuesGetLastValueShouldReturnTheSecondValue(List<int> list)
    {
        list.Add(1, 0);
        list.Add(2, 1);
        Assert.That(list.GetValue(1), Is.EqualTo(2));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void AddValueChangeValueGetValueShouldReturnNewValue(List<int> list)
    {
        list.Add(1, 0);
        list.ChangeValue(2, 0);
        Assert.That(list.GetValue(0), Is.EqualTo(2));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void GetValueShouldReturnThrowValueDoesntExistException(List<int> list)
    {
        Assert.Throws<ValueDoesntExistException>(() => list.GetValue(0));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void ChangeValueShouldReturnThrowValueDoesntExistException(List<int> list)
    {
        Assert.Throws<ValueDoesntExistException>(() => list.ChangeValue(2, 0));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void ChangeValueWithNegativePositionShouldReturnThrowArgumentOutOfRange(List<int> list)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => list.ChangeValue(1, -1));
    }

    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void GetValueWithNegativePositionShouldReturnThrowArgumentOutOfRange(List<int> list)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => list.GetValue(-1));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void RemoveFromEmptyListShouldThrowValueDoesntExistException(List<int> list)
    {
        Assert.Throws<ValueDoesntExistException>(() => list.Remove(1));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void RemoveNegativeIndexShouldThrowArgumentOutOfRangeException(List<int> list)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => list.Remove(-1));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void Add2ValuesRemoveLastGetFirstShouldReturnFirst(List<int> list)
    {
        list.Add(1, 0);
        list.Add(2, 1);
        list.Remove(1);
        Assert.That(list.GetValue(0), Is.EqualTo(1));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void Add2ValuesRemoveFirstGetFirstShouldReturnSecond(List<int> list)
    {
        list.Add(1, 0);
        list.Add(2, 1);
        list.Remove(0);
        Assert.That(list.GetValue(0), Is.EqualTo(2));
    }
    
    [Test, TestCaseSource(nameof(ListImplementations))]
    public static void Add2SameValuesShouldWorkFineIfNotUniqueThrowValueAlreadyExistExceptionIfUnique(List<int> list)
    {
        list.Add(1, 0);
        if (list is UniqueList<int>)
        {
            Assert.Throws<ValueAlreadyExistException>(() => list.Add(1, 1));
        }
        else
        {
            list.Add(1, 1);
            Assert.That(list.GetValue(1), Is.EqualTo(1));
        }
    }
}