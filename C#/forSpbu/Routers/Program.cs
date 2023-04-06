using Routers;

var graph = new Graph();
graph.AddEdge(1, 2, 3);
graph.AddEdge(1, 3, 4);
graph.AddEdge(1, 5, 1);
graph.AddEdge(2, 3, 5);
graph.AddEdge(3, 4, 2);
graph.AddEdge(3, 5, 6);
graph.AddEdge(4, 5, 7);

graph.TransformToMaximalWeightTree();
graph.Print();


