namespace BurrowsWheeler;

public static class Test
{
    public static void TestBurrowsWheeler()
    {
        Tuple<string, int> encodedValue = BurrowsWheeler.Encode("BANANA");
        if (string.Compare(encodedValue.Item1, "NNBAAA") != 0 || encodedValue.Item2 != 3)
        {
            throw new Exception("Tests failed");
        }

        string decodedString = BurrowsWheeler.Decode(encodedValue.Item1, encodedValue.Item2);
        if (string.Compare(decodedString, "BANANA") != 0)
        {
            throw new Exception("Tests failed");
        }
    }
}