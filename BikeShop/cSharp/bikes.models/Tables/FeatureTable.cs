using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class FeatureTable
    {
        public int FeatureId { get; set; }
        public int BikeId{ get; set; }
        public string FeatureDescription { get; set; }
    }
}
