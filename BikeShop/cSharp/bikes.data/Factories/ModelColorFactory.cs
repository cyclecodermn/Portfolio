using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO;

namespace bikes.data.Interfaces.Factories
{
    public static class ColorRepoFactory
    {
        public static IColorRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new ColorRepoADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value for Color.");
            }
        }
    }
}
