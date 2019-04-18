using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.VMs
{
    public class DropDownPriceYear
    {
        public int YearId { get; set; }
        public int BikeYear { get; set; }
        public int PriceId { get; set; }
        public decimal Price { get; set; }
    }
}
