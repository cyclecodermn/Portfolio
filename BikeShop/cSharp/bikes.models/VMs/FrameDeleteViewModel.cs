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
        /// <summary>
        /// Contains Frame object & IEnumer BikeFrameUsed
        /// </summary>
        public BikeFrameTable Frame { get; set; }
        // The IEnumerables below are populated from the database
        // All of them can be edited by the user or will be in future versions
        public IEnumerable<BikeTable> BikeFramesUsed { get; set; }
        public IEnumerable<BikeShortItem> BikesWithFrame { get; set; }
        public string message { get; set; }

    }
}
