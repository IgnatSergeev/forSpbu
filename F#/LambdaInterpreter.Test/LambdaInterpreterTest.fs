module LambdaInterpreter.Test

open NUnit.Framework
open FsUnit

let S = Abstr("x", Abstr("y", Abstr("z", App(App(Var("x"), Var("z")), App(Var("y"), Var("z"))))))
let K = Abstr("x", Abstr("y", Var("x")))
let KStar = Abstr("y", Abstr("x", Var("x")))
let Ix = Abstr("x", Var("x"))
let Iz = Abstr("z", Var("z"))

[<Test>]
let getAllNamesTest () =
    App(Abstr("a", Abstr("x", App(Var("a"), Var("x")))), Var("b")) |> getAllNames |> should equal (Set.ofList [ "a"; "x"; "b"])

[<Test>]
let getNameTest () =
    App(Abstr("a", Abstr("b", App(App(Var("a"), Var("b1")), Var("b")))), Var("b")) |> generateName "b" |> should equal "b2"

[<Test>]
let alphaTest () =
    Abstr("b", App(Var("a"), Var("b"))) |> alphaTransform |> should equal (Abstr("b1", App(Var("a"), Var("b1"))))

[<Test>]
let betaTest () =
    App(Abstr("a", Abstr("b", App(App(Var("a"), Var("b1")), Var("b")))), Var("b")) |> betaTransform |> should equal (Abstr("b2", App(App(Var("b"), Var("b1")), Var("b2"))))

[<Test>]
let varNormalizeTest () =
    Var("a") |> normalize |> should equal (Var("a"))

[<Test>]
let INormalizeTest () =
    Ix |> normalize |> should equal Ix

[<Test>]
let renameNormalizeTest () =
    App(Abstr("a", Abstr("b", App(Var("a"),Var("b")))), Var("b")) |> normalize |> should equal (Abstr("b1", App(Var("b"),Var("b1"))))

[<Test>]
let KIKStarTest () =
    App(K, Ix) |> normalize |> should equal KStar

[<Test>]
let SkkTest () =
    App(App(S, K), K) |> normalize |> should equal Iz
