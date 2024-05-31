module Lazy.Test

open NUnit.Framework
open FsUnit

type Counter () =
    let mutable callCounter = 0
    member this.Increment () = 
        System.Threading.Interlocked.Increment(&callCounter)
    member this.Counter = callCounter

let lazyImplementations =  
    [ 
    (fun func -> S.Lazy<int> func :> ILazy.ILazy<int>) 
    (fun func -> M.Lazy func)
    (fun func -> LF.Lazy func)
    ] |> List.map (fun x -> TestCaseData(x))

[<Test>]
[<TestCaseSource("lazyImplementations")>]
let getTest (genLazy) =
    let l : ILazy.ILazy<int> = genLazy (fun () -> 1)
    l.Get() |> should equal 1

[<Test>]
let singleThreadCountTest () =
    let counter = Counter()
    let l : ILazy.ILazy<int> = S.Lazy<int>(counter.Increment)
    l.Get() |> ignore
    l.Get() |> ignore
    l.Get() |> ignore
    counter.Counter |> should equal 1

[<Test>]
let simpleMultiThrCountTest () =
    let counter = Counter()
    let l : ILazy.ILazy<int> = M.Lazy<int>(counter.Increment)
    l.Get() |> ignore
    l.Get() |> ignore
    l.Get() |> ignore
    counter.Counter |> should equal 1

[<Test>]
let complexMultiThrCountTest () =
    let counter = Counter()
    let l : ILazy.ILazy<int> = M.Lazy<int>(counter.Increment)
    let getTask = task { return l.Get() }
    let results = (fun _ -> getTask |> Async.AwaitTask) |> Seq.initInfinite  |> Seq.take 10 |> Async.Parallel |> Async.RunSynchronously
    counter.Counter |> should equal 1
    Seq.iter (fun x -> (x = (Seq.last results)) |> should equal true) results

[<Test>]
let simpleLockFreeTest () =
    let counter = Counter()
    let l : ILazy.ILazy<int> = LF.Lazy<int>(counter.Increment)
    l.Get() |> ignore
    l.Get() |> ignore
    l.Get() |> ignore
    counter.Counter |> should equal 1

[<Test>]
let complexLockFreeTest () =
    let counter = Counter()
    let l : ILazy.ILazy<int> = LF.Lazy<int>(counter.Increment)
    let getTask = task { return l.Get() }
    let results = (fun _ -> getTask |> Async.AwaitTask) |> Seq.initInfinite  |> Seq.take 10 |> Async.Parallel |> Async.RunSynchronously
    Seq.iter (fun x -> (x = (Seq.last results)) |> should equal true) results
