using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models
{/// <summary>
/// Order object with these fields: OrderDate, OrderNumber, 
/// CustomerName, State, ProductType, Area, CostPerSquareFoot, 
/// LaborCostPerSquareFoot, Material Cost, Tax, and Total
/// </summary>
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost
        {
            //•	MaterialCost = (Area* CostPerSquareFoot)
            get
            {
                return Area * CostPerSquareFoot;
            }
            set { }
        }
        public decimal LaborCost
        {
            //•	LaborCost = (Area* LaborCostPerSquareFoot)
            get
            {
                return Area * LaborCostPerSquareFoot;
            }
            set { }
        }
        public decimal Tax
        {
            get
            {
                return Tax = ((MaterialCost + LaborCost) * (TaxRate / 100));
            }
            set { }

        }
        //•	Tax = ((MaterialCost + LaborCost) * (TaxRate/100))
        //o Tax rates are stored as whole numbers
        public decimal Total
        {
            get
            {
                return MaterialCost + LaborCost;
            }
            set { }

        }
        //•	Total = (MaterialCost + LaborCost + Tax)

    }
}
