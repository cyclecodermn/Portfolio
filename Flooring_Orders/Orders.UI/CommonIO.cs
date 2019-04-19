using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI
{
    public class CommonIO
    {
        public static void ShowHeader(string headerInfo)
        {
            Console.Clear();
            Console.WriteLine(headerInfo);
            Console.WriteLine("--------------------------");
        }
        public static void Continue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press any key to continue");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static int GetIntFromUser(int min, int max, int numOffset=0, string msg="")
        {
            int userChoice = 0;
            bool inRange = false;

            if (msg != "") { Console.WriteLine(msg); }

            do
            {
                while (!int.TryParse(Console.ReadLine(), out userChoice))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Please type a number between {min + numOffset} - {max + numOffset}");
                    Console.ResetColor();
                }

                inRange = (userChoice >= min) && (userChoice <= max);
                if (!inRange)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Please type a number between {min} - {max}");
                    Console.ResetColor();
                }

            } while (!inRange);
            Console.ResetColor();
            return userChoice + numOffset;
        }

        public static void MessageToUserInBlue(string theMsg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(theMsg);
            Console.ResetColor();
        }
        public static void errMsg(string theMsg)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(theMsg);

            Console.ResetColor();
        }


    }
}