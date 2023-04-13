namespace ListFunctions;

public static class ListFunctions
{
    public static List<T> Map<T>(IEnumerable<T> list, Func<T, T> transformFunction)
    {
        return list == null ? throw new ArgumentNullException(nameof(list)) 
            : new List<T>(list.Select(transformFunction));
    }
    
    public static List<T> Filter<T>(IEnumerable<T> list, Func<T, bool> filterFunction)
    {
        return list == null ? throw new ArgumentNullException(nameof(list)) 
            : new List<T>(list.Where(filterFunction));
    }
    
    public static T Fold<T>(IEnumerable<T> list, Func<T, T, T> transformFunction)
    {
        var listArray = list.ToArray();
        return listArray.Length == 0 ? throw new ArgumentNullException(nameof(list)) 
            : listArray.Aggregate(listArray.First(), transformFunction);
    }
}