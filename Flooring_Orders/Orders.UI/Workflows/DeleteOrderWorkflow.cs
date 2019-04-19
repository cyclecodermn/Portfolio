using Orders.BLL;
using Orders.Models;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI.Workflows
{
    public class DeleteOrderWorkflow
    {
        private OrderManager _manager;
        private List<Order> _orders;
        public DeleteOrderWorkflow()
        {
            _manager = OrderManagerFactory.Create();
            // _orders = _manager.LoadOrders();
        }

        public void Execute()
        {
            CommonIO.ShowHeader("Delete Order");
            DateTime OrderDateToDelete = ConsoleIO.AskForOrderDate();

            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = _manager.LoadOrders(OrderDateToDelete);

            if (AllOrders.Success)
            {
                _orders = AllOrders.Orders;
                int orderNumtoDelete = CommonIO.GetIntFromUser(1, 999, 0, "What order number would you like to delete?");
                DeleteOrder(OrderDateToDelete, orderNumtoDelete);
            }
            else
            {
                CommonIO.MessageToUserInBlue(AllOrders.Message);
            }

        }

        private void DeleteOrder(DateTime OrderDateToDelete, int orderNumtoDelete)
        {
            OrderLookupResponse response = _manager.LookupOrder(OrderDateToDelete, orderNumtoDelete);

            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
                string confirmYN = ConsoleIO.RequireYorN("Are you sure you want to remove this order? (y=yes, n=no)");

                do
                {
                    if (confirmYN == "Y")
                    {
                        _orders.RemoveAll(o => o.OrderNumber == response.Order.OrderNumber);
                        //_orders.Remove(_orders[indexToDelete]);
                        //TODO: Learn why .remove did not work and .removeall did work.

                        _manager.SaveOrders(OrderDateToDelete, _orders);
                        CommonIO.MessageToUserInBlue($"Order {orderNumtoDelete} was deleted.");
                    }
                    else if (confirmYN == "N")
                    {
                        CommonIO.MessageToUserInBlue($"Order {orderNumtoDelete} was NOT deleted.");
                        //CommonIO.Continue();
                    }
                    else
                    {
                        CommonIO.MessageToUserInBlue("Please enter y or n.");
                    }

                } while ((confirmYN == "Y") && (confirmYN == "N"));
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            CommonIO.Continue();
        }

    }
}