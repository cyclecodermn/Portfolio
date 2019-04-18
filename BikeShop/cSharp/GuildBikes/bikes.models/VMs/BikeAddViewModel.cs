using System;
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
    public class BikeAddViewModel: IValidatableObject
    {
        // The IEnumerables below are populated from the database
        // All of them can be edited by the user or will be in future versions
        public IEnumerable<SelectListItem> BikeMakes { get; set; }
        public IEnumerable<SelectListItem> BikeModels { get; set; }
        public IEnumerable<SelectListItem> BikeFrames { get; set; }
        public IEnumerable<SelectListItem> BikeColors { get; set; }

        // The lists below are populated in the controller since they come
        // from other tables, like color, or are numbers, like years.
        // In intial releases, the user will not be able to edit these items.
        public List<SelectListItem> BikeYearItems { get; set; }
        public List<SelectListItem> BikeGearItems { get; set; }
        public List<SelectListItem> FrameColorItems { get; set; }
        public List<SelectListItem> ConditionItems { get; set; }


        public BikeTable Bike { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Bike.BikeSerialNum))
            {
                errors.Add(new ValidationResult("Serial Number is required"));
            }

            if (string.IsNullOrEmpty(Bike.BikeDescription))
            {
                errors.Add(new ValidationResult("Description is required"));
            }

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg."));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required"));
            }

            if (Bike.BikeListPrice <= 0)
            {
                errors.Add(new ValidationResult("List price must be greater than 0"));
            }

            if (Bike.BikeMsrp <= 0)
            {
                errors.Add(new ValidationResult("MSRP must be greater than 0"));
            }

            return errors;
        }

    }
}
