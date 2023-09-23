using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums.PolyEnum.Extensions
{
    internal static class TypeExtensions
    {
        internal static List<Type> GetParents(this Type type)
        {
            var parentTypes = new List<Type>();
            while (type.BaseType.FullName != typeof(object).FullName && type.BaseType != null)
            {
                parentTypes.Add(type.BaseType);
                type = type.BaseType;
            }
            return parentTypes;
        }

        internal static T GetParentAttributeInstance<T>(this Type type) where T : Attribute
        {
            while (true)
            {
                var attributeInstance = type.GetCustomAttribute<T>();
                if (attributeInstance != null)
                {
                    return attributeInstance;
                }
                if (type.BaseType == null || type.FullName == typeof(object).FullName)
                    break;
                type = type.BaseType;
            }
            return null;
        }

        internal static T GetParentInterfaceAttributeInstance<T>(this Type type) where T : Attribute
        {
            var parentInterfaces = type.GetInterfaces();

            foreach (var parentInterface in parentInterfaces)
            {
                var attributeInstance = parentInterface.GetCustomAttribute<T>();
                if (attributeInstance != null)
                {
                    return attributeInstance;
                }
            }
            return null;
        }

        internal static Type GetParentWithAttributeInstance<T>(this Type type) where T : Attribute
        {
            while (type.BaseType.FullName != typeof(object).FullName && type.BaseType != null)
            {
                var attributeInstance = type.GetCustomAttribute<T>();
                if (attributeInstance != null)
                {
                    return type;
                }
                type = type.BaseType;
            }
            return null;
        }

        internal static bool HasSameName(this Type type, Type typeB)
        {
            return type.FullName == typeB.FullName;
        }


    }
}
