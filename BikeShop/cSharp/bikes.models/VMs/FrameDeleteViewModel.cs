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
    public class FrameDeleteViewModel
    {
        public BikeFrameTable Frame { get; set; }
        public IEnumerable<BikeTable> BikeFramesUsed { get; set; }
        public IEnumerable<BikeShortItem> BikesWithFrame { get; set; }
        public string message { get; set; }

    }
}
