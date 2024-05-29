module ParseTree

type MaybeBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) =
        Some x
    member this.ReturnFrom(m) =
        m

let maybe = new MaybeBuilder()

type ParseTree<'T> = 
    | Value of 'T
    | Add of ParseTree<'T> * ParseTree<'T>
    | Subtract of ParseTree<'T> * ParseTree<'T>
    | Divide of ParseTree<'T> * ParseTree<'T>
    | Multiply of ParseTree<'T> * ParseTree<'T>

let rec calulateParseTree tree = 
    match tree with
    | Value value -> value
    | Add (lhs, rhs) -> calulateParseTree lhs + calulateParseTree rhs
    | Subtract (lhs, rhs) -> calulateParseTree lhs - calulateParseTree rhs
    | Multiply (lhs, rhs) -> calulateParseTree lhs * calulateParseTree rhs
    | Divide (lhs, rhs) -> calulateParseTree lhs / calulateParseTree rhs
