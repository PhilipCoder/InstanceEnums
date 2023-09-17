using InstanceEnums.testData;

namespace InstanceEnums.Tests.TestInterfaces
{
    public interface IVehiclePriceCalculator
    {
        decimal GetPrice(AgeGroups.IChild ageGroup, decimal income);

        decimal GetPrice(AgeGroups.IYoung ageGroup, decimal income);

        decimal GetPrice(AgeGroups.IOld ageGroup, decimal income);

        decimal GetPrice(AgeGroups.INullRef ageGroup, decimal income);
    }
}
