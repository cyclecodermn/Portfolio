﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Queries
{
    public class AdminAddModel
    {
        public int BikeMakeId { get; set; }
        public int BikeModelId { get; set; }
        public string BikeMake { get; set; }
        public DateTime ModelAddedDate { get; set; }
    }
}
