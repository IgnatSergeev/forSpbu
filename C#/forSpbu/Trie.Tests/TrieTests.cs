namespace Trie.Tests;

public class TrieTests
{
    [SetUp]
    public void Setup()
    {
    }

    private static IEnumerable<TestCaseData> TrieRealisations()
    {
        var trie = new TrieRealisation();
        yield return new TestCaseData(trie);
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Test1()
    {
        Assert.Pass();
    }
}