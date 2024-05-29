module ParseTree.Test

open NUnit.Framework
open FsUnit

[<Test>]
let ValueParseTreeTest () =
    Value 1 |> calulateParseTree |> should equal 1

[<Test>]
let SumParseTreeTest () =
    Add(Value 1, Value 2) |> calulateParseTree |> should equal 3

[<Test>]
let SubtractParseTreeTest () =
    Subtract(Value 1, Value 2) |> calulateParseTree |> should equal -1

[<Test>]
let MultiplyParseTreeTest () =
    Multiply(Value 1, Value 4) |> calulateParseTree |> should equal 4

[<Test>]
let DivideParseTreeTest () =
    Divide(Value 5, Value 2) |> calulateParseTree |> should equal 2

[<Test>]
let DivideZeroParseTreeTest () =
    (fun () -> Divide(Value 5, Value 0) |> calulateParseTree |> ignore) |> should throw typeof<System.DivideByZeroException>

[<Test>]
let ActualParseTreeTest () =
    Add(Value 2, Multiply(Divide(Value 6, Value 2), Value 2)) |> calulateParseTree |> should equal 8
