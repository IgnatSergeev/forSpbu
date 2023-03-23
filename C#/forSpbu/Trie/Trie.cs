namespace Trie;

/// <summary>
/// Container for storing strings
/// </summary>
public class Trie
{
    /// <summary>
    /// Adds a string element into the container works in O(|element|)
    /// </summary>
    /// <param name="element">String to add</param>
    /// <returns>true if the element wasn't in the container,
    /// false if it was</returns>
    public virtual bool Add(IEnumerable<char> element)
    {
        return false;
    }

    /// <summary>
    /// Adds a char after a prefix string
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="symbol"></param>
    /// <param name="code"></param>
    /// <returns>true if prefix contains in trie and symbol doesnt</returns>
    public virtual bool AddChar(IEnumerable<char> prefix, char symbol, int code)
    {
        return false;
    }

    /// <summary>
    /// Checks if the container contains the element works in O(|element|)
    /// </summary>
    /// <param name="element">Element to check</param>
    /// <returns>true if it contains the element, false if it`s not</returns>
    public virtual bool Contains(IEnumerable<char> element)
    {
        return false;
    }

    /// <summary>
    /// Removes an element from the container works in O(|element|)
    /// </summary>
    /// <param name="element">Element to remove</param>
    /// <returns>true if the element was in the container, false if it wasn't</returns>
    public virtual bool Remove(IEnumerable<char> element)
    {
        return false;
    }

    /// <summary>
    /// Checks how many string in the container starts with given prefix
    /// </summary>
    /// <param name="prefix">Prefix to check with</param>
    /// <returns>number of string which starts with given prefix</returns>
    public virtual int HowManyStartsWithPrefix(IEnumerable<char> prefix)
    {
        return 0;
    }

    /// <summary>
    /// Gets code associated with the string value
    /// </summary>
    /// <param name="element">string to get code from</param>
    /// <returns>code of the string</returns>
    public virtual int GetCode(IEnumerable<char> element)
    {
        return -1;
    }
    
    /// <summary>
    /// Gets string associated with the code value
    /// </summary>
    /// <param name="code">code to get the string from</param>
    /// <returns>string with given code</returns>
    public virtual char[]? GetString(int code)
    {
        return null;
    }

    /// <summary>
    /// Checks if the trie contain the string with the given code
    /// </summary>
    /// <param name="code">code to check with</param>
    /// <returns>true if the code contains, false if not</returns>
    public virtual bool ContainsCode(int code)
    {
        return false;
    }
    
    public virtual int Size { get; }
 }