using System.ComponentModel;

namespace Core.EnumConverters;

public class DescriptionAttributeEnumToStringConverter(Type type) :
  AttributeEnumToStringConverterBase<DescriptionAttribute>(type)
{
    protected override string GetStringByAttribute(DescriptionAttribute attribute)
    {
        return attribute.Description;
    }
}