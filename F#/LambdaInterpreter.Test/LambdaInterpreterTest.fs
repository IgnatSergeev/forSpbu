module LambdaInterpreter.Test

open NUnit.Framework
open FsUnit

let S = Abstr("x", Abstr("y", Abstr("z", App(App(Var("x"), Var("z")), App(Var("y"), Var("z"))))))
let K = Abstr("x", Abstr("y", Var("x")))
let KStar = Abstr("y", Abstr("x", Var("x")))
let I = Abstr("x", Var("x"))

[<Test>]
let VarTest () =
    Var("a") |> normalize |> should equal (Var("a"))

[<Test>]
let AbstrTest () =
    I |> normalize |> should equal I

[<Test>]
let KIKStarTest () =
    App(K, I) |> normalize |> should equal KStar

[<Test>]
let SKKTest () =
    App(App(S, K), K) |> normalize |> should equal I

[<Test>]
let RenameTest () =
    App(Abstr("a", Abstr("b", App(Var("a"),Var("b")))), Var("b")) |> normalize |> should equal I
