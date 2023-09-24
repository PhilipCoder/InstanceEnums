using InstanceEnums.PolyEnum.Extensions;
using InstanceEnums.PolyEnum.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Data;

namespace InstanceEnums
{
    public static class ServiceProviderExtensions
    {
        public static HashSet<Type> ServiceTypes = new HashSet<Type>();

        public static IServiceCollection Services = null;

        public static void RegisterEnumServiceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)  
            where TService : class where TImplementation : class, TService
        {
            Services = serviceCollection;
            if (!ServiceTypes.Contains(typeof(TService)))
            {
                TypeDescriptor.AddAttributes(typeof(TService), new TypeConverterAttribute(typeof(StringTypeConverter)));
            }
            serviceCollection.AddTransient<TService, TImplementation>();
            ServiceTypes.Add(typeof(TService));
        }

        public static T GetServiceForEnum<T, E>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetServiceForEnum(typeof(T), typeof(E));
        }

        public static object GetServiceForEnum(this IServiceProvider serviceProvider, Type serviceType, Type enumType, string enumValue)
        {
            var enumMemberType = enumType.BaseType.GetMethod("GetByName").Invoke(null, new object[] { (dynamic)enumValue });
            return serviceProvider.GetServiceForEnum(serviceType, enumMemberType.GetType());
        }

        public static object GetServiceForEnum(this IServiceProvider serviceProvider, Type serviceType, Type enumType, int enumValue)
        {
            var enumMemberType = enumType.BaseType.GetMethod("GetInstance").Invoke(null, new object[] { (dynamic)enumValue });
            return serviceProvider.GetServiceForEnum(serviceType, enumMemberType.GetType());
        }

        public static T GetServiceForEnum<T>(this IServiceProvider serviceProvider, Type enumMemberType)
        {
            return (T)serviceProvider.GetServiceForEnum(typeof(T), enumMemberType);
        }

        public static T GetServiceForEnum<T>(this IServiceProvider serviceProvider, object enumInstance)
        {
            return (T)serviceProvider.GetServiceForEnum(typeof(T), enumInstance.GetType());
        }

        public static object GetServiceForEnum(this IServiceProvider serviceProvider, Type serviceType, Type enumMemberType)
        {
            var services = serviceProvider.GetServices(serviceType);

            var enumInterfaces = enumMemberType.GetInterfaces();

            var parentInterface = enumInterfaces.FirstOrDefault(x=>x.Name == enumMemberType.Name);

            var servicesOfType = services.Where(x => parentInterface.IsAssignableFrom(x.GetType()));

            var result = servicesOfType.Count() > 1 ? servicesOfType.OrderBy(x=>x.GetType().GetInterfaceLevel(enumMemberType)).FirstOrDefault() : servicesOfType.FirstOrDefault();

            return result;
        }
    }
}
