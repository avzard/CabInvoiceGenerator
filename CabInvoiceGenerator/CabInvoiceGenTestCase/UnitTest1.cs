using CabInvoiceGenerator;
using System;
using NUnit.Framework;
using System.Collections.Generic;

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
        /*
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
        */
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
        // TC2.1 -> TC3.1 - Given Invoice Generator Should Return The Number Of Ride, TotalFare. and AverageFare Per Ride.
        [Test]
        public void GivenInvoiceReturnNumOfRideTotalFareandAverageFare()
        {
            //Arrange
            generateNormalFare = new CabInvoiceGen(RideType.NORMAL);
            Ride[] cabRides = new Ride[]
            {
                new Ride(10, 15), //160
                new Ride(15, 20)  //15*1+20*10=215
            };
            double totalfare = 375;
            InvoiceSummary expected = new InvoiceSummary(cabRides.Length, totalfare);  //215+160 = 375/-

            //Act
            var actual = generateNormalFare.CalculateAgreegateFare(cabRides);

            //Assert
            Assert.AreEqual(actual, expected);
        }
        //TC4.1 Given User ID Should Return The Invoice Summary
        [TestCase(1, 2, 375, 10, 15, 15, 20)]
        public void GivenUserIdReturnInvoiceSummary(int userId, int cabRideCount, double totalFare, int time1, double distance1, int time2, double distance2)
        {
            RideRepository rideRepository = new RideRepository();
            Ride[] userRides = new Ride[]
            {
                new Ride(time1, distance1),
                new Ride(time2, distance2)
            };
            rideRepository.AddUserRidesToRepository(userId, userRides, RideType.NORMAL);
            List<Ride> list = new List<Ride>();
            list.AddRange(userRides);
            InvoiceSummary userInvoice = new InvoiceSummary(cabRideCount, totalFare);

            UserCabInvoiceService expected = new UserCabInvoiceService(list, userInvoice);
            UserCabInvoiceService actual = rideRepository.ReturnInvoicefromRideRepository(userId);
            Assert.AreEqual(actual.InvoiceSummary.totalFare, expected.InvoiceSummary.totalFare);
        }
    }
}