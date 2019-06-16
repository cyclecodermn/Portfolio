using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class BikeModelTable
    {
    public int BikeModelId { get; set; }
    public string BikeModelName { get; set; }
    public DateTime ModelAddedDate { get; set; }

    }
}
