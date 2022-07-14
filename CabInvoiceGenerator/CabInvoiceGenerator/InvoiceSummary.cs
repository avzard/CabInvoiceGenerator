using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceSummary : Object
    {
        public int numOfRides;
        public double totalFare, averageFare;

        public InvoiceSummary(int numOfRides, double totalFare)
        {
            this.numOfRides = numOfRides;
            this.totalFare = totalFare;
            this.averageFare = totalFare / numOfRides;
        }
        //Oue Expexted Object and method returning is same but failing in test case.
        //Because Assert.AreEqual Implemented Only For Variables Not For Objects
        //in order to work on AreEqual method in UnitTest1 Class
        //we Have To Override Equals Method
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is InvoiceSummary))
            {
                return false;
            }
            InvoiceSummary inputedObject = (InvoiceSummary)obj;
            return this.numOfRides == inputedObject.numOfRides && this.totalFare == inputedObject.totalFare;
        }
        public override string ToString()
        {
            return $"Total number of rides : {this.numOfRides} \nTotalFare ={this.totalFare} \nAverageFare = {this.averageFare}";
        }
    }
}