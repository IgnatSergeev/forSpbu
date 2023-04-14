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
    public void AddStringContainsShouldReturnTrue(Trie trie)
    {
        trie.Add("%string/");
        Assert.That(trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringRemoveStringContainsShouldReturnFalse(Trie trie)
    {
        trie.Add("%string/");
        trie.Remove("%string/");
        Assert.That(!trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddEmptyStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Add(""), "Cannot add empty string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Add(null), "Cannot add null string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void RemoveNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Remove(null), "Cannot remove null string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void RemoveEmptyStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Remove(""), "Cannot remove empty string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void ContainsEmptyStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Contains(""), "Cannot check of containing empty string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void ContainsNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<Exception>(() => trie.Contains(null), "Cannot check of containing null string");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringRemoveSubStringContainsStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%string/");
        trie.Remove("%str");
        Assert.That(trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSubStringRemoveSubStringContainsStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%string/");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSubStringRemoveStringContainsStringShouldReturnFalse(Trie trie)
    {
        trie.Add("%string/");
        trie.Add("%str");
        trie.Remove("%string/");
        Assert.That(!trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSubStringRemoveStringContainsSubStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%string/");
        trie.Add("%str");
        trie.Remove("%string/");
        Assert.That(trie.Contains("%str"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringRemoveSubStringShouldReturnFalse(Trie trie)
    {
        trie.Add("%string/");
        Assert.That(!trie.Remove("%str"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSubStringRemoveSubStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%string/");
        trie.Add("%str");
        Assert.That(trie.Remove("%str"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddStringAddSubStringForBothRemoveSubStringContainsSecondStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%strnd");
        trie.Add("%string/");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddStringRemoveStringContainsSecondStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%strnd");
        trie.Add("%string/");
        trie.Remove("%strnd");
        Assert.That(trie.Contains("%string/"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSameStringShouldReturnFalse(Trie trie)
    {
        trie.Add("%strnd");
        Assert.That(!trie.Add("%strnd"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringShouldReturnTrue(Trie trie)
    {
        Assert.That(trie.Add("%strnd"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddStringAddSubStringShouldReturnTrue(Trie trie)
    {
        trie.Add("%strndasdasd");
        Assert.That(trie.Add("%strnd"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add3StringsHowManyStartsWithEmptyPrefixShouldBeEqualToSize(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%str");
        Assert.That(trie.HowManyStartsWithPrefix(""), Is.EqualTo(trie.Size));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add3StringsHowManyStartsWithEmptyPrefixShouldBeEqualTo3(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%str");
        Assert.That(trie.HowManyStartsWithPrefix(""), Is.EqualTo(3));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add3StringsRemoveStringHowManyStartsWithEmptyPrefixShouldBeEqualTo2(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.HowManyStartsWithPrefix(""), Is.EqualTo(2));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add3StringsRemoveStringHowManyStartsWithEmptyPrefixShouldBeEqualToSize(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.HowManyStartsWithPrefix(""), Is.EqualTo(trie.Size));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add4StringsRemoveStringHowManyStartsWithPrefixShouldBeEqualTo3(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%strind");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.HowManyStartsWithPrefix("%str"), Is.EqualTo(3));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void Add4StringsRemoveStringHowManyStartsWithPrefixShouldBeEqualTo2(Trie trie)
    {
        trie.Add("%strndasdasd");
        trie.Add("%string");
        trie.Add("%strind");
        trie.Add("%str");
        trie.Remove("%str");
        Assert.That(trie.HowManyStartsWithPrefix("%stri"), Is.EqualTo(2));
    }
}