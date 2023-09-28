using InstanceEnums;
using InstanceEnums.PolyEnum;
using InstanceEnums.PolyEnum.ModelBinding;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace TypedEnums
{
    [TypeConverter(typeof(StringTypeConverter))]
    public abstract class InstanceEnum<T> : InstanceEnumBase where T : InstanceEnum<T>, new()
    {
        protected static InterfaceValueFactory interfaceValueFactory = new InterfaceValueFactory();

        protected static ConcurrentDictionary<string, TypedEnumMember> _instances = new ConcurrentDictionary<string, TypedEnumMember>();

        protected static ConcurrentDictionary<int, TypedEnumMember> _indexedInstances = new ConcurrentDictionary<int, TypedEnumMember>();

        protected static Type[] _nestedTypes;

        protected static T _instance;

        internal Type[] NestedTypes {
            get {
                return _nestedTypes = _nestedTypes ?? this.GetType().GetNestedTypes().Where(nestedType => nestedType.IsInterface).ToArray();
            }
        }
        public InstanceEnum() {
            PopulateEnumIndexes();

        }

        public static string[] GetNames()
        {
            Get();
            return _instances.Keys.ToArray();
        }

        public static T Get()
        {
            _instance = _instance ?? new T();
            return _instance;
        }

        public static TypedEnumMember GetInstance(int? enumValue)
        {
            Get();
            return enumValue == null ?
                _instances["INullRef"] :
                _indexedInstances[enumValue.Value];
        }

        public static E Get<E>() where E : class
        {
            var instance = Get();
            return instance.GetInstance<E>()!;
        }

        public static TypedEnumMember Get(int? enumValue)
        {
            Get();
            return enumValue == null ? 
                _instances["INullRef"] : 
                _indexedInstances[enumValue.Value];
        }

        public static TypedEnumMember Get(string enumName)
        {
            Get();
            return enumName == null ?
                _instances["INullRef"] :
                _instances[enumName];
        }

        public static TypedEnumMember GetByName(string enumName)
        {
            Get();
            return enumName == null ?
                _instances["INullRef"] :
                _instances[enumName];
        }

        protected I GetInstance<I>() where I : class
        {
            var enumInstance = GetInstance(typeof(I)) as I;

            return enumInstance == null ?
                ((_instances.ContainsKey("INullRef") ? _instances["INullRef"] : null) as I) :
                enumInstance;
        }

        protected object GetInstance(Type enumType)
        {
            return !_instances.ContainsKey(enumType.Name) ?
                ((_instances.ContainsKey("INullRef") ? _instances["INullRef"] : null) as T) :
                _instances[enumType.Name];
        }

        protected void PopulateEnumIndexes()
        {
            for (int nestedTypeIndex = 0; nestedTypeIndex < NestedTypes.Length; nestedTypeIndex++)
            {
                Type nestedType = NestedTypes[nestedTypeIndex];
                var result = interfaceValueFactory.CreateNewObject<TypedEnumMember>(nestedType);

                var enumInstanceAttribute = nestedType.GetCustomAttribute<InstanceEnumMemberAttribute>();

                result.Name = enumInstanceAttribute?.Name ?? nestedType.Name;

                result.EnumValue = enumInstanceAttribute?.Value ?? nestedTypeIndex;

                _instances[nestedType.Name] = result;
                _indexedInstances[nestedTypeIndex] = result;
            }
        }
    }
}