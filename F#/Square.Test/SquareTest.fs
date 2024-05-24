module Square.Test

open NUnit.Framework

[<TestCase (-5, "")>]
[<TestCase (0, "")>]
[<TestCase (1, "*")>]
[<TestCase (2, "**\n**")>]
[<TestCase (5, "*****\n*   *\n*   *\n*   *\n*****")>]
let TestGenSquare (num, result) =
    Assert.That(genStarSquare num, Is.EqualTo result)