using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class TrainingDayRepository : ITrainingDayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TrainingDay> _trainingDays;

        public TrainingDayRepository(ApplicationDbContext context)
        {
            _context = context;
            _trainingDays = context.TrainingDays;
        }

        public void Add(TrainingDay trainingDay)
        {
            _trainingDays.Add(trainingDay);
        }

        public void Delete(TrainingDay trainingDay)
        {
            _trainingDays.Remove(trainingDay);
        }

        public IEnumerable<TrainingDay> GetAll()
        {
            return _trainingDays.AsNoTracking().ToList();
        }

        public TrainingDay GetByDayOfWeek(DayOfWeek dayOfWeek)
        {
            return _trainingDays.SingleOrDefault(d => d.DayOfWeek == dayOfWeek);
        }

        public TrainingDay getById(int id)
        {
            return _trainingDays.SingleOrDefault(d => d.TrainingDayId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
