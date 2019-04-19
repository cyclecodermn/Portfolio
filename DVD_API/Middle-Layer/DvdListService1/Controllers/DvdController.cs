using DvdListService1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdListService1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        private static IDvdRepository _repo =  DvdFactory.Create();
        [Route("dvds/{category}/{term}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetaByTerm(string category, string term)
        {
            IEnumerable<DVD> found = GetSearchResults(category, term);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        IEnumerable<DVD> GetSearchResults(string category, string term)
        {
                switch (category)
                {
                    case "title":
                    return _repo.GetByTitle(term);

                    case "year":
                    return _repo.GetByYear(term);

                    case "director":
                    return _repo.GetByDirector(term);

                    case "rating":
                    return _repo.GetByRating(term);

                    default:
                        return null;
                }

        }


        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            DVD found = _repo.Get(id);

            if (found == null)
                return NotFound();

            return Ok(found);
        }

        [Route("dvd/")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(DVD dvd)
        {
            _repo.Create(dvd);

            return Created($"dvd/{dvd.DvdId}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, DVD dvd)
        {
            _repo.Update(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }

}
