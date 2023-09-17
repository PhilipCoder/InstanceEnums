using InstanceEnums.testData;
using InstanceEnums.Tests.testData;
using InstanceEnums.Tests.TestInterfaces;

namespace InstanceEnums.Tests.TestPriceCalculators
{
    public class TruckPriceCalculator : Vehicles.ITruck, IVehiclePriceCalculator
    {
        private decimal _basePrice = 2000;
        public decimal GetPrice(AgeGroups.IChild ageGroup, decimal income)
        {
            return _basePrice * 1.5m + income;
        }

        public decimal GetPrice(AgeGroups.IYoung ageGroup, decimal income)
        {
            return _basePrice * 2 + income;
        }

        public decimal GetPrice(AgeGroups.IOld ageGroup, decimal income)
        {
            return _basePrice * 3 + income;
        }

        public decimal GetPrice(AgeGroups.INullRef ageGroup, decimal income)
        {
            return 0;
        }
    }
}
