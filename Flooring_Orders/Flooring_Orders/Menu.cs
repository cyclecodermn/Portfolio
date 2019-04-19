using Orders.UI;
using Orders.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    public class Menu
    {
        public static void Start()
        {
            int menuChoice = 0;
            do
            {
                ShowMenu();
                menuChoice = CommonIO.GetIntFromUser(1, 5);
                ApplyChoice(menuChoice);
            } while (menuChoice != 5) ;
        }

        private static void ShowMenu()
        {
            Console.Clear();
            DrawStarLine();
            Console.WriteLine("* Flooring Program ");
            Console.WriteLine("*");
            Console.WriteLine("* 1. Display Orders ");
            Console.WriteLine("* 2. Add an Order");
            Console.WriteLine("* 3. Edit an Order");
            Console.WriteLine("* 4. Remove an Order");
            Console.WriteLine("* 5. Quit");
            Console.WriteLine("*");
            DrawStarLine();
        }

        private static void ApplyChoice(int menuChoice)
        {
            switch (menuChoice)
            {
                case 1:
                    OrderLookupWorkflow LookupWorkflow = new OrderLookupWorkflow();
                    LookupWorkflow.Execute();
                    break;
                case 2:
                    AddOrderWorkflow AddOrder = new AddOrderWorkflow();
                    AddOrder.Execute();
                    break;
                case 3:
                    EditOrderWorkflow EditOrder = new EditOrderWorkflow();
                    EditOrder.Execute();
                    break;
                case 4:
                    DeleteOrderWorkflow DeleteOrder = new DeleteOrderWorkflow();
                    DeleteOrder.Execute();
                    break;
                case 5:
                    Console.Write("To exit, ");
                    CommonIO.Continue();
                    break;
            }

        }
        private static void DrawStarLine()
        {
            for (int i = 0; i < 85; i++) { Console.Write("*"); }
            Console.WriteLine();
        }
    }
}
