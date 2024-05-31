module Workflows.Test

open NUnit.Framework
open FsUnit

[<Test>]
let roundingTest () =
    rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should equal 0.048

[<Test>]
let stringCalcTest () =
    calculate {
        let! a = "1"
        let! b = "2"
        return a + b
    } |> should equal (Some(3))

[<Test>]
let stringIncorrectCalcTest () =
    calculate {
        let! a = "1"
        let! b = "a"
        let z = a + b
        return z
    } |> should equal None
