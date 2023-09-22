using InstanceEnums.PolyEnum.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinders
{
    public class EnumInstanceModelBinder : IModelBinder
    {
        private ServiceProvider _serviceProvider { get; }

        public EnumInstanceModelBinder(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var enumBaseTypeAttribute = bindingContext.ModelType.GetParentAttributeInstance<InstanceEnumMemberAttribute>();

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (enumBaseTypeAttribute == null)
            {
                return Task.CompletedTask;
            }

            object serviceInstance = null;

            var value = valueProviderResult.FirstValue;

            if (int.TryParse(value, out int modelValue))
            {
                bindingContext.Result = ModelBindingResult.Success(_serviceProvider.GetServiceForEnum(bindingContext.ModelType, enumBaseTypeAttribute.EnumType, modelValue));
                return Task.CompletedTask;
            }
            bindingContext.Result = ModelBindingResult.Success(_serviceProvider.GetServiceForEnum(bindingContext.ModelType, enumBaseTypeAttribute.EnumType, value));
            return Task.CompletedTask;
        }
    }
}