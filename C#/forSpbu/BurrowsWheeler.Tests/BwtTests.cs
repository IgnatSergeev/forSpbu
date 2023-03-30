namespace BurrowsWheeler.Tests;

public class BwtTests
{
    [Test]
    public void EncodeStringBANANAShouldReturnNNBAAAand3()
    {
        var (encodedString, index) = BurrowsWheeler.Encode("BANANA".ToArray());
        Assert.That(encodedString, Is.EqualTo("NNBAAA"));
        Assert.That(index, Is.EqualTo(3));
    }
    
    [Test]
    public void DecodeStringNNBAAAand3ShouldReturnBANANA()
    {
        var decodedString = BurrowsWheeler.Decode("NNBAAA".ToArray(), 3);
        Assert.That(decodedString, Is.EqualTo("BANANA"));
    }
    
    [Test]
    public void EncodeNullStringShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => BurrowsWheeler.Encode(null), "stringToEncode");
    }
    
    [Test]
    public void EncodeEmptyStringShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BurrowsWheeler.Encode("".ToArray()), "Empty string: stringToEncode");
    }
    
    [Test]
    public void DecodeEmptyStringShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BurrowsWheeler.Decode("".ToArray(), 0), "Empty string: stringToDecode");
    }
    
    [Test]
    public void DecodeNullStringShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => BurrowsWheeler.Decode(null, 0), "stringToDecode");
    }
    
    [Test]
    public void DecodeStringIndexLessThanZeroShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BurrowsWheeler.Decode("n".ToArray(), -1), "indexOfInitialString");
    }
    
    [Test]
    public void DecodeStringIndexGreaterThanSizeOfStringShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BurrowsWheeler.Decode("n".ToArray(), 1), "indexOfInitialString");
    }
}