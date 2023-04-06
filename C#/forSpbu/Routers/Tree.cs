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

        if (priority.Values.Any(nodePriority => nodePriority == -2))
        {
            throw new WrongGraphException("Граф не связен");
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
    
    public void Parse(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        var lines = File.ReadLines(path).ToArray();
        var numOfLines = lines.Length;
        for (var i = 0; i < numOfLines; i++)
        {
            var newLine = RemoveUnwantedChars(lines[i]);
            var splitLine = newLine.Split();
            if (splitLine is not { Length: > 1 } || string.IsNullOrEmpty(splitLine[0]))
            {
                throw new ParseException();
            }
            
            if (int.TryParse(splitLine[0], out var nodeNum))
            {
                AddNode(nodeNum);
            }
            else
            {
                throw new ParseException();
            }

            for (var j = 1; j < splitLine.Length; j += 2)
            {
                if (int.TryParse(splitLine[j], out var neighbourNodeNum) && int.TryParse(splitLine[j + 1], out var length))
                {
                    AddEdge(nodeNum, neighbourNodeNum, length);
                }
                else
                {
                    throw new ParseException();
                }
            }
        }
    }

    private static string RemoveUnwantedChars(string line)
    {
        return new string(line.Select(x => x is '(' or ')' or ':' ? ' ' : x).ToArray());
    }
    
    private Dictionary<int, Node> _nodes = new ();
}