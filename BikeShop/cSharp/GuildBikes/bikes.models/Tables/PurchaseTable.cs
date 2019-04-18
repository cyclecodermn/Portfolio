using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class PurchasedTable
    {
        public int PurchaseSaleId { get; set; }
        public int BikeId { get; set; }
        public decimal PurchasePrice { get; set; }
        public string PurchCustFirst { get; set; }
        public string PurchCustLast { get; set; }
        public string PurchCustPhone { get; set; }
        public string PurchCustAddress1 { get; set; }
        public string PurchCustAddress2 { get; set; }
        public string PurchCustCity { get; set; }
        public string PurchCustState { get; set; }
        public string PurchCustPostCode { get; set; }
        public string PurchFinType { get; set; }
    }
}
