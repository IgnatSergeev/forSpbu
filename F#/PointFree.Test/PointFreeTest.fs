module PointFree.Test

open NUnit.Framework
open FsCheck

let functionsEqual x l = 
    func x l = func5 x l

[<Test>]
let equaltyTest () =
    Check.QuickThrowOnFailure functionsEqual
