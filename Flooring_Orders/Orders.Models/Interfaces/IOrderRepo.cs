using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models.Interfaces
{
    public interface IOrderRepo
    {
        OrdrLkpResponses LoadOrders(DateTime FileDate);
        void SaveOrders(DateTime FileDate, List<Order> allorders);
        void SaveOrder(Order newOrder);
        Order LoadOrder(DateTime DateLookup, int OrderNumber);
    }
}
