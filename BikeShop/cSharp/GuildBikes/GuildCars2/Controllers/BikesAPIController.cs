using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bikes.data.ADO;
using bikes.data.Interfaces;
using bikes.data.Interfaces.Factories;
using bikes.data.Interfaces.FactoriesFactories;
using bikes.models.Queries;
using bikes.models.Tables;

namespace GuildBikes.Controllers
{
    public class BikesAPIController : ApiController
    {
        //        private static IDvdRepository _repo = DvdFactory.Create();
        //private IModelRepo _ModelRepo = ModelRepoFactory.GetRepo();
        //[Route("dvds/{category}/{term}")]

        [Route("api/bikes/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(bool? isNew, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, string makeModelOrYr)
        {
            var repo = BikeRepoFactory.GetRepo();

            try
            {
                var parameters = new BikeSearchParameters()
                {
                    IsNew = isNew,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    MakeModelOrYr = makeModelOrYr
                };

                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/bike/delete/{bikeId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteBike(int bikeId)
        {
            var repo = BikeRepoFactory.GetRepo();
            try
            {
                repo.Delete(bikeId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("api/model/add/{newModel}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddModel(BikeModelTable newModel)
        {
            var repo = ModelRepoFactory.GetRepo();
            try
            {
                repo.Insert(newModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/model/getall/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllModels()
        {
            var repo = ModelRepoFactory.GetRepo();
            try
            {
                return Ok(repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/bike/getall/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllbikes()
        {
            var repo = BikeRepoFactory.GetRepo();
            try
            {
                return Ok(repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/bike/getone/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetOneBike(int id)
        {
            var repo = BikeRepoFactory.GetRepo();
            try
            {
                return Ok(repo.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/model/add/{newMake}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddMake(BikeMakeTable newMake)
        {
            var repo = MakeRepoFactory.GetRepo();
            try
            {
                repo.Insert(newMake);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
