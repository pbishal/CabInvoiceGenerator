using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        //Variable
        RideType rideType;
        private RideRepository rideRepository;

        //Constants
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        /// <summary>
        /// Constructure to create rideRepository instance
        /// Initializes a new instance of the <see cref="InvoiceGenerator"/> class.
        /// </summary>
        /// <param name="rideType">Type of the ride.</param>
        /// <exception cref="CabInvoiceGenerator.CabInvoiceException">Invalid Ride Type</exception>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                //If ride type is premium then rates set for premium else for normal
                if(rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if(rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch(CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }

        /// <summary>
        /// Calculates the fare.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceGenerator.CabInvoiceException">
        /// Invalid Ride Type
        /// or
        /// Invalid Distance
        /// or
        /// Invalid Ride Time
        /// </exception>
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                //Calculating totalfare.
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch(CabInvoiceException)
            {
                if(rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Ride Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }

        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                //Calculating total fare for all rides.
                foreach(Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch(CabInvoiceException)
            {
                if(rides==null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }

        /// <summary>
        /// Function to Add Rides for UserId
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="rides">The rides.</param>
        /// <exception cref="CabInvoiceException">Rides are null</exception>
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                //Adding ride to the specified user
                rideRepository.AddRide(userId, rides);
            }
            catch(CabInvoiceException)
            {
                if(rides==null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
                }
            }
        }

        public InvoiceSummary GetInvoiceSummery(string userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch(CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid UserID");
            }
        }
    }
}
