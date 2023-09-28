using TypedEnums;

namespace InstanceEnums.Test.Web.Enums
{
    public class AgeGroups : InstanceEnum<AgeGroups>
    {
        public interface IAdult : IAgeGroup { }

        public interface ITeen : IAgeGroup { }

        public interface IChild : IAgeGroup { }

        public interface IToddler : IAgeGroup { }

        public interface IAgeGroup { }
    }
}
