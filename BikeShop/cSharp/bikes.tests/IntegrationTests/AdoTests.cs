using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO;
using bikes.models.Queries;
using bikes.models.Tables;
using NUnit.Framework;

namespace bikes.tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanSearchIsNew()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { IsNew = true });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchOnBikeModel()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { MakeModelOrYr = "RidgeBack" }).ToList();

            Assert.AreEqual(4, found.Count());

            Assert.AreEqual("RidgeBack", found[0].BikeModelName);
            Assert.AreEqual("RidgeBack", found[1].BikeModelName);
            Assert.AreEqual("RidgeBack", found[2].BikeModelName);

        }

        [Test]
        public void CanSearchOnBikeFrame()
        {
            var repo = new BikeRepoADO();

            //List<BikeShortItem> found = new List<BikeShortItem>();
            var found = repo.Search(new BikeSearchParameters { MakeModelOrYr = "Touring" }).ToList();

            Assert.AreEqual(3, found.Count());
            Assert.AreEqual("Touring", found[0].BikeFrame);
            Assert.AreEqual("Touring", found[1].BikeFrame);
            Assert.AreEqual("Touring", found[2].BikeFrame);
        }

        [Test]
        public void CanSearchOnMinYear()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { MinYear = 2006 });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchOnMaxYear()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { MaxYear = 2007 });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchOnMinPrice()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { MinPrice = 500M });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchOnMaxPrice()
        {
            var repo = new BikeRepoADO();

            var found = repo.Search(new BikeSearchParameters { MaxPrice = 500M });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanLoadFrames()
        {
            var repo = new FrameRepoADO();
            var frames = repo.GetAll();

            int frameCount = frames.Count();
            Assert.AreEqual(5, frameCount);

            Assert.AreEqual("Touring", frames[0].BikeFrame);
        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialRepoADO();
            var specials = repo.GetAll();

            int specialCount = specials.Count();
            Assert.AreEqual(3, specialCount);

            Assert.AreEqual("Summer Sale", specials[0].SpecialTitle);
        }

        [Test]
        public void CanLoadAllBikes()
        {
            var repo = new BikeRepoADO();
            List<InvDetailedItem> Bikes = repo.GetAll();

            int BikeCount = Bikes.Count();
            Assert.AreEqual(10, BikeCount);

            Assert.AreEqual("Surley", Bikes[2].BikeMake);
            Assert.AreEqual(1975, Bikes[4].BikeYear);
            Assert.AreEqual(true, Bikes[9].BikeIsNew);
        }

        [Test]
        public void CanLoadNewBikes()
        {
            var repo = new BikeRepoADO();
            List<InvDetailedItem> Bikes = repo.GetAll();

            int BikeCount = Bikes.Count();
            Assert.AreEqual(10, BikeCount);

            Assert.AreEqual("Surley", Bikes[2].BikeMake);
            Assert.AreEqual(1975, Bikes[4].BikeYear);
            Assert.AreEqual(true, Bikes[9].BikeIsNew);
        }



        [Test]
        public void CanLoadAllModels()
        {
            var repo = new ModelRepoADO();
            var Models = repo.GetAll();

            int ModelCount = Models.Count();
            Assert.AreEqual(3, ModelCount);

            Assert.AreEqual("RidgeBack", Models[1].BikeModelName);
        }

        [Test]
        public void CanLoadAllMakes()
        {
            var repo = new MakeRepoADO();
            var Makes = repo.GetAll();

            int MakeCount = Makes.Count();
            Assert.AreEqual(4, MakeCount);

            Assert.AreEqual("Surley", Makes[1].BikeMakeName);
        }


        [Test]
        public void CanLoadBike()
        {
            var repo = new BikeRepoADO();
            var bike = repo.GetBikeDetails(1);

            Assert.IsNotNull(bike);
            //            Assert.AreEqual(1, bike.BikeId);
            Assert.AreEqual("Surley", bike.BikeMake);
            Assert.AreEqual("Long Haul Trucker", bike.BikeModel);
            Assert.AreEqual("Yellow", bike.FrameColor);
            Assert.AreEqual("Red", bike.TrimColor);
            Assert.AreEqual("Touring", bike.BikeFrame);
            Assert.AreEqual(1111.00M, bike.BikeMsrp);
            Assert.AreEqual(1100.00M, bike.BikeListPrice);
            Assert.AreEqual(2019, bike.BikeYear);
            Assert.AreEqual(true, bike.BikeIsNew);
            Assert.AreEqual(10, bike.BikeCondition);
            Assert.AreEqual(18, bike.BikeNumGears);
            Assert.AreEqual("1111111", bike.BikeSerialNum);
            Assert.AreEqual("Fresh out of the box", bike.BikeDescription);
            Assert.AreEqual("bike-pic (0).jpg", bike.BikePictName);

        }

        [Test]
        public void NotFoundListingReturnsNull()
        {
            var repo = new BikeRepoADO();
            var bike = repo.GetById(999999);
            Assert.IsNull(bike);
        }

        [Test]
        public void CanAddBike()
        {
            BikeTable BikeToAdd = new BikeTable();
            var repo = new BikeRepoADO();

            //            BikeToAdd.BikeId =
            BikeToAdd.BikeMakeId = 3;
            BikeToAdd.BikeModelId = 3;
            BikeToAdd.BikeFrameColorId = 3;
            BikeToAdd.BikeTrimColorId = 3;
            BikeToAdd.BikeFrameId = 3;
            BikeToAdd.BikeMsrp = 3333.00M;
            BikeToAdd.BikeListPrice = 2222.00M;
            BikeToAdd.BikeYear = 2019;
            BikeToAdd.BikeIsNew = true;
            BikeToAdd.BikeCondition = 10;
            BikeToAdd.BikeNumGears = 1;
            BikeToAdd.BikeSerialNum = "34567890";
            BikeToAdd.BikeDescription = "New bike added from Cs";
            BikeToAdd.BikePictName = "bike3.png";

            repo.Insert(BikeToAdd);
            Assert.AreEqual(11, BikeToAdd.BikeId);

        }

        [Test]
        public void CanUpdateBike()
        {
            BikeTable BikeToAdd = new BikeTable();
            var repo = new BikeRepoADO();

            BikeToAdd.BikeMakeId = 3;
            BikeToAdd.BikeModelId = 3;
            BikeToAdd.BikeFrameColorId = 3;
            BikeToAdd.BikeTrimColorId = 3;
            BikeToAdd.BikeFrameId = 3;
            BikeToAdd.BikeMsrp = 3333.00M;
            BikeToAdd.BikeListPrice = 2222.00M;
            BikeToAdd.BikeYear = 2019;
            BikeToAdd.BikeIsNew = true;
            BikeToAdd.BikeCondition = 10;
            BikeToAdd.BikeNumGears = 1;
            BikeToAdd.BikeSerialNum = "34567890";
            BikeToAdd.BikeDescription = "New bike added from Cs";
            BikeToAdd.BikePictName = "bike3.png";

            repo.Insert(BikeToAdd);

            BikeTable BikeToUpdate = new BikeTable();

            BikeToAdd.BikeMakeId = 1;
            BikeToAdd.BikeModelId = 1;
            BikeToAdd.BikeFrameColorId = 1;
            BikeToAdd.BikeTrimColorId = 2;
            BikeToAdd.BikeFrameId = 1;
            BikeToAdd.BikeMsrp = 1234.00M;
            BikeToAdd.BikeListPrice = 1122.00M;
            BikeToAdd.BikeYear = 2018;
            BikeToAdd.BikeIsNew = false;
            BikeToAdd.BikeCondition = 8;
            BikeToAdd.BikeNumGears = 2;
            BikeToAdd.BikeSerialNum = "45678901";
            BikeToAdd.BikeDescription = "Upadeted info from Cs";
            BikeToAdd.BikePictName = "bike-update.png";

            repo.Update(BikeToAdd);

            var updatedBike = repo.GetById(3);

            Assert.AreEqual(1, BikeToAdd.BikeMakeId);
            Assert.AreEqual(1, BikeToAdd.BikeModelId);
            Assert.AreEqual(1, BikeToAdd.BikeFrameColorId);
            Assert.AreEqual(2, BikeToAdd.BikeTrimColorId);
            Assert.AreEqual(1, BikeToAdd.BikeFrameId);
            Assert.AreEqual(1234.00M, BikeToAdd.BikeMsrp);
            Assert.AreEqual(1122.00M, BikeToAdd.BikeListPrice);
            Assert.AreEqual(2018, BikeToAdd.BikeYear);
            Assert.AreEqual(false, BikeToAdd.BikeIsNew);
            Assert.AreEqual(8, BikeToAdd.BikeCondition);
            Assert.AreEqual(2, BikeToAdd.BikeNumGears);
            Assert.AreEqual("45678901", BikeToAdd.BikeSerialNum);
            Assert.AreEqual("Upadeted info from Cs", BikeToAdd.BikeDescription);
            Assert.AreEqual("bike-update.png", BikeToAdd.BikePictName);
        }

        [Test]
        public void CanDeleteBike()
        {
            BikeTable BikeToAdd = new BikeTable();
            var repo = new BikeRepoADO();

            BikeToAdd.BikeMakeId = 3;
            BikeToAdd.BikeModelId = 3;
            BikeToAdd.BikeFrameColorId = 3;
            BikeToAdd.BikeTrimColorId = 3;
            BikeToAdd.BikeFrameId = 3;
            BikeToAdd.BikeMsrp = 0.00M;
            BikeToAdd.BikeListPrice = 0.00M;
            BikeToAdd.BikeYear = 1984;
            BikeToAdd.BikeIsNew = true;
            BikeToAdd.BikeCondition = 0;
            BikeToAdd.BikeNumGears = 1;
            BikeToAdd.BikeSerialNum = "000000";
            BikeToAdd.BikeDescription = "New bike added from Cs for delete test";
            BikeToAdd.BikePictName = "delete.png";

            repo.Insert(BikeToAdd);

            var loaded = repo.GetById(3);
            Assert.IsNotNull(loaded);

            repo.Delete(3);
            loaded = repo.GetById(3);
            Assert.IsNull(loaded);
        }

        [Test]
        public void CanGetOneBikeDetails()
        {
            InvDetailedItem oneBike = null;
            var repo = new BikeRepoADO();

            oneBike = repo.GetBikeDetails(1);

            //  Assert.AreEqual(2, oneBike.BikeFrameColorId);
            // Assert.AreEqual(1, oneBike.BikeTrimColorId);
            Assert.AreEqual(1111.00M, oneBike.BikeMsrp);
            Assert.AreEqual(1100.00M, oneBike.BikeListPrice);
            Assert.AreEqual(2019, oneBike.BikeYear);
            Assert.AreEqual(true, oneBike.BikeIsNew);
            Assert.AreEqual(10, oneBike.BikeCondition);
            Assert.AreEqual(18, oneBike.BikeNumGears);
            Assert.AreEqual("1111111", oneBike.BikeSerialNum);
            Assert.AreEqual("Fresh out of the box", oneBike.BikeDescription);
            Assert.AreEqual("bike-pic (0).jpg", oneBike.BikePictName);


            Assert.AreEqual("Surley", oneBike.BikeMake);
            Assert.AreEqual("Long Haul Trucker", oneBike.BikeModel);
            Assert.AreEqual("Yellow", oneBike.FrameColor);
            Assert.AreEqual("Red", oneBike.TrimColor);

            Assert.IsNotNull(oneBike);
        }

        [Test]
        public void CanLoadFeaturedBikes()
        {
            var repo = new BikeRepoADO();
            List<FeaturedItem> FeaturedBike = repo.GetFeatured().ToList();

            Assert.AreEqual(5, FeaturedBike.Count());

            Assert.AreEqual(1, FeaturedBike[0].FeatureId);
            Assert.AreEqual(2019, FeaturedBike[0].BikeYear);
            Assert.AreEqual("Surley", FeaturedBike[0].BikeMake);
            Assert.AreEqual("Long Haul Trucker", FeaturedBike[0].BikeModel);
            Assert.AreEqual(1100, FeaturedBike[0].BikeListPrice);
        }

        //[Test]
        //public void CanLoadContacts()
        //{
        //    var repo = new ContactTable();
        //    List<FeaturedItem> FeaturedBike = repo.GetFeatured().ToList();

        //    Assert.AreEqual(5, FeaturedBike.Count());

        //    Assert.AreEqual(1, FeaturedBike[0        [Test]

        [Test]
        public void CanAddFrame()
        {
            BikeFrameTable FrameToAdd = new BikeFrameTable();
            var repo= new FrameRepoADO();

            //ModelToAdd.BikeModelId = 4;
            FrameToAdd.BikeFrame = "Test Frame";

            repo.Insert(FrameToAdd);
            Assert.AreEqual(6, FrameToAdd.BikeFrameId);
            if (FrameToAdd.BikeFrameId == 6)
            {
                //If the test succeeded, remove the test frame just added.
                FrameRepoADO.Delete(FrameToAdd.BikeFrameId);
            }
        }

        [Test]
        public void CanAddModel()
        {
            BikeModelTable ModelToAdd = new BikeModelTable();
            var repo = new ModelRepoADO();

            //ModelToAdd.BikeModelId = 4;
            ModelToAdd.BikeModelName = "329-1858";

            repo.Insert(ModelToAdd);
            Assert.AreEqual(4, ModelToAdd.BikeModelId);
            
            //TODO: Uncomment the code below after adding delete to ModelRepo
            //if (ModelToAdd.BikeModelId == 6)
            //{
            //    //If the test succeeded, remove the test frame just added.
            //    ModelRepoADO.Delete(ModelToAdd.BikeModelId);
            //}

        }
        [Test]
        public void CanAddMake()
        {
            BikeMakeTable MakeToAdd = new BikeMakeTable();
            var repo = new MakeRepoADO();

            //MakeToAdd.BikeMakeId = 4;
            MakeToAdd.BikeMakeName = "329-1858";

            repo.Insert(MakeToAdd);
            Assert.AreEqual(5, MakeToAdd.BikeMakeId);
        }

    }

}
