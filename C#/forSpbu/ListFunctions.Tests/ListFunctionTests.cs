namespace ListFunctions.Tests;

public static class ListFunctionsTests
{
    private static IEnumerable<TestCaseData> IEnumerablesForTests()
    {
        yield return new TestCaseData(new List<int>(new[] {1, 2 , 3, 4, 5}));
        yield return new TestCaseData(new[] {1, 2 , 3, 4, 5});
    }

    [Test]
    public static void MapNullArgumentShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => ListFunctions.Map<int>(null, x => 2 * x));
    }
    
    [Test]
    public static void FilterNullArgumentShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => ListFunctions.Filter<int>(null, x => true));
    }
    
    [Test]
    public static void FoldNullArgumentShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => ListFunctions.Fold<int, int>(null, 0,(x, y) => x * y));
    }
    
    [Test, TestCaseSource(nameof(IEnumerablesForTests))]
    public static void Map5ElementTest(IEnumerable<int> enumerable)
    {
        var enumerableArray = enumerable.ToArray();
        var newList = ListFunctions.Map(enumerableArray, x => x + 2);
        var index = 0;
        foreach (var element in newList)
        {
            Assert.That(element, Is.EqualTo(enumerableArray[index++] + 2));
        }
    }
    
    [Test, TestCaseSource(nameof(IEnumerablesForTests))]
    public static void Filter5ElementTest(IEnumerable<int> enumerable)
    {
        var enumerableArray = enumerable.ToArray();
        var newList = ListFunctions.Filter(enumerableArray, x => x % 2 == 0);
        var index = 2;
        Assert.That(newList, Has.Count.EqualTo(2));
        foreach (var element in newList)
        {
            Assert.That(element, Is.EqualTo(index));
            index += 2;
        }
    }
    
    [Test, TestCaseSource(nameof(IEnumerablesForTests))]
    public static void Fold5ElementTest(IEnumerable<int> enumerable)
    {
        var enumerableArray = enumerable.ToArray();
        var foldedValue = ListFunctions.Fold(enumerableArray, 1,(previousFoldedValue, x) => previousFoldedValue * x);
        Assert.That(foldedValue, Is.EqualTo(120));
    }
}