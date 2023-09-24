using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstanceEnums.PolyEnum.Helpers
{
    internal static class NullHelpers
    {
        public static bool HasValue<T>(T value) where T : class
        {
            return value != null;
        }

        public static T IfNotNull<T>(Func<T> tocheck,params object[] values) where T : class
        {
            if (values.Any(x => x == null))
            {
                return (T)tocheck.Method.ReturnType.GetDefaultValue();
            }
            return (T)tocheck.DynamicInvoke();
        }

        internal static T IfNotNull<T>(this object value, Func<T> tocheck)
        {
            if (value == null)
            {
                return (T)tocheck.Method.ReturnType.GetDefaultValue();
            }
            if (tocheck.Method.GetParameters().Length > 0)
            {
                return (T)tocheck.DynamicInvoke(value);
            }
            return (T)tocheck.DynamicInvoke();
        }
    }
}
