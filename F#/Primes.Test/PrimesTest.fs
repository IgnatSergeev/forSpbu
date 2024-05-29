module Primes.Test

open NUnit.Framework
open FsUnit

[<Test>]
let testStart () = 
    primes |> Seq.take 5 |> Seq.toList |> should equal [ 2; 3; 5; 7; 11 ]
