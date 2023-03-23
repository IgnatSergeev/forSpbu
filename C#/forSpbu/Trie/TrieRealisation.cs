using System.Runtime.InteropServices.JavaScript;

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
        public int Code { get; set; } = -1;
    }
    
    public TrieRealisation()
    {
        _head = new Node('0', null);
    }

    public override bool AddChar(IEnumerable<char> prefix, char symbol, int code)
    {
        if (prefix == null)
        {
            throw new ArgumentNullException(nameof(prefix));
        }

        var currentNode = _head;
        foreach (var element in prefix)
        {
            if (currentNode.HasNext(element))
            {
                currentNode = currentNode.GetNext(element);
            }
            else
            {
                return false;
            }
        }

        if (currentNode.HasNext(symbol))
        {
            return false;
        }
        currentNode.AddNext(symbol);
        currentNode = currentNode.GetNext(symbol);
        currentNode.IsTheEndOfTheString = true;
        currentNode.Code = code;
        _stringCodes.Add(code, prefix.Append(symbol).ToArray());

        currentNode = _head;
        currentNode.NumberOfUpStrings += 1;
        foreach (var element in prefix)
        {
            currentNode = currentNode.GetNext(element);
            currentNode.NumberOfUpStrings += 1;
        }

        currentNode = currentNode.GetNext(symbol);
        currentNode.NumberOfUpStrings += 1;
        return true;
    }
    
    public override bool Contains(IEnumerable<char> element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }
        if (!element.Any())
        {
            throw new ArgumentException("Empty string: " + nameof(element));
        }
        
        Node currentNode = _head;
        foreach (var symbol in element)
        {
            if (currentNode.HasNext(symbol))
            {
                currentNode = currentNode.GetNext(symbol);
            }
            else
            {
                return false;
            }
        }

        return currentNode.IsTheEndOfTheString;
    }
    
    public override bool Remove(IEnumerable<char> element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }
        if (!element.Any())
        {
            throw new ArgumentException("Empty string: " + nameof(element));
        }
        
        Node currentNode = _head;
        foreach (var symbol in element)
        {
            if (currentNode.HasNext(symbol))
            {
                currentNode = currentNode.GetNext(symbol);
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
        _stringCodes.Remove(currentNode.Code);
        
        if (currentNode.GetNextDictionarySize() > 0)
        {
            currentNode.IsTheEndOfTheString = false;
        }
        else
        {
            var nodeToRemove = currentNode;
            while (nodeToRemove.PreviousNode != _head && nodeToRemove.PreviousNode != null && nodeToRemove.PreviousNode.IsTheEndOfTheString != true 
                   && nodeToRemove.PreviousNode.GetNextDictionarySize() <= 1)
            {
                nodeToRemove = nodeToRemove.PreviousNode;
            }
            
            nodeToRemove.PreviousNode?.RemoveNext(nodeToRemove);
        }
        
        currentNode = _head;
        currentNode.NumberOfUpStrings -= 1;
        foreach (var symbol in element)
        {
            if (!currentNode.HasNext(symbol))
            {
                break;
            }
            currentNode = currentNode.GetNext(symbol);
            currentNode.NumberOfUpStrings -= 1;
        }
        return true;
    }
    
    public override int HowManyStartsWithPrefix(IEnumerable<char> prefix)
    {
        if (prefix == null)
        {
            throw new ArgumentNullException(nameof(prefix));
        }

        Node currentNode = _head;
        foreach (var symbol in prefix)
        {
            if (currentNode.HasNext(symbol))
            {
                currentNode = currentNode.GetNext(symbol);
            }
            else
            {
                return 0;
            }
        }

        return currentNode.NumberOfUpStrings;
    }

    public override int GetCode(IEnumerable<char> element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }
        if (!element.Any())
        {
            throw new ArgumentException("Empty string: " + nameof(element));
        }
        
        Node currentNode = _head;
        foreach (var symbol in element)
        {
            if (currentNode.HasNext(symbol))
            {
                currentNode = currentNode.GetNext(symbol);
            }
            else
            {
                return -1;
            }
        }

        return currentNode.Code;
    }

    public override char[]? GetString(int code)
    {
        if (code < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(code));
        }

        _stringCodes.TryGetValue(code, out var value);
        return value;
    }

    public override bool ContainsCode(int code)
    {
        return _stringCodes.ContainsKey(code);
    }
    
    private readonly Node _head;
    private readonly Dictionary<int, char[]> _stringCodes = new ();
    public override int Size => _head.NumberOfUpStrings;
}