using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models
{
    /// <summary>
    /// Product contains string ProductType, decimal CostPerSquareFoot, 
    /// and decimal LaborCostPerSquareFoot
    /// </summary>
    public class Product
    {
        //ProductType,CostPerSquareFoot,LaborCostPerSquareFoot

        public string ProductType { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
    }
}
