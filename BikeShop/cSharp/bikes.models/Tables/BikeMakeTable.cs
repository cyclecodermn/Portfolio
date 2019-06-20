using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class BikeMakeTable
    {
        public int BikeMakeNameId { get; set; }
        public int BikeModelId { get; set; }
        public string BikeMakeName { get; set; }
        public DateTime MakeAddedDate { get; set; }

    }
}
