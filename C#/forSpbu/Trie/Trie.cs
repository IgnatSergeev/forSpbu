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
    public virtual bool Add(string element)
    {
        return false;
    }

    /// <summary>
    /// Checks if the container contains the element works in O(|element|)
    /// </summary>
    /// <param name="element">Element to check</param>
    /// <returns>true if it contains the element, false if it`s not</returns>
    public virtual bool Contains(string element)
    {
        return false;
    }

    /// <summary>
    /// Removes an element from the container works in O(|element|)
    /// </summary>
    /// <param name="element">Element to remove</param>
    /// <returns>true if the element was in the container, false if it wasn't</returns>
    public virtual bool Remove(string element)
    {
        return false;
    }

    /// <summary>
    /// Checks how many string in the container starts with given prefix
    /// </summary>
    /// <param name="prefix">Prefix to check with</param>
    /// <returns>number of string which starts with given prefix</returns>
    public virtual int HowManyStartsWithPrefix(string prefix)
    {
        return 0;
    }
    
    public virtual int Size { get; }
 }