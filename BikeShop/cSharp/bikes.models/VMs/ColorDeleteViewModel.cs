﻿using System;
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
    public class ColorDeleteViewModel
    {
        /// <summary>
        /// Contains Color object & IEnumer BikeColorUsed
        /// </summary>
        public BikeColorTable Color { get; set; }
        // The IEnumerables below are populated from the database
        // All of them can be edited by the user or will be in future versions
        public IEnumerable<BikeTable> BikeColorsUsed { get; set; }
        public IEnumerable<BikeShortItem> BikesWithColor { get; set; }
        public string message { get; set; }

    }
}
