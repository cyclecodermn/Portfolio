using Orders.Models;
using Orders.Models.Interfaces;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data
{
    public class OrderTestRepo2 : IOrderRepo
    {
        //static List<Order> Orders = new List<Order>();
        private static List<Order> _allOrders = new List<Order>()
        {
            GetOneOrder(),
            GetSecondOrder()
        };



        public OrderTestRepo2()
        {
            //_allOrders.Add(GetOneOrder());
            //_allOrders.Add(GetSecondOrder());
        }

        public OrdrLkpResponses LoadOrders(DateTime FileDate)
        {
            //_allOrders = Orders;

            OrdrLkpResponses AllResponses = new OrdrLkpResponses();

            AllResponses.Orders = _allOrders.Where(o => o.OrderDate == FileDate).ToList();

            if (AllResponses.Orders.Count > 0)
            {
                AllResponses.Success = true;
            }
            else
            {
                AllResponses.Success = false;
            }
            return AllResponses;
        }

        public Order LoadOrder(DateTime orderDate, int orderNum)
        {
            //            List<Order> Orders = new List<Order>();

            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = LoadOrders(orderDate);
            //_allOrders = AllOrders.Orders;

            if (!AllOrders.Success)
            {
                return null;
            }

            Order orderCk = AllOrders.Orders.FirstOrDefault(a =>  a.OrderNumber == orderNum);


            return orderCk;
        }

        private static Order GetOneOrder()
        {
            Order NewOrder = new Order();

            DateTime orderDate = new DateTime(2022, 02, 2);

            NewOrder.OrderDate = orderDate;
            NewOrder.OrderNumber = 1;
            NewOrder.CustomerName = "Al Adams";
            NewOrder.State = "IN";
            NewOrder.TaxRate = 6.00M;
            NewOrder.ProductType = "Tile";
            NewOrder.Area = 2.00m;
            NewOrder.CostPerSquareFoot = 3.50m;
            NewOrder.LaborCostPerSquareFoot = 4.15m;
            NewOrder.MaterialCost = 7.00m;
            NewOrder.LaborCost = 8.30m;
            NewOrder.Tax = 0.99m;
            NewOrder.Total = 16.29m;
            return NewOrder;
        }

        private static Order GetSecondOrder()
        {
            Order NewOrder = new Order();

            DateTime orderDate = new DateTime(2022, 2, 2);

            NewOrder.OrderDate = orderDate;
            NewOrder.OrderNumber = 2;
            NewOrder.CustomerName = "Sandy Meyer";
            NewOrder.State = "IN";
            NewOrder.TaxRate = 6.00M;
            NewOrder.ProductType = "Tile";
            NewOrder.Area = 2.00m;
            NewOrder.CostPerSquareFoot = 3.50m;
            NewOrder.LaborCostPerSquareFoot = 4.15m;
            NewOrder.MaterialCost = 7.00m;
            NewOrder.LaborCost = 8.30m;
            NewOrder.Tax = 0.99m;
            NewOrder.Total = 16.29m;
            return NewOrder;
        }

        public void SaveOrder(Order NewOrder)
        {

            _allOrders.Add(NewOrder);
            //_allOrders = Orders;
            //Console.WriteLine();

            // return this.Orders;
        }
        public void SaveOrders(DateTime FileDate, List<Order> allOrders)
        {
            _allOrders.RemoveAll(o => o.OrderDate == FileDate);
            foreach(Order order in allOrders)
            {
                _allOrders.Add(order);
            }
            //'_allOrders = allOrders;
        }
    }

}