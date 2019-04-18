using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO;

namespace bikes.data.Interfaces.FactoriesFactories
{
    public static class MakeRepoFactory
    {
        public static IMakeRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new MakeRepoADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value for Make.");
            }
        }
    }
}
