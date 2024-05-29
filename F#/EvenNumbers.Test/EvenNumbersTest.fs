module EvenNumbers.Test

open NUnit.Framework
open FsUnit
open FsCheck

let mapEqualsFold (list: list<uint32>) = mapEvenCounter list = foldEvenCounter list
let mapEqualsFilter (list: list<uint32>) = mapEvenCounter list = filterEvenCounter list

[<Test>]
let mapEqualsFilterTest () =
    Check.QuickThrowOnFailure mapEqualsFilter

[<Test>]
let mapEqualsFoldTest () =
    Check.QuickThrowOnFailure mapEqualsFold

[<Test>]
let mapEmptyTest () =
    [] |> mapEvenCounter |> should equal 0

[<Test>]
let mapRepetedTest () =
    [ 0u; 0u; 1u; 1u; ] |> mapEvenCounter |> should equal 2

[<Test>]
let mapNoneEvenTest () =
    [ 1u; 1u; ] |> mapEvenCounter |> should equal 0
