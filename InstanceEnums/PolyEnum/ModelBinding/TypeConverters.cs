using InstanceEnums.PolyEnum.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums.PolyEnum.ModelBinding
{
    public class SomeWrapperTypeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            var enumType = EnumRegistry.GetEnumType(destinationType);
            Debug.WriteLine("=========================================================================");
            Debug.WriteLine(context.Instance.GetType().FullName);
            return context.Instance.GetType() == typeof(string) && enumType != null;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var casted = value as string;
            return casted != null
                ? context.Instance.GetType().GetMethod("Get").Invoke(null, new object[] { (dynamic)value })
                : null;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var casted = value as string;
            return casted != null
                ? context.Instance.GetType().GetMethod("Get").Invoke(null, new object[] { (dynamic)value })
                : null;
        }
    }
}
