using CabInvoiceGenerator;
using NUnit.Framework;
using System;

namespace CabInvoicGeneratorTest
{
    public class CabInvoiceGenUnitTest
    {
        public CabInvoiceGen generateNormalFare;
        public void Setup()
        {
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
        }
        [Test]
        public void GivenProperDistanceAndTimeShouldResturnFare()
        {
            //Arrange
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
            double expected = 160;
            int time = 10; //10*1 =10
            double distance = 15;  //15*10=150
            //Act
            double actual = generateNormalFare.CalculateFare(time, distance);
            //Assert
            Assert.AreEqual(actual, expected);
        }
        [Test]
        public void GivenImproperTimeDistanceShouldThrowException()
        {
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
            var invalidTimeException = Assert.Throws<CabInvoiceGeneratorException>(() => generateNormalFare.CalculateFare(0, 5));
            Assert.AreEqual(CabInvoiceGeneratorException.ExceptionType.INVALID_TIME, invalidTimeException.exceptionType);
            var invalidDistanceException = Assert.Throws<CabInvoiceGeneratorException>(() => generateNormalFare.CalculateFare(12, 0));
            Assert.AreEqual(CabInvoiceGeneratorException.ExceptionType.INVALID_DISTANCE, invalidDistanceException.exceptionType);
        }
        // TC2.1 - Given multiple rides should return aggregate fare
        [Test]
        public void GivenMultipleRidesReturnAggregateFare()
        {
            //Arrange
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
            double actual, expected = 375;  //215+160 = 375/-
            int time = 10; //10*1 =10
            double distance = 15;  //15*10=150
            Ride[] cabRides = new Ride[]
            {
                new Ride(10, 15), //160
                new Ride(15, 20)  //15*1+20*10=215
            };

            //Act
            actual = generateNormalFare.CalculateAgreegateFare(cabRides);
            //Assert
            Assert.AreEqual(actual, expected);
        }

        // TC2.2 - given no rides return custom exception
        [Test]
        public void GivenNoRidesReturnCustomException()
        {
            //Arrange
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
            Ride[] cabRides = { };
            //Act
            var nullRidesException = Assert.Throws<CabInvoiceGeneratorException>(() => generateNormalFare.CalculateAgreegateFare(cabRides));
            //Assert
            Assert.AreEqual(CabInvoiceGeneratorException.ExceptionType.NULL_RIDES, nullRidesException.exceptionType);
        }
    }
}