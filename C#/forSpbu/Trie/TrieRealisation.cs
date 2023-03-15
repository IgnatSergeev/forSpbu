namespace Trie;

public class TrieRealisation : Trie
{
    private class Node
    {
        public Node(char symbol, Node? previousNode)
        {
            PreviousNode = previousNode;
            _symbol = symbol;
            _nextDictionary = new Dictionary<char, Node>();
            IsTheEndOfTheString = false;
        }

        public bool HasNext(char symbol)
        {
            return _nextDictionary.ContainsKey(symbol);
        }
        public Node GetNext(char symbol)
        {
            return _nextDictionary[symbol];
        }

        public void AddNext(char symbol)
        {
            if (!HasNext(symbol))
            {
                _nextDictionary.Add(symbol, new Node(symbol, this));
            }
        }

        public void RemoveNext(Node nodeToRemove)
        {
            _nextDictionary.Remove(nodeToRemove._symbol);
        }

        public int GetNextDictionarySize()
        {
            return _nextDictionary.Keys.Count;
        }

        public Node? PreviousNode { get; }

        private readonly char _symbol;
        public bool IsTheEndOfTheString { get; set; }
        private readonly Dictionary<char, Node> _nextDictionary;
        public int NumberOfUpStrings { get; set; }
    }
    
    public TrieRealisation()
    {
        _head = new Node('0', null);
    }
    
    public override bool Add(string element)
    {
        if (element == null)
        {
            throw new Exception("Cannot add null string");
        }

        if (element.Length == 0)
        {
            throw new Exception("Cannot add empty string");
        }

        var size = element.Length;
        var currentNode = _head;
        for (var i = 0; i < size; i++)
        {
            if (currentNode.HasNext(element[i]))
            {
                currentNode = currentNode.GetNext(element[i]);
            }
            else
            {
                currentNode.AddNext(element[i]);
                currentNode = currentNode.GetNext(element[i]);
            }
        }

        if (currentNode.IsTheEndOfTheString) return false;
        currentNode.IsTheEndOfTheString = true;

        currentNode = _head;
        currentNode.NumberOfUpStrings += 1;
        for (var i = 0; i < size; i++)
        {
            currentNode = currentNode.GetNext(element[i]);
            currentNode.NumberOfUpStrings += 1;
        }

        return true;
    }
    
    public override bool Contains(string element)
    {
        if (element == null)
        {
            throw new Exception("Cannot check of containing null string");
        }

        if (element.Length == 0)
        {
            throw new Exception("Cannot check of containing empty string");
        }
        
        var size = element.Length;
        var currentNode = _head;
        for (var i = 0; i < size; i++)
        {
            if (currentNode.HasNext(element[i]))
            {
                currentNode = currentNode.GetNext(element[i]);
            }
            else
            {
                return false;
            }
        }

        return currentNode.IsTheEndOfTheString;
    }
    
    public override bool Remove(string element)
    {
        if (element == null)
        {
            throw new Exception("Cannot remove null string");
        }
        
        if (element.Length == 0)
        {
            throw new Exception("Cannot remove empty string");
        }
        
        var size = element.Length;
        var currentNode = _head;
        for (var i = 0; i < size; i++)
        {
            if (currentNode.HasNext(element[i]))
            {
                currentNode = currentNode.GetNext(element[i]);
            }
            else
            {
                return false;
            }
        }

        if (currentNode.IsTheEndOfTheString == false)
        {
            return false;
        }

        if (currentNode.GetNextDictionarySize() > 0)
        {
            currentNode.IsTheEndOfTheString = false;
        }
        else
        {
            var nodeToRemove = currentNode;
            while (nodeToRemove.PreviousNode != _head && nodeToRemove.PreviousNode != null && nodeToRemove.PreviousNode.IsTheEndOfTheString != true 
                   && nodeToRemove.PreviousNode.GetNextDictionarySize() > 1)
            {
                nodeToRemove = nodeToRemove.PreviousNode;
            }
            
            nodeToRemove.PreviousNode?.RemoveNext(nodeToRemove);
        }
        
        currentNode = _head;
        currentNode.NumberOfUpStrings -= 1;
        for (var i = 0; i < size; i++)
        {
            currentNode = currentNode.GetNext(element[i]);
            currentNode.NumberOfUpStrings -= 1;
        }
        return true;
    }
    
    public override int HowManyStartsWithPrefix(string prefix)
    {
        if (prefix == null)
        {
            throw new Exception("Cannot count ho many string starts with null prefix");
        }
        
        if (prefix.Length == 0)
        {
            throw new Exception("Cannot count ho many string starts with empty prefix");
        }
        
        var size = prefix.Length;
        var currentNode = _head;
        for (var i = 0; i < size; i++)
        {
            if (currentNode.HasNext(prefix[i]))
            {
                currentNode = currentNode.GetNext(prefix[i]);
            }
            else
            {
                return 0;
            }
        }

        return currentNode.NumberOfUpStrings;
    }
    
    private readonly Node _head;
    public override int Size => _head.NumberOfUpStrings;
}