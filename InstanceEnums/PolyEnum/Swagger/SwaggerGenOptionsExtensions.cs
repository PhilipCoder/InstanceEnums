using InstanceEnums.PolyEnum.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InstanceEnums.PolyEnum.Swagger
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddInstanceEnums(this SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var enumType in EnumRegistry.EnumMappings.Keys) 
                swaggerGenOptions.MapType(enumType, () => new OpenApiSchema { Type = "string" });

            foreach (var baseService in BuilderExtensions.ServiceTypes)
                swaggerGenOptions.MapType(baseService, () => new OpenApiSchema { Type = "string" });

            swaggerGenOptions.ParameterFilter<EnumParamOperationsFilter>();
        }
    }
}
