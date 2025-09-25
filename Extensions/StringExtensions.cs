namespace Core.Extensions;

public static class StringExtensions
{
    public static bool Contains(this string source, string value, StringComparison comparison)
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Contains(value, comparison);
    }
}
