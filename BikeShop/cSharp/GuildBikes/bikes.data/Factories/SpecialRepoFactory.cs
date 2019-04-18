using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO;

namespace bikes.data.Interfaces.Factories
{
    public static class SpecialRepoFactory
    {
        public static ISpecialRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new SpecialRepoADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value for Model.");
            }
        }
    }
}
