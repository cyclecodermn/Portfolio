using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Queries;
using bikes.models.Tables;

namespace bikes.data.Interfaces
{
    public interface IBikesRepo
    {
        //BikeTable GetById(int BikeId);
        void Insert(BikeTable bike);
        void Update(BikeTable bike);
        void Delete(int BikeId);
        IEnumerable<FeaturedItem> GetFeatured();

        BikeTable GetById(int BikeId);
        InvDetailedItem GetBikeDetails(int BikeId);

        List<InvDetailedItem> GetAll();
        IEnumerable<BikeShortItem> Search(BikeSearchParameters parameters);
    }
}
