namespace Trie.Tests;

public class TrieTests
{
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
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.Add(""), "Empty container: element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<ArgumentNullException>(() => trie.Add(null), "element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void RemoveNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<ArgumentNullException>(() => trie.Remove(null), "element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void RemoveEmptyStringShouldThrowException(Trie trie)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.Remove(""), "Empty container: element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void ContainsEmptyStringShouldThrowException(Trie trie)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.Contains(""), "Empty container: element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void ContainsNullStringShouldThrowException(Trie trie)
    {
        Assert.Throws<ArgumentNullException>(() => trie.Contains(null), "element");
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
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharContainsShouldReturnTrue(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.Contains("s"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharContainsCodeShouldReturnTrue(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.ContainsCode(1));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharGetCodeShouldReturnCorrectCode(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.GetCode("s"), Is.EqualTo(1));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharGetStringShouldReturnChar(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.GetString(1), Is.EqualTo("s"));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharGetStringShouldReturnNull(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.GetString(2), Is.EqualTo(null));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharGetCodeShouldReturnMinusOne(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(trie.GetCode("sa"), Is.EqualTo(-1));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void AddCharContainsCodeShouldReturnFalse(Trie trie)
    {
        trie.AddChar("", 's', 1);
        Assert.That(!trie.ContainsCode(2));
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void ContainsCodeMinusOneShouldThrowArgumentOutOfRangeException(Trie trie)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.ContainsCode(-1), "code");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void GetStringMinusOneShouldThrowArgumentOutOfRangeException(Trie trie)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.GetString(-1), "code");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void GetCodeNullShouldThrowArgumentNullException(Trie trie)
    {
        Assert.Throws<ArgumentNullException>(() => trie.GetCode(null), "element");
    }
    
    [Test, TestCaseSource(nameof(TrieRealisations))]
    public void GetCodeEmptyStringShouldThrowArgumentOutOfRangeException(Trie trie)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => trie.GetCode(""), "Empty container: element");
    }
}