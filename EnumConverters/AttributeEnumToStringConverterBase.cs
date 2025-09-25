using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Core.EnumConverters;

public abstract class AttributeEnumToStringConverterBase<TAttribute>(Type type) : EnumConverter(type)
  where TAttribute : Attribute
{
    protected abstract string GetStringByAttribute(TAttribute attribute);

    public sealed override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
        object obj = (object)string.Empty;
        if (destinationType == typeof(string))
        {
            if (value != null)
            {
                FieldInfo field = value.GetType().GetField(value?.ToString());
                if (field != null)
                {
                    object[] customAttributes = field.GetCustomAttributes(typeof(TAttribute), false);
                    string str = ((IEnumerable<object>)customAttributes).Any<object>() ? this.GetStringByAttribute((TAttribute)customAttributes[0]) : string.Empty;
                    obj = !string.IsNullOrEmpty(str) ? (object)str : value.ToString();
                }
            }
        }
        else
            obj = base.ConvertTo(context, culture, value, destinationType);
        return obj;
    }
}
