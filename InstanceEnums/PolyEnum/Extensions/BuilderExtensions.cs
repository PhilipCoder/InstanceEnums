using InstanceEnums.PolyEnum.ModelBinding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums.PolyEnum.Extensions
{
    public static class BuilderExtensions
    {
        public static HashSet<Type> ServiceTypes = new HashSet<Type>();
        public static List<Tuple<Type, Type>> EnumServices = new List<Tuple<Type, Type>>();


        public static void ActivateEnums(this WebApplicationBuilder webApplicationBuilder)
        {
            foreach (var service in webApplicationBuilder.Services)
            {
                if (service.ImplementationType?.IsSubclassOf(typeof(InstanceEnumBase)) ?? false)
                {
                    EnumRegistry.RegisterEnum(service.ImplementationType);
                    continue;
                }
                var baseEnumInterface = EnumRegistry.GetBaseEnumTypeThatIsParentOf(service.ServiceType);
                if (baseEnumInterface != null)
                {
                    TypeDescriptor.AddAttributes(service.ServiceType, new TypeConverterAttribute(typeof(StringTypeConverter)));
                    ServiceTypes.Add(service.ServiceType);
                }
            }
        }
    }
}
