namespace InstanceEnums.PolyEnum.Extensions;

internal static class TypeExtensions
{
    internal static int GetInterfaceLevel(this Type targetType, Type interfaceType)
    {
        var interfaces = targetType.GetInterfaces();
        int count = 0;
        foreach (var inter in interfaces)
        {
            if (inter == interfaceType)
            {
                return count;
            }
            count++;
        }
        return count;
    }
}
