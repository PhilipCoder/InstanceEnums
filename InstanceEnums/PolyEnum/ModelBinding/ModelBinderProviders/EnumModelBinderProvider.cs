using InstanceEnums.PolyEnum.ModelBinding.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders
{
    public class EnumModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var enumType = EnumRegistry.GetEnumBaseType(context.Metadata.ModelType);

            if (enumType != null && enumType.IsSubclassOf(typeof(InstanceEnumBase))) return new EnumModelBinder();

            enumType = EnumRegistry.GetEnumTypeThatIsParentOf(context.Metadata.ModelType);

            return enumType != null ? new EnumInstanceModelBinder(context.Services) : null;
        }
    }
}
