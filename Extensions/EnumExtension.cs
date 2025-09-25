using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        if (value == null)
            return string.Empty;
        var field = value.GetType().GetField(value.ToString());
        var customAttribute = field.GetCustomAttribute<DescriptionAttribute>(true);
        return customAttribute != null ? customAttribute.Description : field.Name;
    }

    public static string GetDisplayName(this Enum value)
    {
        if (value == null)
            return string.Empty;
        var customAttribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayAttribute>(true);
        return customAttribute != null ? customAttribute.GetName() : value.GetDescription();
    }
}
