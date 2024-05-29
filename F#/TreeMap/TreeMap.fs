module TreeMap

type BinTree<'T> =
    | Node of 'T * BinTree<'T> * BinTree<'T>
    | Leaf of 'T

let rec treeMap func tree = 
    match tree with
    | Node (value, lhs, rhs) -> Node (func value, treeMap func lhs, treeMap func rhs)
    | Leaf value -> Leaf (func value)
