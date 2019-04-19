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
    /// Returns a list objects in StateTax format. Each object includes StateAbrev, StateName, and Tax.
    /// </summary>
    public static class TaxRepo
    {
        public static List<StateTax> ReadFile()
        {
            //C:\Users\steve\source\repos\CRRUD _Weekend_Bike_Routes2\CRRUD _Weekend_Bike_Routes2\DAL

            string line, ignoreHeader;
            StateTax oneStateTax = null;
            List<StateTax> TaxesInFile = new List<StateTax>();

            //string path = "Files\\Taxes.txt";
            string path = @"C:\Users\steve\OneDrive\Work\sGuild\Week05\Code\Flooring_Orders\Orders.Data\Files\Taxes.txt";

            using (StreamReader fileRead = new StreamReader(path))
            {
                ignoreHeader = fileRead.ReadLine();
                while ((line = fileRead.ReadLine()) != null)
                {
                    oneStateTax = mapStateTax(line);
                    TaxesInFile.Add(oneStateTax);
                }
            }
            return TaxesInFile;
            //Console.WriteLine("Press a Enter...");
            //Console.ReadLine();
        }

        private static StateTax mapStateTax(string line)
        {
            //var numStyle = NumberStyles.None;
            string acctType = "";
            string[] fields = line.Split(',');

            StateTax oneStateTax = new StateTax();
            oneStateTax.StateAbrev = fields[0];
            oneStateTax.StateName = fields[1];
            oneStateTax.Tax = Decimal.Parse(fields[2]);
            
            return oneStateTax;
        }
    }
}
