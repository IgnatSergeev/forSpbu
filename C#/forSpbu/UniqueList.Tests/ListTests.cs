namespace UniqueList.Tests;

public static class IntListTests
{
    private static IEnumerable<TestCaseData> ListImplementations()
    {
        yield return new TestCaseData(new List<int>());
    }

    [Test]
    public static void Test1()
    {
        Assert.Pass();
    }
}