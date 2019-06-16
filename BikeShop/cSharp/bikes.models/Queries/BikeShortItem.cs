using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Queries
{
    public class BikeShortItem
    {
        public int BikeId { get; set; }
        public bool BikeIsNew { get; set; }
        public int BikeYear { get; set; }
        public string BikeMakeName { get; set; }
        public string BikeModelName { get; set; }
        public string BikeFrame { get; set; }
        public int BikeNumGears { get; set; }
        public int BikeCondition { get; set; }
        public string BikeSerialNum { get; set; }
        public decimal BikeMsrp { get; set; }
        public decimal BikeListPrice { get; set; }
        public string BikePictName { get; set; }
        public string BikeFrameColor { get; set; }
        public string BikeTrimColor { get; set; }
    }
}
