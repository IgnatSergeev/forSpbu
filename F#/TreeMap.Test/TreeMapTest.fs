module TreeMap.Test

open NUnit.Framework
open FsUnit

[<Test>]
let LeafMapTest () =
    Leaf 1 |> treeMap ((+) 1) |> should equal (Leaf 2)

[<Test>]
let TreeMapTest () =
    Node(2, Leaf 1, Node(3, Leaf 4, Leaf 5)) |> treeMap ((+) 1) |> should equal (Node(3, Leaf 2, Node(4, Leaf 5, Leaf 6)))
