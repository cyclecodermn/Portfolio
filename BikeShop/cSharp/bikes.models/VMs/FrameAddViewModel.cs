﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using bikes.models.Tables;

namespace bikes.models.VMs
{
    public class FrameAddViewModel: IValidatableObject
    {
        // The IEnumerables below are populated from the database
        // All of them can be edited by the user or will be in future versions
        public IEnumerable<SelectListItem> BikeFrames { get; set; }
        public IEnumerable<SelectListItem> BikeColors { get; set; }

        public BikeFrameTable BikeFrame { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            return errors;
        }

    }
}
