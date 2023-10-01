using TypedEnums;

namespace InstanceEnums.testData;

public record AgeGroups : InstanceEnum<AgeGroups>
{
    public interface IToddler : IAgeGroup { }

    public interface IChild : IAgeGroup { }

    public interface IYoung : IAgeGroup { }

    public interface IOld : IAgeGroup { }

    public interface IDead : IAgeGroup { }

    public interface INullRef : IAgeGroup { }

    public interface IAgeGroup { }
}
