using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceSummary
    {
        //variables
        private int numberOfRides;
        private double TotalFare;
        private double averageFare;

        /// <summary>
        /// parameter constructor for setting data
        /// Initializes a new instance of the <see cref="InvoiceSummary"/> class.
        /// </summary>
        /// <param name="numberOfRides">The number of rides.</param>
        /// <param name="totalFare">The total fare.</param>
        public InvoiceSummary(int numberOfRides, double totalFare)
        {
            //string data.
            this.numberOfRides = numberOfRides;
            this.TotalFare = totalFare;
            this.averageFare = averageFare / this.numberOfRides;
        }

        /// <summary>
        /// overriding Equals method
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is InvoiceSummary)) return false;

            InvoiceSummary inputedObject = (InvoiceSummary)obj;
            return this.numberOfRides == inputedObject.numberOfRides && this.TotalFare == inputedObject.TotalFare && this.averageFare == inputedObject.averageFare;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.numberOfRides.GetHashCode() ^ this.TotalFare.GetHashCode() ^ this.averageFare.GetHashCode();
        }
    }
}
