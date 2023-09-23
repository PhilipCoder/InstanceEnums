using InstanceEnums.PolyEnum.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TypedEnums;

namespace InstanceEnums.PolyEnum.Swagger
{
    public class EnumParamOperationsFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var enumType = EnumRegistry.GetEnumType(context.ParameterInfo?.ParameterType);
            Type[] typeArgs = { enumType };
            if (enumType?.IsSubclassOf(typeof(PolyEnum<>).MakeGenericType(typeArgs)) ?? false){
                var memberNames = (string[])enumType.GetMethod("GetNames",BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { });
                parameter.Schema.Type = "string";
                parameter.Schema.Enum = memberNames.Select(x=> (IOpenApiAny)new OpenApiString(x)).ToList();

            }
        }
    }
}
