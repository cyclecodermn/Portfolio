using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdListService1.Models
{
    public class DvdRepoMock : IDvdRepository
    {
        private static List<DVD> _DVDs;

        static DvdRepoMock()
        {
            _DVDs = new List<DVD>()
                {
                  new DVD {DvdId=1, Title="Chocolat", realeaseYear=2000, Director="Lasse Hallström", Rating="PG", Notes="Great French film, and Johnny Depp's accent is spot-on." },
                  new DVD {DvdId=2, Title="Ethel & Ernest", realeaseYear=2016, Director="Roger Mainwood", Rating="PG-13", Notes="The true story of British author and illustrator Raymond Briggs's parents, Ethel and Ernest, two ordinary Londoners living through a period of extraordinary events and immense social change." },
                };
        }

        public  IEnumerable<DVD> GetAll()
        {
            return _DVDs;
        }

        public  DVD Get(int DvdId)
        {
            return _DVDs.FirstOrDefault(d => d.DvdId == DvdId);
        }

        public  void Create(DVD newDVD)
        {
            if (_DVDs.Any())
            {
                newDVD.DvdId = _DVDs.Max(d => d.DvdId) + 1;
            }
            else
            {
                newDVD.DvdId = 0;
            }

            _DVDs.Add(newDVD);
        }

        public  void Update(DVD updatedDVD)
        {
            _DVDs.RemoveAll(d => d.DvdId == updatedDVD.DvdId);
            _DVDs.Add(updatedDVD);
        }

        public  void Delete(int DvdId)
        {
            _DVDs.RemoveAll(d => d.DvdId == DvdId);
        }

        public IEnumerable<DVD> GetByDirector(string term)
        {
            return _DVDs.Where(d => d.Director == term);
        }

        public IEnumerable<DVD> GetByYear(string term)
        {
            return _DVDs.Where(d => d.realeaseYear.ToString().Contains(term));
        }

        public IEnumerable<DVD> GetByTitle(string term)
        {
            return _DVDs.Where(d => d.Title.ToString().Contains(term));
        }

        public IEnumerable<DVD> GetByRating(string term)
        {
            return _DVDs.Where(d => d.Title.ToString().Contains(term));
        }
    }
}