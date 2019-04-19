using Orders.Models;
using Orders.Models.Interfaces;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data
{
    public class FileRepo : IOrderRepo
    {

        List<Order> _allOrders = new List<Order>();

        public Order LoadOrder(DateTime DateLookup, int OrderNumber)
        {
            OrdrLkpResponses AllOrders = new OrdrLkpResponses();
            AllOrders = LoadOrders(DateLookup);
            _allOrders = AllOrders.Orders;

            if (!AllOrders.Success)
            {
                return null;
            }
            //Order orderCk = _allOrders.FirstOrDefault(a => a.OrderDate == DateLookup);
            Order orderCk = AllOrders.Orders.FirstOrDefault(a => a.OrderNumber == OrderNumber);

            //if (orderCk == null)
            //{
            //    return orderCk;
            //}
            //else
            //{
            //    orderCk = _allOrders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
            //}

            return orderCk;
        }

        public OrdrLkpResponses LoadOrders(DateTime FileDate)
        {
            //DateTime FileDate = new DateTime(2018, 5, 26);

            string fileName = "Orders_" + FileDate.ToString("MMddyyyy") + ".txt";
            fileName = fileName.Replace("/", "");
            string path = @"C:\Users\steve\OneDrive\Work\sGuild\Week05\Code\Flooring_Orders\Flooring_Orders\bin\Debug\Files\" + fileName;

            OrdrLkpResponses ResponsesFromFile = new OrdrLkpResponses();

            List<Order> OrdersFromFile = new List<Order>();
            Order OneFileOrder = new Order();

            FileLookupResponse response = null;
            string[] readText = null;

            try
            {
                readText = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                string exceptionType = ex.GetType().ToString();
                string fileNotFoundEx = "System.IO.FileNotFoundException";
                if (exceptionType == fileNotFoundEx)
                {
                    ResponsesFromFile.Success = false;
                    ResponsesFromFile.Message = "No file found for date " + FileDate;
                    return ResponsesFromFile;
                }
                else
                {
                    throw ex;
                }
            }
            
            bool isHeader = true;
            foreach (string line in readText)
            {
                if (!isHeader)
                {
                    OneFileOrder = MapToOrders(FileDate, line);
                    OrdersFromFile.Add(OneFileOrder);
                }
                isHeader = false;
            }
            _allOrders = OrdersFromFile;

            ResponsesFromFile.Success = true;
            ResponsesFromFile.Message = "Orders successfully Loaded";
            ResponsesFromFile.Orders = OrdersFromFile;

            return ResponsesFromFile;
        }

        public static Order MapToOrders(DateTime FileDate, string line)
        {
            Order OneFileOrder = new Order();

            OneFileOrder.OrderDate = FileDate;

            string[] fields = line.Split(',');

            OneFileOrder.OrderNumber = int.Parse(fields[0]);
            OneFileOrder.CustomerName = fields[1];
            OneFileOrder.State = fields[2];
            OneFileOrder.TaxRate = decimal.Parse(fields[3]);
            OneFileOrder.ProductType = (fields[4]);
            OneFileOrder.Area = decimal.Parse(fields[5]);
            OneFileOrder.CostPerSquareFoot = decimal.Parse(fields[6]);
            OneFileOrder.LaborCostPerSquareFoot = decimal.Parse(fields[7]);
            OneFileOrder.MaterialCost = decimal.Parse(fields[8]);
            OneFileOrder.LaborCost = decimal.Parse(fields[9]);
            OneFileOrder.Tax = decimal.Parse(fields[10]);
            OneFileOrder.Total = decimal.Parse(fields[11]);

            return OneFileOrder;
        }

        public void SaveOrder(Order NewOrder)
        {
            //Orders.Add(NewOrder);
            this._allOrders.Add(NewOrder);
            //           _allOrders = Orders;
            SaveOrders(NewOrder.OrderDate, _allOrders);
        }

        public void SaveOrders(DateTime FileDate, List<Order> AllOrders)

        {
            //private static void WriteOrders(string[] allOrders)
            //{
            //DateTime theDate = new DateTime(2018, 05, 26);
            string fileName = "Orders_" + FileDate.ToString("MMddyyyy") + ".txt";
            fileName = fileName.Replace("/", "");

            //string path = "Files\\" + fileName;
            string path = @"C:\Users\steve\OneDrive\Work\sGuild\Week05\Code\Flooring_Orders\Flooring_Orders\bin\Debug\Files\" + fileName;

            List<string> OneOrderStr = new List<String>();
            string buildOrderString = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,";
            buildOrderString += "CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
            OneOrderStr.Add(buildOrderString);

            foreach (Order OneOrder in AllOrders)
            {
                buildOrderString = (OneOrder.OrderNumber + "," + OneOrder.CustomerName + "," + OneOrder.State + ",");
                buildOrderString += (OneOrder.TaxRate + "," + OneOrder.ProductType + "," + OneOrder.Area + ",");
                buildOrderString += (OneOrder.CostPerSquareFoot + "," + OneOrder.MaterialCost + "," + OneOrder.LaborCost + ",");
                buildOrderString += (OneOrder.MaterialCost + "," + OneOrder.LaborCost + "," + OneOrder.Tax + "," + OneOrder.Total);

                OneOrderStr.Add(buildOrderString);
            }

            try
            {
                //readText = File.ReadAllLines(path);
                File.WriteAllLines(path, OneOrderStr, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                string exceptionType = ex.GetType().ToString();
                string fileNotFoundEx = "System.IO.FileNotFoundException";
                if (exceptionType == fileNotFoundEx)
                {
                    //Handle file not found, create new file with path, made above
                    //List<string> OneOrderStr = new List<String>();
                    buildOrderString = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,";
                    buildOrderString += "CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
                    OneOrderStr.Add(buildOrderString);

                    File.WriteAllLines(path, OneOrderStr, Encoding.UTF8);
                    File.WriteAllLines(path, OneOrderStr, Encoding.UTF8);
                }
                else
                {
                    throw ex;
                }
            }

            /////////////////////////////////////////



            //File.WriteAllLines(path, OneOrderStr, Encoding.UTF8);
            //Console.ReadLine();

        }
    }
}
