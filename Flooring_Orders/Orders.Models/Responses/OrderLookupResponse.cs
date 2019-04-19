using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models.Responses
{
    public class OrderLookupResponse:Response
    {
        public Order Order { get; set; }
    }

    public class OrdrLkpResponses : Response
    {
        public List <Order> Orders { get; set; }
    }


}
