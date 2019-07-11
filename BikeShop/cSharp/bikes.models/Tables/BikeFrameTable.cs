using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class BikeFrameTable
    {
        public int BikeFrameId { get; set; }
        [FrameDoesNotExist(ErrorMessage = "You cannot book appointments on the weekend")]
        [Required(ErrorMessage = "Please enter the name of the frame.")]
        public string BikeFrame { get; set; }
    }
}
