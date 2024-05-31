module BracketSequence.Test

open NUnit.Framework
open FsUnit

[<Test>]
let emptyTest () =
    "" |> checkBracketSequence |> should equal true

[<Test>]
let roundTest () =
    "()" |> checkBracketSequence |> should equal true

[<Test>]
let roundIncorrectTest () =
    "(()" |> checkBracketSequence |> should equal false

[<Test>]
let squareTest () =
    "[]" |> checkBracketSequence |> should equal true

[<Test>]
let squareIncorrectTest () =
    "[[]" |> checkBracketSequence |> should equal false

[<Test>]
let curlyTest () =
    "{}" |> checkBracketSequence |> should equal true

[<Test>]
let curlyIncorrectTest () =
    "{{}" |> checkBracketSequence |> should equal false

[<Test>]
let complicatedTest () =
    "[({}){}[]]" |> checkBracketSequence |> should equal true

[<Test>]
let complicateIncorrectTest () =
    "({[}){]}" |> checkBracketSequence |> should equal false
