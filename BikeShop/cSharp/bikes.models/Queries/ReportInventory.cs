using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Queries
{
    class ReportInventory
    {
        public int BikeId { get; set; }
        public string BikeMake { get; set; }
        public string BikeModel { get; set; }
        public decimal BikeListPrice { get; set; }
    }
}
