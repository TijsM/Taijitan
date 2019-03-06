using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Formula> _formulas;

        public FormulaRepository(ApplicationDbContext context)
        {
            _context = context;
            _formulas = context.Formulas;
        }
        public void Add(Formula formula)
        {
            _formulas.Add(formula);
        }

        public void Delete(Formula formula)
        {
            _formulas.Remove(formula);
        }

        public IEnumerable<Formula> GetAll()
        {
            return _formulas.Include(f => f.FormulaTrainingDays).AsNoTracking().ToList();
        }
            
        public IEnumerable<Formula> GetByTrainingDay(TrainingDay trainingDay)
        {
            return _formulas.Include(f => f.FormulaTrainingDays).Where(f => f.FormulaTrainingDays.Any(td => td.formTrainingDayId == trainingDay.TrainingDayId));
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
