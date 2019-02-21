using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<City> _cities;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
            _cities = _context.Cities;
        }

        public void Add(City city)
        {
            _cities.Add(city);
        }

        public void Delete(City city)
        {
            _cities.Remove(city);
        }

        public IEnumerable<City> GetAll()
        {
            return _cities.AsNoTracking().ToList();
        }

        public City GetByPostalCode(string postalCode)
        {
            return _cities.SingleOrDefault(c => c.Postalcode.Equals(postalCode));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
