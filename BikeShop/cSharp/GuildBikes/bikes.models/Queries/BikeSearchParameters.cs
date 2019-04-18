using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Queries
{
    /// <summary>
    /// Params are MinPrice,MaxPrice,MinYear,MaxYear,MakeModelOrYr,StateId
    /// </summary>
    public class BikeSearchParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public bool? IsNew { get; set; }
        public string MakeModelOrYr { get; set; }
    }
}
