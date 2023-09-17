using TypedEnums;

namespace InstanceEnums.Tests.testData
{
    public class Vehicles : PolyEnum<Vehicles>
    {
        public interface ICar : IVehicle { }

        public interface IBike : IVehicle { }

        public interface ITruck : IVehicle { }

        public interface IBus : IVehicle { }

        public interface INullRef : IVehicle { }

        public interface IVehicle { }

    }
}
