namespace UniqueList;

public class UniqueList<T> : List<T>
{
    public new void Add(T valueToAdd, int position)
    {
        if (_valuesSet.Contains(valueToAdd))
        {
            throw new ValueAlreadyExistException(nameof(valueToAdd));
        }
        base.Add(valueToAdd, position);
        _valuesSet.Add(valueToAdd);
    }

    private readonly HashSet<T> _valuesSet = new ();
}