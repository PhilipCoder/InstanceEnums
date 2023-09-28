using InstanceEnums.testData;
using InstanceEnums.Tests.TestClasses;
using InstanceEnums.Tests.testData;
using InstanceEnums.Tests.TestInterfaces;
using InstanceEnums.Tests.TestPriceCalculators;
using Microsoft.Extensions.DependencyInjection;
using TypedEnums;
using Xunit;

namespace InstanceEnums.Tests
{
    public class BasicFeatures
    {
        [Fact]
        public void TestToInteger()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var truckInstance = Vehicles.Get<Vehicles.ITruck>();
            var oldInstance = AgeGroups.Get<AgeGroups.IOld>();

            int oldValue = Convert.ToInt32(oldInstance);
            int truckValue = Convert.ToInt32(truckInstance);

            Assert.Equal(3, Convert.ToInt32(oldValue));
            Assert.Equal(2, Convert.ToInt32(truckValue));
        }

        [Fact]
        public void TestToString()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var truckInstance = Vehicles.Get<Vehicles.ITruck>();
            var oldInstance = AgeGroups.Get<AgeGroups.IOld>();

            Assert.Equal("ITruck", truckInstance.ToString());
            Assert.Equal("IOld", oldInstance.ToString());
        }

        [Fact]
        public void TestFromInt()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            Assert.Equal(Convert.ToInt32(Vehicles.Get(0)), Convert.ToInt32(Vehicles.Get<Vehicles.ICar>()));
            Assert.Equal(Convert.ToInt32(Vehicles.Get(1)), Convert.ToInt32(Vehicles.Get<Vehicles.IBike>()));
            Assert.Equal(Convert.ToInt32(Vehicles.Get(2)), Convert.ToInt32(Vehicles.Get<Vehicles.ITruck>()));
        }

        [Fact]
        public void TestFromString()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            Assert.Equal(Vehicles.Get("ICar"), (TypedEnumMember)Vehicles.Get<Vehicles.ICar>());
            Assert.Equal(Vehicles.Get("IBike"), (TypedEnumMember)Vehicles.Get<Vehicles.IBike>());
            Assert.Equal(Vehicles.Get("ITruck"), (TypedEnumMember)Vehicles.Get<Vehicles.ITruck>());
        }

        [Fact]
        public void TestOverloading()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var valueCalculator = new ValueCalculator();

            Assert.Equal("$1400", valueCalculator.CalculateCost(Vehicles.Get<Vehicles.ICar>()));
            Assert.Equal("$0400", valueCalculator.CalculateCost(Vehicles.Get<Vehicles.IBike>()));
            Assert.Equal("Not sold", valueCalculator.CalculateCost(Vehicles.Get<Vehicles.ITruck>()));
            Assert.Equal("Not sold", valueCalculator.CalculateCost(Vehicles.Get<Vehicles.IVehicle>()));
        }

        [Fact]
        public void TestOverloadingDynamic()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var valueCalculator = new ValueCalculator();

            string getValues(Vehicles.IVehicle vehicle)
            {
                return valueCalculator.CalculateCost((dynamic)vehicle);
            }

            Assert.Equal("$1400", getValues(Vehicles.Get<Vehicles.ICar>()));
            Assert.Equal("$0400", getValues(Vehicles.Get<Vehicles.IBike>()));
            Assert.Equal("Not sold", getValues(Vehicles.Get<Vehicles.ITruck>()));
            Assert.Equal("Not sold", getValues(Vehicles.Get<Vehicles.IVehicle>()));
        }

        [Fact]
        public void TestDIInterface()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var services = new ServiceCollection();
            services.AddTransient<IVehiclePriceCalculator, VehiclePriceCalculator>();
            services.AddTransient<IVehiclePriceCalculator, NullPriceCalculator>();
            services.AddTransient<IVehiclePriceCalculator, TruckPriceCalculator>();


            var provider = services.BuildServiceProvider();

            int truckEnumValue = 2,
                childEnumValue = 1,
                oldEnumValue = 3;

            var priceCalculator = provider.GetServiceForEnum<IVehiclePriceCalculator>(Vehicles.Get(truckEnumValue));
            var youngPrice = priceCalculator.GetPrice((dynamic)AgeGroups.Get(childEnumValue), 200);
            var oldPrice = priceCalculator.GetPrice((dynamic)AgeGroups.Get(oldEnumValue), 200);

            Assert.Equal(youngPrice, 3200);
            Assert.Equal(oldPrice, 6200);
        }

        [Fact]
        public void TestDINullInterface()
        {
            EnumRegistry.RegisterEnum<Vehicles, Vehicles.IVehicle>();
            var services = new ServiceCollection();
            services.AddTransient<IVehiclePriceCalculator, VehiclePriceCalculator>();
            services.AddTransient<IVehiclePriceCalculator, NullPriceCalculator>();
            services.AddTransient<IVehiclePriceCalculator, TruckPriceCalculator>();


            var provider = services.BuildServiceProvider();

            int? nullEnumValue = null,
                childEnumValue = 1;

            var priceCalculator = provider.GetServiceForEnum<IVehiclePriceCalculator>(Vehicles.Get(nullEnumValue));
            var youngPrice = priceCalculator.GetPrice((dynamic)AgeGroups.Get(childEnumValue), 200);
            var nullPrice = priceCalculator.GetPrice((dynamic)AgeGroups.Get(nullEnumValue), 200);

            Assert.Equal(youngPrice, -1);
            Assert.Equal(nullPrice, 0);
        }
    }
}