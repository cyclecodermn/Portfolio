using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO;

namespace bikes.data.Interfaces.Factories
{
    public static class FrameRepoFactory
    {
        public static IFrameRepo GetRepo()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new FrameRepoADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value for Model.");
            }
        }
    }
}
