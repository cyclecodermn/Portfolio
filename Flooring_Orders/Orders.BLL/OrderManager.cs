using Orders.Data;
using Orders.Models;
using Orders.Models.Interfaces;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.BLL
{

    public class OrderManager
    {

        public IOrderRepo _orderRepository;
        OrdrLkpResponses AllLkupResponses = new OrdrLkpResponses();

        private int numProducts;
        //TODO: I used auto-fix to add the private line above. Why is it required to set numProducts?
        public OrderManager(IOrderRepo orderRepository)
        {
            _orderRepository = orderRepository;
            //TODO: Search for _orderRepository to see how it's used and ask if I still don't know.

            // Load all tax Info
            List<StateTax> Taxes = new List<StateTax>();
            Taxes = TaxRepo.ReadFile();

            //Load all Product Info
            List<Product> Products = new List<Product>();
            Products = ProductRepo.ReadFile();
            int numProducts = Products.Count;
            List<Order> allOrders = new List<Order>();

        }
        //TODO: Ask what the lines above do.

        public int TotalProducts()
        {
            return numProducts;
        }

        public OrdrLkpResponses LoadOrders(DateTime OrderDate)
        {   
            AllLkupResponses = _orderRepository.LoadOrders(OrderDate);

            return AllLkupResponses;
        }

        public OrderLookupResponse LookupOrder(DateTime orderDate, int orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            response.Order = _orderRepository.LoadOrder(orderDate, orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "Order number was not found.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public void SaveOrder(Order OrderToSave)
        {             
            _orderRepository.SaveOrder(OrderToSave);
        }

        public void SaveOrders(DateTime FileDate, List<Order> OrdersToSave)
        {
            _orderRepository.SaveOrders(FileDate, OrdersToSave);
        }

        public int NextOrderID (DateTime OrderDate)
        {
            /// Should go in Repo
            /// ///
            List<Order> ListOfOrders = new List<Order>();

            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = LoadOrders(OrderDate);
            ListOfOrders = AllOrders.Orders;

            int maxOrderNum = 0;
            foreach (Order order in ListOfOrders)
            {
                if (order.OrderNumber>maxOrderNum) { maxOrderNum = order.OrderNumber; }
            }

            return maxOrderNum+1;
        }

    }
}
