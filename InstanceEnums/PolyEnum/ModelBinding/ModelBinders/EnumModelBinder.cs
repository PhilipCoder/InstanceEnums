using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypedEnums;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinders
{//https://localhost:7116/workflow/1
    public class EnumModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var enumType = EnumRegistry.GetEnumType(bindingContext.ModelType);

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (enumType == null || valueProviderResult == ValueProviderResult.None || !enumType.IsSubclassOf(typeof(PolyEnumBase)))
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (int.TryParse(value, out var enumNumber))
            {
                var fromIntResult = enumType.BaseType.GetMethod("GetInstance").Invoke(null, new object[]{ (dynamic)enumNumber});
                bindingContext.Result = ModelBindingResult.Success(fromIntResult);
                return Task.CompletedTask;
            }

            var fromStringResult = enumType.GetMethod("GetByName", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Invoke(null, new object[] { (dynamic)value });
            bindingContext.Result = ModelBindingResult.Success(fromStringResult);
            return Task.CompletedTask;

        }
    }
}
