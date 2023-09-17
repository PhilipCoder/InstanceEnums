using System.Runtime.CompilerServices;

namespace InstanceEnums
{
    public class InstanceEnumMemberAttribute : Attribute
    {
        public string Name { get; }

        public int Value { get; }

        public InstanceEnumMemberAttribute([CallerMemberName] string name = null, int value = 0)
        {
            Name = name;
            Value = value;
        }
    }
}
