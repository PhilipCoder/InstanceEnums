using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums.PolyEnum.Swagger
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddInstanceEnums(this SwaggerGenOptions swaggerGenOptions)
        {
            EnumRegistry.EnumMappings.Keys.ToList().ForEach(enumType => {
                swaggerGenOptions.MapType(enumType, () => new OpenApiSchema { Type = "string" });
            });
            
            swaggerGenOptions.ParameterFilter<EnumParamOperationsFilter>();
        }
    }
}
