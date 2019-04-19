using Orders.BLL;
using Orders.Data;
using Orders.Models;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI
{
    public class ConsoleIO

    {
        private List<Order> _orders;
        private static OrderManager _manager = OrderManagerFactory.Create();
        //Since there is a static method below, the constructor does not run.
        // Therefore, define key (global) variables/objects after the class definition.
        //private List<Order> _orders;
        public ConsoleIO()
        {
            _manager = OrderManagerFactory.Create();
            //_orders = _manager.LoadOrders(DateTime.Now);
        }

        /// <summary>
        /// Ask the user for a date in 3 steps: 1) a 2 number month, 2) a 2 number day,
        /// and 3) a 4 number year.
        /// </summary>
        /// <returns>Date entered as a DateTime object</returns>
        public static Order GetNewOrder()
        {
            //DateTime currentDateTime = DateTime.Now;

            Order NewOrder = new Order();
            NewOrder.OrderDate = AskForNewOrderDate();
            DateTime OrderDate = NewOrder.OrderDate;
            //NewOrder.OrderNumber = _manager.CountOrders(OrderDate) + 1;
            NewOrder.OrderNumber = _manager.NextOrderID(OrderDate);

            NewOrder.CustomerName = AskForNewCustomerName();
            NewOrder.State = AskForNewStateAbbr();
            Product prodFromUser = AskForNewProduct();
            NewOrder.ProductType = prodFromUser.ProductType;
            NewOrder.Area = AskForArea();

            return NewOrder;
        }

        internal static string RequireYorN(string msg = "")
        {
            if (msg != "")
            {
                CommonIO.MessageToUserInBlue(msg);
            }
            string usrResponse = "";
            do
            {
                usrResponse = Console.ReadLine().ToUpper();
                if ((usrResponse != "Y") && (usrResponse != "N"))
                {
                    Console.WriteLine("You must enter y or n.");
                }

            } while (usrResponse != "Y" && usrResponse != "N");

            return usrResponse.ToUpper();
        }

        internal static string AskConfirmation(string msg = "")
        {
            if (msg != "")
            {
                CommonIO.MessageToUserInBlue(msg);
            }
            string usrResponse = Console.ReadLine();
            return usrResponse.ToUpper();
        }

        public static Product AskForNewProduct()
        {
            List<Product> ProductList = ProductRepo.ReadFile();

            foreach (Product product in ProductList)
            {
                Console.WriteLine("  " + (ProductList.IndexOf(product) + 1) + ". " + product.ProductType);
            }
            Console.WriteLine("Type the number of the product being purchased.");
            int userNumChoice = CommonIO.GetIntFromUser(0, ProductList.Count);

            if (userNumChoice == 0)
            {
                // Users choose zero when selecting no product
                return null;
            }
            else
            {
                return ProductList.ElementAt(userNumChoice - 1);
            }
        }

        public static string AskForNewStateAbbr()
        {
            List<StateTax> TaxList = TaxRepo.ReadFile();

            foreach (StateTax tax in TaxList)
            {
                Console.WriteLine("  " + (TaxList.IndexOf(tax) + 1) + ". " + tax.StateName);
            }
            Console.WriteLine("Type the number of the state.");

            int userNumChoice = CommonIO.GetIntFromUser(0, TaxList.Count, -1);
            if (userNumChoice == -1)
            {
                // Users choose zero when selecting no state, which gets set to -1 in GetIntFromUser using offset
                return null;
            }
            else
            {
                StateTax SelectedState = TaxList.ElementAt(userNumChoice);
                return SelectedState.StateAbrev;
            }
        }

        public static string AskForNewCustomerName()
        {
            //TODO: With new customer names, the calling routine should check for blank names, which are usedd in edting.
            bool wrongInput = true;
            String customerName;
            do
            {
                Console.WriteLine("What is the name of the customer on the new order?");
                customerName = Console.ReadLine();
                wrongInput = !customerName.All(c => Char.IsLetterOrDigit(c) || c == ' '
                            || c == ',' || c == '.');
                if (wrongInput)
                { CommonIO.errMsg("Customer names can only contain numbers, letters, periods, and commas."); }
                else
                { wrongInput = false; }
            } while (wrongInput);
            return customerName;
        }

        private static DateTime AskForNewOrderDate(string msg = "")
        {
            if (msg != "")
            {
                Console.WriteLine(msg);
            }
            DateTime orderDate;
            int orderDay = 0, maxYear = 0, orderYear = 0, orderMonth = 0;
            do
            {
                Console.WriteLine("What is the numeric month of the new order?");
                orderMonth = CommonIO.GetIntFromUser(1, 12);

                Console.WriteLine("What is the numeric day of the new order?");
                orderDay = CommonIO.GetIntFromUser(1, 31);

                int maxYr = DateTime.Today.Year + 20;
                Console.WriteLine($"What is the 4 digit year of the order? (Max is {maxYr}.)");
                int currentYr = DateTime.Now.Year;
                
                int orderYr = CommonIO.GetIntFromUser(currentYr, maxYr);

                orderDate = new DateTime(orderYr, orderMonth, orderDay);
                if (orderDate < DateTime.Now)
                { CommonIO.errMsg("Order dates must be in the future. Please type a new date."); }

            } while (orderDate < DateTime.Now);
            return orderDate;
        }

        public static DateTime AskForOrderDate()
        {
            Console.WriteLine("What is the numeric month of the order?");
            int orderMonth = CommonIO.GetIntFromUser(1, 12);

            Console.WriteLine("What is the numeric day of the order?");
            int orderDay = CommonIO.GetIntFromUser(1, 31);

            Console.WriteLine("What is the 4 digit year of the order?");
            int currentYear = DateTime.Now.Year;
            int maxYear = currentYear + 20;
            int orderYear = CommonIO.GetIntFromUser(currentYear, maxYear);

            DateTime orderDate = new DateTime(orderYear, orderMonth, orderDay);
            return orderDate;
        }

        /// <summary>
        /// Display only 1 order in console, which different than displaying
        /// one day's order. See the routine ADD NAME for one day's order.
        /// </summary>
        /// <param name="order"></param>
        public static void DisplayOrderDetails(Order order)
        {
            Console.Write($"OrderNumber: { order.OrderNumber} | ");
            Console.WriteLine(order.OrderDate.ToShortDateString());
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost}");
            Console.WriteLine($"Labor: {order.LaborCost}");
            Console.WriteLine($"Tax: " + String.Format("{0:.##}", order.Tax));
            Console.WriteLine($"Total: {order.Total}");
            DrawStarLine();
        }

        private static void DrawStarLine()
        {
            for (int i = 0; i < 85; i++) { Console.Write("*"); }
            Console.WriteLine();
        }


        public static OrderLookupResponse AskForExistingOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime fileDateTime = ConsoleIO.AskForOrderDate();

            //Ask the user to select a date by the number listed.
            int userNumChoice = CommonIO.GetIntFromUser(1, 999, 0, "What order number would you like to view?");

            OrderLookupResponse response = manager.LookupOrder(fileDateTime, userNumChoice);
            if (!response.Success)
            {
                response.Message = "Order not found";

                CommonIO.MessageToUserInBlue(response.Message);
                CommonIO.Continue();
            }
            return response;

        }
        public static decimal AskForArea()
        {
            Console.WriteLine("What is area in square feet (min=100)?");
            decimal area = 0;
            int min = 100, max = 100000;
            do
            {
                area = CommonIO.GetIntFromUser(min, max);
                if (area < 100)
                {
                    CommonIO.MessageToUserInBlue("Please enter a number between 100 and 100000");
                }

            } while (area < min && area > max);
            return area;
        }

    }
}
