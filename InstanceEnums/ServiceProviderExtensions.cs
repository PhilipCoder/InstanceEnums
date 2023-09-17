using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Data;

namespace InstanceEnums
{
    public static class ServiceProviderExtensions
    {
        private static ConcurrentDictionary<string, string> _serviceEnumIndex = new ConcurrentDictionary<string, string>();

        public static T GetServiceForEnum<T, E>(this ServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetServiceForEnum(typeof(T), typeof(E));
        }

        public static T GetServiceForEnum<T>(this ServiceProvider serviceProvider, Type enumType)
        {
            return (T)serviceProvider.GetServiceForEnum(typeof(T), enumType);
        }

        public static T GetServiceForEnum<T>(this ServiceProvider serviceProvider, object enumInstance)
        {
            var enumType = enumInstance.GetType().GetInterfaces().FirstOrDefault(x => x.Name == enumInstance.GetType().Name);
            if (enumType == null) throw new InvalidConstraintException("PolyEnums should always implement interfaces.");

            return (T)serviceProvider.GetServiceForEnum(typeof(T), enumType);
        }

        public static object GetServiceForEnum(this ServiceProvider serviceProvider, Type serviceType, Type enumType)
        {
            var services = serviceProvider.GetServices(serviceType);

            var serviceEnumIndexKey = $"{serviceType.FullName}{enumType.FullName}";

            return _serviceEnumIndex.ContainsKey(serviceEnumIndexKey) ? 
                GetServiceForEnumType(services, serviceEnumIndexKey) : 
                GetServiceForEnumType(serviceType, enumType, services, serviceEnumIndexKey);
        }

        private static object GetServiceForEnumType(IEnumerable<object> services, string serviceFullTypeName)
        {
            return services.FirstOrDefault(x => x.GetType().FullName == serviceFullTypeName);
        }

        private static object GetServiceForEnumType(Type serviceType, Type enumType, IEnumerable<object> services, string serviceEnumIndexKey)
        {
            var enumInterfaces = enumType.GetInterfaces();

            var parentInterface = enumInterfaces.FirstOrDefault();

            var service = services.FirstOrDefault(x => x.GetType().IsAssignableTo(enumType)) ?? services.FirstOrDefault(x => x.GetType().IsAssignableTo(parentInterface));

            if (service == null)
            {
                var exceptionMessage = parentInterface != null ?
                    $"No registered serivce found for type {serviceType.FullName} implementing enums ${enumType.FullName} or {parentInterface.GetType().FullName}." :
                    $"No registered serivce found for type {serviceType.FullName} implementing enums ${enumType.FullName}.";
                throw new KeyNotFoundException(exceptionMessage);
            }

            _serviceEnumIndex[serviceEnumIndexKey] = service.GetType().FullName;

            return service;
        }
    }
}
