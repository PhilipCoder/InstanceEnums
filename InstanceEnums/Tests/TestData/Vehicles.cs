using TypedEnums;

namespace InstanceEnums.Tests.testData;

public record Vehicles : InstanceEnum<Vehicles>
{
    public interface ICar : IVehicle { }

    public interface IBike : IVehicle { }

    public interface ITruck : IVehicle { }

    public interface IBus : IVehicle { }

    public interface INullRef : IVehicle { }

    public interface IVehicle { }

}
