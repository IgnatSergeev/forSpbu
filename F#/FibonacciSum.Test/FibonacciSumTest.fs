module FibonacciSum.Test

open NUnit.Framework

[<Test>]
let FibonacciEvenSumRegress() =
    Assert.That(fibonacciEvenSum(), Is.EqualTo 1089154)