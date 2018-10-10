using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Calculator.Source;
using TUI.Gasoline.Source;
using TUI.Transportations.Air.Source;
using TUI.Transportations.Source;

namespace TUI.Gasoline.Calculator.Test.Source
{
    [TestFixture]
    class CalculatorTest
    {
        //average factor
        [TestCase(10,0.1,1,1,1)]
        [TestCase(20, 10, 1, 1, 200)]
        public void Should_get_factor_when_having_transportaton_device(
            Double weight, 
            Double engineFactor, 
            Double speedAverage, 
            Double gasolineFactor,
            Double expectedResult)
        {
            // Arrange
            var deviceMock = new Mock<ITransportationDeviceKind>();
            deviceMock.Setup(p => p.Weight).Returns(weight);
            deviceMock.Setup(p => p.EngineFactor).Returns(engineFactor);
            deviceMock.Setup(p => p.KmsHourSpeedAverage).Returns(speedAverage);
            var device = deviceMock.Object;

            var gazMock = new Mock<IGasKind>();
            gazMock.Setup(p => p.GasConsumptionCoefficient).Returns(gasolineFactor);
            var gaz = gazMock.Object;

            // Act
            var result = device.GetAverageCoefficient(gaz);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        //average factor
        [Test]
        public void Should_ThrowNullExecption_When_device_is_null()
        {
            var gazMock = new Mock<IGasKind>();
            gazMock.Setup(p => p.GasConsumptionCoefficient).Returns(1);
            var gaz = gazMock.Object;

            Assert.Throws<NullReferenceException>(() => ConsumptionCalculator.GetAverageCoefficient(null, gaz));
        }

        //liter average
        [TestCase(20, 10, 1, 1, 20, 4000)]
        [TestCase(20, 10, 1, 1, 10, 2000)]
        public void Should_get_consumption_when_device_gasoline_and_distance(Double weight,
            Double engineFactor,
            Double speedAverage,
            Double gasolineFactor,
            Double distance,
            Double expectedResult)
        {
            // Arrange
            var deviceMock = new Mock<ITransportationDeviceKind>();
            deviceMock.Setup(p => p.Weight).Returns(weight);
            deviceMock.Setup(p => p.EngineFactor).Returns(engineFactor);
            deviceMock.Setup(p => p.KmsHourSpeedAverage).Returns(speedAverage);
            var device = deviceMock.Object;

            var gazMock = new Mock<IGasKind>();
            gazMock.Setup(p => p.GasConsumptionCoefficient).Returns(gasolineFactor);
            var gaz = gazMock.Object;

            // Act
            var result = device.GetAverageConsumption(gaz, distance);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Should_ThrowInvalidParametersException_When_Device_is_null_In_Average()
        {
            Assert.Throws<NullReferenceException>(() => ConsumptionCalculator.GetAverageConsumption(null,null, 1000));
        }

        [TestCase(20, 10, 1,1,1, 200)]
        public void Should_get_starting_factor_when_having_device(Double weight,
            Double requiredDistance,
            Double startingFactor,
            Double engineFactor,
            Double averageSpeed,
            Double expectedResult)
        {
            var deviceMock = new Mock<ITransportationDeviceKind>();
            deviceMock.Setup(p => p.Weight).Returns(weight);
            deviceMock.Setup(p => p.RequiredStartingDistance).Returns(requiredDistance);
            deviceMock.Setup(p => p.StartingEffort).Returns(startingFactor);
            deviceMock.Setup(p => p.EngineFactor).Returns(engineFactor);
            deviceMock.Setup(p => p.KmsHourSpeedAverage).Returns(averageSpeed);
            var device = deviceMock.Object;

            var result = device.GetStartingCoefficient();
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase(20, 10, 1, 10, 1, 1, 2000)]
        public void Should_get_starting_consumption_when_having_device(Double weight,
            Double requiredStartingDistance,
            Double gasolineFactor,
            Double startingFactor,
            Double engineFactor,
            Double speedAverage,
            Double expectedResult)
        {
            var deviceMock = new Mock<ITransportationDeviceKind>();
            deviceMock.Setup(p => p.Weight).Returns(weight);
            deviceMock.Setup(p => p.RequiredStartingDistance).Returns(requiredStartingDistance);
            deviceMock.Setup(p => p.StartingEffort).Returns(startingFactor);
            deviceMock.Setup(p => p.EngineFactor).Returns(engineFactor);
            deviceMock.Setup(p => p.KmsHourSpeedAverage).Returns(speedAverage);
            var device = deviceMock.Object;

            var gazMock = new Mock<IGasKind>();
            gazMock.Setup(p => p.GasConsumptionCoefficient).Returns(gasolineFactor);
            var gaz = gazMock.Object;

            var result = device.GetStartingConsumption(gaz);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Should_ThrowInvalidParametersException_When_Device_is_null_In_Starting()
        {
            Assert.Throws<NullReferenceException>(() => ConsumptionCalculator.GetStartingConsumption(null,null));
        }

        [TestCase(20,1,1,1,1,1,1,40)]
        public void SHOULD_get_total_consumption_WHEN_having_device_and_gas(Double weight,
            Double engineFactor,
            Double speedAverage,
            Double distance,
            Double gasolineFactor,
            Double requiredStartingDistance,
            Double startingFactor,
            Double expectedResult)
        {
            var deviceMock = new Mock<ITransportationDeviceKind>();
            deviceMock.Setup(p => p.Weight).Returns(weight);
            deviceMock.Setup(p => p.RequiredStartingDistance).Returns(requiredStartingDistance);
            deviceMock.Setup(p => p.StartingEffort).Returns(startingFactor);
            deviceMock.Setup(p => p.KmsHourSpeedAverage).Returns(speedAverage);
            deviceMock.Setup(p => p.EngineFactor).Returns(engineFactor);
            var device = deviceMock.Object;

            var gazMock = new Mock<IGasKind>();
            gazMock.Setup(p => p.GasConsumptionCoefficient).Returns(gasolineFactor);
            var gaz = gazMock.Object;

            var result = device.GetTotalConsumption(gaz, distance);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
