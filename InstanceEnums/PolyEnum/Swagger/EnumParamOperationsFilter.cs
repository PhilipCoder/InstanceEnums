using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using TypedEnums;

namespace InstanceEnums.PolyEnum.Swagger
{
    public class EnumParamOperationsFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.ParameterInfo?.ParameterType == null) return;

            if (HandleInstanceEnumParameter(parameter, context)) return;

            HandleEnumService(parameter, context);
        }

        private static bool HandleInstanceEnumParameter(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var enumType = EnumRegistry.GetEnumBaseType(context.ParameterInfo.ParameterType);

            if (!IsEnumType(enumType)) return false;

            var memberNames = (string[])enumType.GetMethod("GetNames", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { });
            parameter.Schema.Type = "string";
            parameter.Schema.Enum = memberNames.Select(x => (IOpenApiAny)new OpenApiString(x)).ToList();

            return true;
        }

        private static void HandleEnumService(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var enumType = EnumRegistry.GetBaseEnumTypeThatIsParentOf(context.ParameterInfo.ParameterType);

            if (enumType == null) return;

            parameter.Schema.Type = "string";

            var memberNames = (string[])enumType.GetMethod("GetNames", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { });
            parameter.Schema.Type = "string";
            parameter.Schema.Enum = memberNames.Select(x => (IOpenApiAny)new OpenApiString(x)).ToList();
        }

        private static bool IsEnumType(Type enumType)
        {
            if (enumType == null) return false;

            return enumType.IsSubclassOf(typeof(PolyEnumBase)) || !enumType.IsSubclassOf(typeof(PolyEnum<>).MakeGenericType(new Type[] { enumType }));
        }
    }
}
