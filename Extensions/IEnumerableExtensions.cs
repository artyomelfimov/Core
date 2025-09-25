namespace Core.Extensions;

public static class IEnumerableExtensions
{
    public static IReadOnlyCollection<T> AsIReadOnlyCollection<T>(this IEnumerable<T> collection)
    {
        return collection == null ? null : (IReadOnlyCollection<T>)((object)(collection as IReadOnlyCollection<T>) ?? collection.ToArray());
    }
}
