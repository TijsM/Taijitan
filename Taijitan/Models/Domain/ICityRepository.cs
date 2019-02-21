using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City GetByPostalCode(string postalCode);
        void Add(City city);
        void Delete(City city);
        void SaveChanges();
    }
}
