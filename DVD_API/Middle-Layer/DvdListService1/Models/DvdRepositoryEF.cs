using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DvdListService1.Models
{
    public class DvdRepositoryEF : IDvdRepository
    {
        DvdRepoEFEntities _DVDs = new DvdRepoEFEntities();
        public void Create(DVD newDVD)
        {
            if (_DVDs.DVDs1.Any())
            {
                newDVD.DvdId = _DVDs.DVDs1.Max(d => d.DvdId) + 1;
            }
            else
            {
                newDVD.DvdId = 0;
            }

            _DVDs.DVDs1.Add(newDVD);
            _DVDs.SaveChanges();
        }

        public void Delete(int DvdId)
        {
            var target = _DVDs.DVDs1.FirstOrDefault(d => d.DvdId == DvdId);
            if (target != null)
            {
                _DVDs.DVDs1.Remove(target);
                _DVDs.SaveChanges();
            }
        }

        public DVD Get(int DvdId)
        {
            return _DVDs.DVDs1.FirstOrDefault(d => d.DvdId == DvdId);
        }

        public IEnumerable<DVD> GetAll()
        {
            return _DVDs.DVDs1;
        }

        public IEnumerable<DVD> GetByDirector(string term)
        {
            return _DVDs.DVDs1.Where(d => d.Director == term);
        }

        public IEnumerable<DVD> GetByRating(string term)
        {
            return _DVDs.DVDs1.Where(d => d.Title.ToString().Contains(term));
        }

        public IEnumerable<DVD> GetByTitle(string term)
        {
            return _DVDs.DVDs1.Where(d => d.Title.ToString().Contains(term));
        }

        public IEnumerable<DVD> GetByYear(string term)
        {
            return _DVDs.DVDs1.Where(d => d.realeaseYear.ToString().Contains(term));
        }

        public void Update(DVD updatedDVD)
        {
            Delete(updatedDVD.DvdId);
            Create(updatedDVD);
           //_DVDs.SaveChanges();
        }

    }
}