using System.Collections.Generic;

namespace DvdListService1.Models
{
    public interface IDvdRepository
    {
        void Create(DVD newDVD);
        void Delete(int DvdId);
        DVD Get(int DvdId);
        IEnumerable<DVD> GetAll();
        void Update(DVD updatedDVD);
        IEnumerable<DVD> GetByDirector(string term);
        IEnumerable<DVD> GetByYear(string term);
        IEnumerable<DVD> GetByTitle(string term);
        IEnumerable<DVD> GetByRating(string term);
    }
}