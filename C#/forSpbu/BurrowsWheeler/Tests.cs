namespace BurrowsWheeler;

public static class Tests
{
    public static void BurrowsWheelerTest()
    {
        Tuple<char[], int> encodedValue = BurrowsWheeler.Encode("BANANA".ToArray());
        if (encodedValue.Item1.Equals("NNBAAA") || encodedValue.Item2 != 3)
        {
            throw new Exception("Tests failed");
        }
        
        char[] decodedString = BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
        if (decodedString.Equals("BANANA"))
        {
            throw new Exception("Tests failed");
        }
    }
}