namespace MatrixMult.Tests;

public static class MultTests
{
    private static readonly (Matrix, Matrix, Matrix?)[] Tests =
    {
        (
            new Matrix(
                new int[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                }),
            new Matrix(
                new int[,]
                {
                    { 2, 3 },
                    { 4, 5 }
                }),
            new Matrix(
                new int[,]
                {
                    { 10, 13 },
                    { 22, 29 }
                })
        ),
        (
            new Matrix(
                new int[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 }
                }),
            new Matrix(
                new int[,]
                {
                    { 1, 2 },
                    { 4, 5 }
                }),
            null
        ),
        (
            new Matrix(
                new int[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 }
                }),
            new Matrix(
                new int[,]
                {
                    { 1, 2 },
                    { 3, 4 },
                    { 5, 6 }
                }),
            new Matrix(
                new int[,]
                {
                    { 22, 28 },
                    { 49, 64 } 
                }) 
        )
    };

    private static IEnumerable<TestCaseData> TestCases()
    {
        foreach (var test in Tests)
        {
            yield return new TestCaseData(MatrixMultiplier.MultiThreadedMultiply, test);
            yield return new TestCaseData(MatrixMultiplier.SingleThreadedMultiply, test);
        }
    }

    private static bool AreMatricesEqual(Matrix fst, Matrix sec)
    {
        if (fst.Height != sec.Height || fst.Width != sec.Width)
        {
            return false;
        }

        for (int i = 0; i < fst.Height; i++)
        {
            for (int j = 0; j < sec.Width; j++)
            {
                if (fst.GetElement(i, j) != sec.GetElement(i, j))
                {
                    return false;
                }
            }
        }

        return true;
    }

    [Test, TestCaseSource(nameof(TestCases))]
    public static void MultiplyMatricesTest(Func<Matrix, Matrix, Matrix> matrixMultImpl, (Matrix, Matrix, Matrix?) test)
    {
        if (test.Item3 == null)
        {
            Assert.Throws<MatrixMultiplierException>(() => matrixMultImpl(test.Item1, test.Item2));
        }
        else
        {
            var multResult = matrixMultImpl(test.Item1, test.Item2);
            Assert.That(AreMatricesEqual(multResult, test.Item3), Is.True);
        }
    } 
}