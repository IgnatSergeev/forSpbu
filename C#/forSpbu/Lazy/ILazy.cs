namespace Lazy;

public interface ILazy<T>
{
    public T? Get();
}