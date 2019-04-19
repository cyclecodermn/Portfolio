using Orders.BLL;
using Orders.Models;
using Orders.Models.Interfaces;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        //I'm going to add the 5 lines of code below to every workflow, to give them 
        //access to all methods in manager--like SaveOrder, SaveOrders, and more.
        private OrderManager _manager;
        private List<Order> _orders;
        public OrderLookupWorkflow()
        {
            _manager = OrderManagerFactory.Create();
            //_orders = _manager.LoadOrders();
        }
        //private List<Order> myVar = Orders;
        public void Execute()
        {
            CommonIO.ShowHeader("Lookup an order");

            List<Order> _allOrders = new List<Order>();
            DateTime OrderDate = ConsoleIO.AskForOrderDate();

            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = _manager.LoadOrders(OrderDate);

            _allOrders = AllOrders.Orders;

            Console.Clear();

            if (AllOrders.Success)
            {
                foreach (Order OneOrder in _allOrders)
                {
                    ConsoleIO.DisplayOrderDetails(OneOrder);
                }
                CommonIO.Continue();
            }
            else
            {
                CommonIO.MessageToUserInBlue(AllOrders.Message);
            }
        }
    }
}
