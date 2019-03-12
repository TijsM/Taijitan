using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ICourseMaterialRepository
    {
        IEnumerable<CourseMaterial> GetAll();
        IEnumerable<CourseMaterial> GetByRank(Rank rank);
        CourseMaterial GetById(int id);
        void Add(CourseMaterial courseMaterial);
        void Delete(CourseMaterial courseMaterial);
        void SaveChanges();
    }
}
