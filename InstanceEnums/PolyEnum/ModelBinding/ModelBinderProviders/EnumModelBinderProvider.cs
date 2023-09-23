﻿using InstanceEnums.PolyEnum.Extensions;
using InstanceEnums.PolyEnum.ModelBinding.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TypedEnums;

namespace InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders
{
    public class EnumModelBinderProvider : IModelBinderProvider
    {
        public EnumModelBinderProvider()
        {
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var enumType = EnumRegistry.GetEnumType(context.Metadata.ModelType);

            if (enumType != null && enumType.IsSubclassOf(typeof(PolyEnumBase)))
            {
                return new EnumModelBinder();
            }
            enumType = EnumRegistry.GetEnumType(context.Metadata.ModelType);
            if (enumType != null)
            {
                return new EnumInstanceModelBinder(context.Services);
            }

            return null;
        }
    }
}
