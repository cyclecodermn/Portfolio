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
    public class ModelDeleteViewModel
    {
        /// <summary>
        /// Contains Model object & IEnumer BikeModelUsed
        /// </summary>
        public BikeModelTable Model { get; set; }
        // The IEnumerables below are populated from the database
        // All of them can be edited by the user or will be in future versions
        public IEnumerable<BikeTable> BikeModelsUsed { get; set; }
        public IEnumerable<BikeShortItem> BikesWithModel { get; set; }
        public string message { get; set; }

    }
}
