module Crawler.Test

open NUnit.Framework
open FsUnit

[<Test>]
let incorrectUrlTest () =
    let result = crawlAsync "asd" |> Async.RunSynchronously 
    match result with
    | Error _ ->  Assert.Pass()
    | _ -> Assert.Fail()

[<Test>]
let complicatedTest () =
    let result = crawlAsync "https://clang.llvm.org/get_started.html" |> Async.RunSynchronously 
    let expectedResult : (string * Choice<int, exn>)[]= List.toArray [
            ("http://llvm.org/", Choice1Of2(16655)); 
            ("http://llvm.org/releases/download.html", Choice2Of2(System.Net.WebException())); 
            ("http://clang-analyzer.llvm.org", Choice1Of2(7996)); 
            ("http://lists.llvm.org/mailman/listinfo/cfe-commits", Choice1Of2(6720)); 
            ("http://clang.llvm.org/doxygen/", Choice1Of2(2614)); 
            ("http://llvm.org/devmtg/", Choice1Of2(13563)); 
            ("http://getgnuwin32.sourceforge.net/", Choice1Of2(10391))
        ]
    match result with
    | Error _ ->  
        Assert.Fail()
    | Ok a -> 
        let check x y =
             fst(x) |> should equal (fst(y))
             match (snd(x), snd(y)) with
             | (Choice1Of2 a, Choice1Of2 b) -> a |> should equal b
             | (Choice2Of2 (a: exn), Choice2Of2 (b: exn)) -> a.GetType() |> should equal (b.GetType())
             | _ -> Assert.Fail()
        Seq.iter2 check expectedResult a
