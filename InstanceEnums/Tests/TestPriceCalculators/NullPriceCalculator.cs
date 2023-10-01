using InstanceEnums.testData;
using InstanceEnums.Tests.testData;
using InstanceEnums.Tests.TestInterfaces;

namespace InstanceEnums.Tests.TestPriceCalculators;

public class NullPriceCalculator : Vehicles.INullRef, IVehiclePriceCalculator
{
    public decimal GetPrice(AgeGroups.IChild ageGroup, decimal income)
    {
        return -1;
    }

    public decimal GetPrice(AgeGroups.IYoung ageGroup, decimal income)
    {
        return -1;
    }

    public decimal GetPrice(AgeGroups.IOld ageGroup, decimal income)
    {
        return -1;
    }

    public decimal GetPrice(AgeGroups.INullRef ageGroup, decimal income)
    {
        return 0;
    }
}
