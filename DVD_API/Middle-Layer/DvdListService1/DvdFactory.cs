using DvdListService1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdListService1
{
    public static class DvdFactory {

        public static IDvdRepository Create()
        {

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {

                case "SampleData":
                    return new DvdRepoMock();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                case "ADO":
                    return new DvdRepositoryADO();

                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}