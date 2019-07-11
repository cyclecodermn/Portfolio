using bikes.data.ADO.AdoUtils;
using bikes.models.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Attributes
{
    class FrameDoesNotExist : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                BikeSearchParameters searchParam = new BikeSearchParameters();
                searchParam.MakeModelOrYr = value.ToString();

                SearchAll BikeSearch = new SearchAll();
                IEnumerable<BikeShortItem> BikesWithFrame = BikeSearch.Search2(searchParam);

                if (BikesWithFrame.Count() > 0)
                {
                    return false;
                }

                {
                    return true;
                }
            }

            return false;
        }
    }
}
