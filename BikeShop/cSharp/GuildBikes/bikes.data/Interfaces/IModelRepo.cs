using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Tables;

namespace bikes.data.Interfaces
{
    public interface IModelRepo
    {
        List<BikeModelTable> GetAll();
        void Insert(BikeModelTable NewModel);
    }
}
