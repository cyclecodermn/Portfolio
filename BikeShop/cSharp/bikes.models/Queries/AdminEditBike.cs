using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Queries
{
    public class AdminEditBike
    {
        public bool BikeIsNew { get; set; }
        public int BikeYear { get; set; }
        public string BikeMake { get; set; }
        public string BikeModel { get; set; }
        public string BikeFrame { get; set; }
        public int BikeNumGears { get; set; }
        public int BikeCondition { get; set; }
        public string BikeSerialNum { get; set; }
        public decimal BikeSMsrp { get; set; }
        public decimal BikeListPrice { get; set; }
        public string BikePictName { get; set; }
        public int BikeFrameColorId { get; set; }
        public int BikeTrimColorId { get; set; }
    }
}
