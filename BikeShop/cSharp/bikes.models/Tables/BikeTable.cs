using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    /// <summary>
    /// Return only items for bike database. For example, framecolor and trimcolor are returned as int IDs. Another model, InvDetailedItem returns more.
    /// </summary>
    public class BikeTable
    {
        public string UserId { get; set; }
        public int BikeId { get; set; }
        public int BikeMakeNameId { get; set; }
        public int BikeModelId { get; set; }
        public int BikeFrameColorId { get; set; }
        public int BikeTrimColorId { get; set; }
        public int BikeFrameId { get; set; }
        public decimal BikeMsrp { get; set; }
        public decimal BikeListPrice { get; set; }
        public int BikeYear { get; set; }
        public bool BikeIsNew { get; set; }
        public int BikeCondition { get; set; }
        public int BikeNumGears { get; set; }
        public string BikeSerialNum { get; set; }
        public string BikeDescription { get; set; }
        public DateTime BikeDateAdded { get; set; }
        public string BikePictName { get; set; }
    }
}
