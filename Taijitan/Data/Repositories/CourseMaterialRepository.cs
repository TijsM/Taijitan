using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class CourseMaterialRepository : ICourseMaterialRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CourseMaterial> _courseMaterials;

        public CourseMaterialRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _courseMaterials = dbContext.CourseMaterials;
        }
        public void Add(CourseMaterial courseMaterial)
        {
            _courseMaterials.Add(courseMaterial);
        }

        public void Delete(CourseMaterial courseMaterial)
        {
            _courseMaterials.Remove(courseMaterial);
        }

        public IEnumerable<CourseMaterial> GetAll()
        {
            return _courseMaterials.Include(c => c.Images).AsNoTracking().OrderBy(c => c.Title).ToList();
        }

        public CourseMaterial GetById(int id)
        {
            return _courseMaterials.Include(c => c.Images).SingleOrDefault(c => c.MaterialId == id);
        }

        public IEnumerable<CourseMaterial> GetByRank(Rank rank)
        {
            return _courseMaterials.Include(c => c.Images).Where(c => (int)c.Rank == (int)rank).AsNoTracking().OrderBy(c => c.Title).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
