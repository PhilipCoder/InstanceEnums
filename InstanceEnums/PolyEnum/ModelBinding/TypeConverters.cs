using System.ComponentModel;

namespace InstanceEnums.PolyEnum.ModelBinding;

public class StringTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return (sourceType == typeof(string));
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        var enumType = EnumRegistry.GetEnumBaseType(destinationType);
        return context.Instance.GetType() == typeof(string) && enumType != null;
    }
}
