using Orders.BLL;
using Orders.Data;
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
    public class EditOrderWorkflow

    {
        //I'm going to add the 5 lines of code below to every workflow, to give them 
        //access to all methods in manager--like SaveOrder, SaveOrders, and more.
        private OrderManager _manager;
        private List<Order> _orders;
        public EditOrderWorkflow()
        {
            _manager = OrderManagerFactory.Create();
            //_orders = _manager.LoadOrders();
        }

        public void Execute()
        {
            //OrderManager manager = OrderManagerFactory.Create();

            CommonIO.ShowHeader("Edit an order");
            OrderLookupResponse response = ConsoleIO.AskForExistingOrder();

            if (response.Success)
            {
                EditOrderIO.ShowEditInstructions();
                GetEdits(response.Order);
                SaveOrderToList(response.Order);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                CommonIO.errMsg(response.Message);
            }
        }

        public void SaveOrderToList(Order newOrder)
        {
            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = _manager.LoadOrders(newOrder.OrderDate);
            String userConfirm = ConsoleIO.RequireYorN("Would you like to save this order? (y=yes, n=no)");
            _orders = AllOrders.Orders;

            if (userConfirm == "Y")
            {
                int orderNumToReplace = newOrder.OrderNumber;
                Order OldOrder = _orders.FirstOrDefault(o => o.OrderNumber == newOrder.OrderNumber);

                int indexToReplace = (_orders.IndexOf(OldOrder));

                _orders[indexToReplace] = newOrder;
                _manager.SaveOrders(newOrder.OrderDate, _orders);

                userConfirm = userConfirm.ToUpper();
                CommonIO.MessageToUserInBlue($"New order ID {newOrder.OrderNumber} has been saved.");
                CommonIO.Continue();
            }
            else if (userConfirm == "N")
            {
                CommonIO.MessageToUserInBlue($"New order ID {newOrder.OrderNumber} was NOT saved.");
                CommonIO.Continue();
            }
        }

        public Order GetEdits(Order OrderToEdit)
        {
            //Order EditedOrder = new Order();

            // Edit Customer Name
            Console.Write($"Customer Name ({OrderToEdit.CustomerName}), ");
            CommonIO.MessageToUserInBlue("press Enter for no change.");
            String newCustomerName = ConsoleIO.AskForNewCustomerName();

            if (newCustomerName == "")
            { Console.WriteLine("Name Unchanged\n"); }
            else
            { OrderToEdit.CustomerName = newCustomerName; }

            // Edit State
            Console.WriteLine($"Order State ({OrderToEdit.State}):");
            CommonIO.MessageToUserInBlue("  0. No change");
            String newState = ConsoleIO.AskForNewStateAbbr();

            if (newState != null)
            { Console.WriteLine("State Unchanged"); }
            else
            { OrderToEdit.State = newState; }

            // Edit Product
            Console.WriteLine($"Product ({OrderToEdit.ProductType}):");
            CommonIO.MessageToUserInBlue("  0. No change");
            Product EditedProduct = ConsoleIO.AskForNewProduct();

            if (newState == null)
            { Console.WriteLine("Product Unchanged"); }
            else
            { OrderToEdit.State = newState; }

            // Edit Area
            Console.WriteLine($"Area ({OrderToEdit.Area}), ");
            CommonIO.MessageToUserInBlue("Enter 0 for no change.");

            //Decimal editedArea = ConsoleIO.AskForArea();

            decimal editedArea = 0;
            
            int min = 100, max = 100000;
            bool intCk = false;
            do
            {

                intCk = decimal.TryParse(Console.ReadLine(), out editedArea);

                if (!intCk)
                {
                    editedArea = 1;
                }
                if (editedArea == 0)
                {
                    CommonIO.MessageToUserInBlue("Area unchanged.");
                    break;
                }
                else if (editedArea < min)
                {
                    CommonIO.MessageToUserInBlue($"Please enter a number between {min} and {max}");
                    //CommonIO.Continue();
                }

            } while (editedArea < min || editedArea>max);

            if (editedArea != 0)
            { OrderToEdit.Area = editedArea; }

            CommonIO.MessageToUserInBlue("Editing order is complete. Next, you'll view the edited order and decide if you want to save it.");
            CommonIO.Continue();
            Console.Clear();
            ConsoleIO.DisplayOrderDetails(OrderToEdit);

            //•	CustomerName, State, ProductType, Area
            return OrderToEdit;
        }
    }
}
