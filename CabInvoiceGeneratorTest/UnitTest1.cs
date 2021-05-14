using Microsoft.VisualStudio.TestTools.UnitTesting;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    [TestClass]
    public class Tests
    {
        //InvoiceGenerator Reference
        InvoiceGenerator invoiceGenerator = null;
        [TestMethod]
        public void GivenDistanceAndTimeSouldReturnTotalFare()
        {
            //creating instance of invoiceGenerator for normal ride
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;

            //calculating Fare
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;

            //Asserting values
            Assert.AreEqual(expected, fare);
        }
        [TestMethod]
        public void GivenMultipleRidesShouldReturnInvoiceSummary()
        {
            //Creating instance of InvoiceGenerator for normal ride
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            //Generating summery for rides
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummery = new InvoiceSummary(2, 30.0);

            //Asserting Values
            Assert.AreEqual(expectedSummery, summary);
        }
    }
}
