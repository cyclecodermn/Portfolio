using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using bikes.models.Queries;
using bikes.models.Tables;

namespace bikes.models.VMs
{
    public class AddModelViewModel
    {
        public IEnumerable<SelectListItem> BikeMakes { get; set; }
        public IEnumerable<SelectListItem> BikeModels { get; set; }
        public IEnumerable<BikeTable> BikeModelsUsed { get; set; }
        public IEnumerable<BikeShortItem> BikesWithModel { get; set; }
        public string message { get; set; }
        public BikeMakeTable Bike { get; set; }
        public BikeModelTable NewBikeModel { get; set; }
    }
}
