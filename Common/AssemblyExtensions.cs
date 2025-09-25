using Core.Properties;
using System.Reflection;

namespace Core.Common;

public static class AssemblyExtensions
{
    public static TResult GetInfoFromAttribute<TAttribute, TResult>(
      this Assembly assembly,
      Func<TAttribute, TResult> func,
      Func<TResult> defaultValueFunc = null)
      where TAttribute : Attribute
    {
        var customAttribute = assembly.GetCustomAttribute<TAttribute>();
        if (customAttribute != null)

            return func(customAttribute);

        if (defaultValueFunc != null)

            return defaultValueFunc();

        throw new InvalidOperationException(string.Format(Resources.StrError_AttributeNotFound, assembly.GetName().Name, typeof(TAttribute).Name));
    }
}
