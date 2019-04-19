using Orders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data
{
    /// <summary>
    /// Returns a list of objects of type Product. Each object contains 
    /// PrductType, CostPerSquareFoot, and LaborCostPerSquareFoot
    /// </summary>
    public static class ProductRepo
    {
        public static List<Product> ReadFile()
        {
            string line, ignoreHeader;
            Product oneProduct = null;
            List<Product> ProductsInFile = new List<Product>();

            //string path = "Files\\Products.txt";
            string path = @"C:\Users\steve\OneDrive\Work\sGuild\Week05\Code\Flooring_Orders\Orders.Data\Files\Products.txt";

            using (StreamReader fileRead = new StreamReader(path))
            {
                ignoreHeader = fileRead.ReadLine();
                while ((line = fileRead.ReadLine()) != null)
                {
                    oneProduct = mapProduct(line);
                    ProductsInFile.Add(oneProduct);
                }
            }
            return ProductsInFile;

        }

        private static Product mapProduct(string line)
        {
            //var numStyle = NumberStyles.None;
            string acctType = "";
            string[] fields = line.Split(',');


            //ProductType,CostPerSquareFoot,LaborCostPerSquareFoot
            Product oneProduct = new Product();
            oneProduct.ProductType = fields[0];
            oneProduct.CostPerSquareFoot = Decimal.Parse(fields[1]);
            oneProduct.LaborCostPerSquareFoot = Decimal.Parse(fields[2]);
            
            return oneProduct;
        }
    }
}
