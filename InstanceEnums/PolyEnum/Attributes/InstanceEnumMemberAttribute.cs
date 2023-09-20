using System.Runtime.CompilerServices;

namespace InstanceEnums
{
    public class InstanceEnumMemberAttribute : Attribute
    {
        public string Name { get; }

        public int Value { get; }

        public Type EnumType { get; set; }

        public InstanceEnumMemberAttribute([CallerMemberName] string name = null, int value = 0, Type enumType = null)
        {
            Name = name;
            Value = value;
            EnumType = enumType;
        }
    }
}
