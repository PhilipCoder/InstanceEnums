using InstanceEnums.PolyEnum;
using InstanceEnums.PolyEnum.ModelBinding;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace InstanceEnums
{
    public static class EnumRegistry
    {
        internal static ConcurrentDictionary<Type, Type> EnumMappings = new ConcurrentDictionary<Type, Type>();
        internal static ConcurrentDictionary<string, Type> ServiceEnumMappings = new ConcurrentDictionary<string, Type>();
        public static void RegisterEnum<EnumType, BaseInterfaceType>()
        {
            RegisterEnum(typeof(EnumType), typeof(BaseInterfaceType));
        }

        private static void RegisterEnum(Type enumType, Type baseInterfaceType)
        {
            if (!baseInterfaceType.IsInterface)
                throw new InvalidOperationException($"Enum members must be interfaces. The type is not an interface: {baseInterfaceType.FullName}");
            TypeDescriptor.AddAttributes(baseInterfaceType, new TypeConverterAttribute(typeof(StringTypeConverter)));

            EnumMappings.TryAdd(baseInterfaceType, enumType);
        }

        internal static Type GetEnumBaseType(Type baseInterfaceType)
        {
            if (baseInterfaceType != null && EnumMappings.TryGetValue(baseInterfaceType, out var enumType))
            {
                return enumType;
            }
            return RegisterFromBaseInterfaceType(baseInterfaceType);
        }

        internal static Type GetEnumType(Type baseInterfaceType)
        {
            return EnumMappings.FirstOrDefault(x => x.Key == baseInterfaceType).Value;
        }

        internal static Type GetEnumTypeThatIsParentOf(Type childType)
        {
            return EnumMappings.FirstOrDefault(x => x.Key.IsAssignableFrom(childType)).Value;
        }

        internal static Type GetBaseEnumTypeThatIsParentOf(Type childType)
        {
            if (ServiceEnumMappings.ContainsKey(childType.FullName))
                return ServiceEnumMappings[childType.FullName];

            var baseEnumType = childType.GetInterfaces().FirstOrDefault(x => x.DeclaringType?.IsSubclassOf(typeof(InstanceEnumBase)) ?? false);
            if (baseEnumType == null) return null;
            ServiceEnumMappings.TryAdd(baseEnumType.FullName, baseEnumType);
            if (baseEnumType != null)
            {
                RegisterEnum(baseEnumType.DeclaringType, baseEnumType);
            }
            return baseEnumType;
        }

        internal static Type GetEnumForService(Type childType)
        {
            var baseEnumType = GetBaseEnumTypeThatIsParentOf(childType);
            return GetEnumType(baseEnumType);
        }

        internal static Type RegisterFromBaseInterfaceType(Type baseInterfaceType)
        {
            var declaringType = baseInterfaceType.DeclaringType;
            if (declaringType == null || !declaringType.IsSubclassOf(typeof(InstanceEnumBase)) || baseInterfaceType.GetInterfaces().Length > 0)
                return null;

            RegisterEnum(declaringType, baseInterfaceType);

            return declaringType;
        }

        internal static void RegisterEnum(Type enumClassType)
        {
            var enumBaseInterface = enumClassType.GetNestedTypes().FirstOrDefault(x => x.GetInterfaces().Count() == 0);
            if (enumBaseInterface == null)
                return;

            RegisterEnum(enumClassType, enumBaseInterface);
        }
    }
}
