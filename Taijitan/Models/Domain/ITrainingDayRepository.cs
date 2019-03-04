using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface ITrainingDayRepository
    {
        IEnumerable<TrainingDay> GetAll();
        TrainingDay getById(int id);
        TrainingDay GetByDayOfWeek(DayOfWeek dayOfWeek);
        void Add(TrainingDay trainingDay);
        void Delete(TrainingDay trainingDay);
        void SaveChanges();
    }
}
