using InstanceEnums.PolyEnum.ModelBinding.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using TypedEnums;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders
{
    public class EnumModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var enumType = context.Metadata.ModelType.GetCustomAttribute<InstanceEnumMemberAttribute>()?.EnumType;

            if (enumType != null && enumType.IsSubclassOf(typeof(PolyEnumBase)))
            {
                return new EnumModelBinder();
            }

            return null;
        }
    }
}
