using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinders
{
    public class EnumInstanceModelBinder : IModelBinder
    {
        private IServiceProvider _serviceProvider { get; }

        public EnumInstanceModelBinder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)  throw new ArgumentNullException(nameof(bindingContext));

            var enumBaseType = EnumRegistry.GetEnumForService(bindingContext.ModelType);

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (enumBaseType == null) return Task.CompletedTask;

            var value = valueProviderResult.FirstValue;

            if (int.TryParse(value, out int modelValue))
            {
                bindingContext.Result = ModelBindingResult.Success(_serviceProvider.GetServiceForEnum(bindingContext.ModelType, enumBaseType, modelValue));
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(_serviceProvider.GetServiceForEnum(bindingContext.ModelType, enumBaseType, value));
            return Task.CompletedTask;
        }
    }
}