using InstanceEnums.PolyEnum.ModelBinding;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace InstanceEnums
{
    public static class EnumRegistry
    {
        internal static ConcurrentDictionary<Type, Type> EnumMappings = new ConcurrentDictionary<Type, Type>();
        public static void RegisterEnum<EnumType, BaseInterfaceType>()
        {
            TypeDescriptor.AddAttributes(typeof(BaseInterfaceType), new TypeConverterAttribute(typeof(StringTypeConverter)));

            EnumMappings.TryAdd(typeof(BaseInterfaceType), typeof(EnumType));
        }

        internal static Type GetEnumBaseType(Type baseInterfaceType)
        {
            if (baseInterfaceType != null && EnumMappings.TryGetValue(baseInterfaceType, out var enumType))
            {
                return enumType;
            }
            return null;
        }

        internal static Type GetEnumType(Type baseInterfaceType)
        {
            return EnumMappings.FirstOrDefault(x => x.Key == baseInterfaceType).Value;
        }

        internal static Type GetEnumTypeThatIsParentOf( Type childType)
        {
            return EnumMappings.FirstOrDefault(x => x.Key.IsAssignableFrom(childType)).Key;
        }

        internal static Type GetBaseEnumTypeThatIsParentOf(Type childType)
        {
            return EnumMappings.FirstOrDefault(x => x.Key.IsAssignableFrom(childType)).Value;
        }
    }
}
