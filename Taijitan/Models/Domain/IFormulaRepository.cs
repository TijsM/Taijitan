using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> GetAll();
        IEnumerable<Formula> GetByTrainingDay(TrainingDay trainingDay);
        void Add(Formula formula);
        void Delete(Formula formula);
        void SaveChanges();
    }
}
