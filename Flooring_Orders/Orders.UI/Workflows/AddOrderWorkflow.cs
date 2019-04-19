using Orders.BLL;
using Orders.Data;
using Orders.Models;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI.Workflows
{
    public class AddOrderWorkflow
    {
        private OrderManager _manager;
        private List<Order> _orders;
        public AddOrderWorkflow()
        {
            _manager = OrderManagerFactory.Create();
            // _orders = _manager.LoadOrders();
        }

        public void Execute()
        // The construct doesn't get executed when a method is static.
        // But, without having this static makes it difficult to count orders
        // AND diffiuclt to get the next order number.
        {
            Order orderFromUsr = new Order();

            CommonIO.ShowHeader("Add an order");

            orderFromUsr = ConsoleIO.GetNewOrder();

            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders= _manager.LoadOrders(orderFromUsr.OrderDate);

            if (AllOrders.Success)
            {
                _orders = AllOrders.Orders;
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(AllOrders.Message);
            }

            orderFromUsr = CalcRestofOrder(orderFromUsr);
            //DateTime OrderDate = orderFromUsr.OrderDate;
            CommonIO.MessageToUserInBlue("Order entry is complete.");
            CommonIO.Continue();

            ConsoleIO.DisplayOrderDetails(orderFromUsr);

            String userConfirm = ConsoleIO.RequireYorN("Would you like to save this information? (y=Yes, n=No.)");
            if (userConfirm.ToUpper() == "Y")
            {

                //List<Order> AllOrders = _manager.LoadOrders(DateTime.Now);
                
                _manager.SaveOrder(orderFromUsr);
                //manager.SaveOrders(AllOrders);
                CommonIO.MessageToUserInBlue($"New order ID {orderFromUsr.OrderNumber} has been saved.");
                CommonIO.Continue();
            }
            else
            {
                CommonIO.MessageToUserInBlue($"New order ID {orderFromUsr.OrderNumber} was NOT saved.");
                CommonIO.Continue();
            }
        }

        private static Order CalcRestofOrder(Order orderFromUsr)
        {
            //Keep lInq statements here.
            // Move others to the getter.
            //Order completedOrder = new Order();
            OrderTestRepo2 orderRepo = new OrderTestRepo2();

            //List<Order> allCrntOrders = orderRepo.LoadOrders();
            List<StateTax> allTaxInfo = TaxRepo.ReadFile();
            List<Product> allPrducts = ProductRepo.ReadFile();
            
            StateTax stateTax = allTaxInfo.FirstOrDefault(t => t.StateAbrev == orderFromUsr.State);
            orderFromUsr.TaxRate = stateTax.Tax;

            Product crntProd = allPrducts.FirstOrDefault(p => p.ProductType == orderFromUsr.ProductType);
            orderFromUsr.CostPerSquareFoot = crntProd.CostPerSquareFoot;
            orderFromUsr.LaborCostPerSquareFoot = crntProd.LaborCostPerSquareFoot;

            return orderFromUsr;
        }
    }
}
