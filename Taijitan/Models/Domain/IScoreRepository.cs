using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface IScoreRepository
    {
        IEnumerable<Score> GetAll();
        Score GetById(int id);
        void Add(Score score);
        void Delete(Score score);
        void SaveChanges();
        Score GetByAmount(int amount, int id);
    }
}
