using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI
{
    public class EditOrderIO
    {
        public static void ShowEditInstructions()
        {
            /////
            //•	CustomerName, State, ProductType, Area
            Console.Clear();
            Console.WriteLine("Edit an order");
            Console.WriteLine("--------------------------\n");
            Console.WriteLine("You can edit 4 fields: 1) Customer name, 2) State, 3) Product type, and 4) Prduct area.");
            Console.WriteLine("Existing data is shown in parenthesis.\n");

        }
    }
}
