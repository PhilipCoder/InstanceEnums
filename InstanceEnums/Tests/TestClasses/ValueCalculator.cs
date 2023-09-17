using InstanceEnums.Tests.testData;

namespace InstanceEnums.Tests.TestClasses
{
    public class ValueCalculator
    {
        public string CalculateCost(Vehicles.ICar car)
        {
            return "$1400";
        }

        public string CalculateCost(Vehicles.IBike bike)
        {
            return "$0400";
        }

        public string CalculateCost(Vehicles.IVehicle vehicle)
        {
            return "Not sold";
        }
    }
}
