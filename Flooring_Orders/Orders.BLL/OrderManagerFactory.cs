using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using Orders.Data;

namespace Orders.BLL
{
    /// <summary>
    /// Reads config file and sets mode/manager for Test or Prod
    /// </summary>
    public static class OrderManagerFactory
    {//still setting this up, changing it from bank to orders
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrderTestRepo2());
                case "Prod":
                   return new OrderManager(new FileRepo());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
