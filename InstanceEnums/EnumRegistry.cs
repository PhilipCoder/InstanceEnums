using InstanceEnums.PolyEnum.ModelBinding;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums
{
    public static class EnumRegistry
    {
        internal static ConcurrentDictionary<Type, Type> EnumMappings = new ConcurrentDictionary<Type, Type>();
        public static void RegisterEnum<EnumType, BaseInterfaceType>()
        {
            TypeDescriptor.AddAttributes(typeof(BaseInterfaceType), new TypeConverterAttribute(typeof(SomeWrapperTypeTypeConverter)));

            EnumMappings.TryAdd(typeof(BaseInterfaceType), typeof(EnumType));
        }

        internal static Type GetEnumType(Type baseInterfaceType)
        {
            if (EnumMappings.TryGetValue(baseInterfaceType, out var enumType))
            {
                return enumType;
            }
            return null;
        }
    }
}
