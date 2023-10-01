using InstanceEnums.PolyEnum.ModelBinding;
using InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders;
using InstanceEnums.PolyEnum.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace InstanceEnums.PolyEnum.Extensions;

public static class BuilderExtensions
{
    public static HashSet<Type> ServiceTypes = new HashSet<Type>();

    public static void ActivateEnums(this WebApplicationBuilder webApplicationBuilder)
    {
        var swaggerGen = webApplicationBuilder.Services.FirstOrDefault(x => x.ServiceType == typeof(SwaggerGeneratorOptions));

        webApplicationBuilder.Services.Configure<MvcOptions>(options => { options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider()); });

        if (swaggerGen == null)
            return;

        RegisterParameterEnums(webApplicationBuilder);

        RegisterServiceEnums(webApplicationBuilder);
    }

    private static void RegisterParameterEnums(WebApplicationBuilder webApplicationBuilder)
    {
        foreach (var enumType in GetEnumAssemblyParameters())
        {
            EnumRegistry.RegisterEnum(enumType.DeclaringType, enumType);
        }

        webApplicationBuilder.Services.Configure<SwaggerGenOptions>((c) => { c.AddInstanceEnums(); });
    }

    private static void RegisterServiceEnums(WebApplicationBuilder webApplicationBuilder)
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

    private static IEnumerable<Type> GetEnumAssemblyParameters()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(assemblyType => typeof(ControllerBase).IsAssignableFrom(assemblyType) && typeof(ControllerBase) != assemblyType)
                        .SelectMany(x => x.GetMethods())
                        .SelectMany(x => x.GetParameters().Where(x => EnumRegistry.IsEnumMember(x.ParameterType)))
                        .Select(x => x.ParameterType);
    }
}
