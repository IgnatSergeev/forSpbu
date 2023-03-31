
var tree = new ParseTree.ParseTree("(* (+ 1 1) 2)");
tree.Print();
Console.WriteLine(tree.Evaluate());