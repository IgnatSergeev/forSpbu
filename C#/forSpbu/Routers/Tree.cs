using System.Xml;

namespace Routers;

public class Graph
{
    private class Node
    {
        public void AddEdge(int neighbourNodeNum, int length)
        {
            if (_adjacentNodes.ContainsKey(neighbourNodeNum))
            {
                return;
            }
            _adjacentNodes.Add(neighbourNodeNum, length);
        }

        public void RemoveEdge(int neighbourNodeNum)
        {
            _adjacentNodes.Remove(neighbourNodeNum);
        }

        public int GetLength(int neighbourNodeNum)
        {
            return _adjacentNodes[neighbourNodeNum];
        }
        
        public void SetLength(int neighbourNodeNum, int newLength)
        {
            _adjacentNodes[neighbourNodeNum] = newLength;
        }
        
        public Dictionary<int, int>.KeyCollection GetKeys()
        {
            return _adjacentNodes.Keys;
        }

        private readonly Dictionary<int, int> _adjacentNodes = new ();
    }
    
    public void AddNode(int nodeNum)
    {
        if (!_nodes.ContainsKey(nodeNum))
        {
            _nodes.Add(nodeNum, new Node());
        }
    }

    public void AddEdge(int firstNodeNum, int secondNodeNum, int length)
    {
        AddNode(firstNodeNum);
        AddNode(secondNodeNum);
        
        _nodes[firstNodeNum].AddEdge(secondNodeNum, length);
        _nodes[secondNodeNum].AddEdge(firstNodeNum, length);
    }

    public void RemoveNode(int nodeNum)
    {
        if (!_nodes.ContainsKey(nodeNum)) { return; }
        
        foreach (var neighbourNodeNum in _nodes[nodeNum].GetKeys())
        { 
            RemoveEdge(nodeNum, neighbourNodeNum);
        }

        _nodes.Remove(nodeNum);
        
    }

    public void RemoveEdge(int firstNodeNum, int secondNodeNum)
    {
        if (!_nodes.ContainsKey(firstNodeNum) || !_nodes.ContainsKey(secondNodeNum))
        {
            return;
        }
        
        _nodes[firstNodeNum].RemoveEdge(secondNodeNum);
        _nodes[secondNodeNum].RemoveEdge(firstNodeNum);
    }

    public void TransformToMaximalWeightTree()
    {
        var priority = new Dictionary<int, int>(_nodes.Keys.Select(x => new KeyValuePair<int, int>(x, -2)).ToArray());
        var startNodeNum = _nodes.Keys.ToArray()[0];
        priority[startNodeNum] = 0;

        var numberOfNodes = _nodes.Count;
        var visitedNodes = new Dictionary<int, bool>(_nodes.Keys.Select(x => new KeyValuePair<int, bool>(x, false)).ToArray());
        var newTree = new Graph();
        
        for (var i = 0; i < numberOfNodes; i++)
        {
            var nodeWithBiggestPriority = priority.MaxBy(x => visitedNodes[x.Key] ? -1 : x.Value).Key;
            if (visitedNodes[nodeWithBiggestPriority])
            {
                break;
            }
            visitedNodes[nodeWithBiggestPriority] = true;

            foreach (var neighbourNodeNum in _nodes[nodeWithBiggestPriority].GetKeys())
            {
                var edgeLength = _nodes[nodeWithBiggestPriority].GetLength(neighbourNodeNum);
                if (!visitedNodes[neighbourNodeNum] && priority[neighbourNodeNum] < edgeLength)
                {
                    priority[neighbourNodeNum] = edgeLength;
                    newTree.RemoveNode(neighbourNodeNum);
                    newTree.AddEdge(nodeWithBiggestPriority, neighbourNodeNum, edgeLength);
                }
            }
        }

        _nodes = newTree._nodes;
    }

    public void Print()
    {
        var numOfNodes = _nodes.Count;
        for (var nodeNum = 1; nodeNum <= numOfNodes; nodeNum++) 
        {
            var neighbours = _nodes[nodeNum].GetKeys().ToArray();
            Array.Sort(neighbours);
            
            var numOfNeighbours = neighbours.Length;
            var index = 0;
            var isFirstNeighbour = true;
            foreach (var neighbourNodeNum in neighbours)
            {
                ++index;
                if (neighbourNodeNum > nodeNum)
                {
                    if (isFirstNeighbour)
                    {
                        Console.Write(nodeNum + ": ");
                        isFirstNeighbour = false;
                    }

                    Console.Write(neighbourNodeNum + " (" + _nodes[nodeNum].GetLength(neighbourNodeNum) + ")");
                    if (index < numOfNeighbours)
                    {
                        Console.Write(", ");
                    }
                    else
                    {
                        Console.WriteLine();
                    } 
                }
            }
        }
    }
    
    private Dictionary<int, Node> _nodes = new ();
}