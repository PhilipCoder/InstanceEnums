using InstanceEnums.PolyEnum;
using InstanceEnums.PolyEnum.ModelBinding;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace InstanceEnums;

public static class EnumRegistry
{
    internal static ConcurrentDictionary<Type, Type> _enumMappings = new ConcurrentDictionary<Type, Type>();

    internal static ConcurrentDictionary<string, Type> _serviceEnumMappings = new ConcurrentDictionary<string, Type>();

    public static void RegisterEnum<EnumType, BaseInterfaceType>()
    {
        RegisterEnum(typeof(EnumType), typeof(BaseInterfaceType));
    }

    internal static void RegisterEnum(Type enumType, Type baseInterfaceType)
    {
        if (!baseInterfaceType.IsInterface)
            throw new InvalidOperationException($"Enum members must be interfaces. The type is not an interface: {baseInterfaceType.FullName}");

        if (enumType != null)
            TypeDescriptor.AddAttributes(baseInterfaceType, new TypeConverterAttribute(typeof(StringTypeConverter)));

        _enumMappings.TryAdd(baseInterfaceType, enumType);
    }

    internal static Type GetAndRegisterEnumForService(Type serviceType)
    {
        if (serviceType != null && _enumMappings.TryGetValue(serviceType, out var enumType))
        {
            return enumType;
        }

        Type enumClassType = GetTypeParentEnum(serviceType);

        if (enumClassType == null)
        {
            return null;
        }

        var enumBaseType = enumClassType.GetNestedTypes().FirstOrDefault(x => x.GetInterfaces().Length == 0);

        if (enumBaseType == null)
            throw new TypeInitializationException(enumClassType.FullName, new Exception("Enum classes should contain one nested interface that is not derived from any base interface."));

        RegisterEnum(enumClassType, enumBaseType);

        return enumClassType;
    }

    internal static Type GetTypeParentEnum(Type serviceType)
    {
        var serviceInterfaces = serviceType.GetInterfaces();

        var enumClassType = serviceInterfaces.FirstOrDefault(x => IsEnumMember(x))?.DeclaringType;
        return enumClassType;
    }

    internal static bool IsEnumMember(Type x)
    {
        return x.DeclaringType?.IsSubclassOf(typeof(InstanceEnumBase)) ?? false;
    }

    internal static Type GetEnumBaseType(Type baseInterfaceType)
    {
        if (baseInterfaceType != null && _enumMappings.TryGetValue(baseInterfaceType, out var enumType))
        {
            return enumType;
        }
        return RegisterFromBaseInterfaceType(baseInterfaceType);
    }

    internal static Type GetEnumType(Type baseInterfaceType)
    {
        return _enumMappings.FirstOrDefault(x => x.Key == baseInterfaceType).Value;
    }

    internal static Type GetEnumTypeThatIsParentOf(Type childType)
    {
        return _enumMappings.FirstOrDefault(x => x.Key.IsAssignableFrom(childType)).Value;
    }

    internal static Type GetBaseEnumTypeThatIsParentOf(Type childType)
    {
        if (_serviceEnumMappings.ContainsKey(childType.FullName))
            return _serviceEnumMappings[childType.FullName];

        var baseEnumType = childType.GetInterfaces().FirstOrDefault(x => x.DeclaringType?.IsSubclassOf(typeof(InstanceEnumBase)) ?? false);
        if (baseEnumType == null)
            return null;
        _serviceEnumMappings.TryAdd(baseEnumType.FullName, baseEnumType);
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
