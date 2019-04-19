using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models.Responses
{
    public class FileLookupResponse:Response
    {
        public Order Order { get; set; }
    }
}
