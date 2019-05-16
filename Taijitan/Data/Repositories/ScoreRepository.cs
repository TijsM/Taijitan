using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Score> _scores;

        public ScoreRepository(ApplicationDbContext context)
        {
            _context = context;
            _scores = context.Scores;
        }
        public void Add(Score score)
        {
            _scores.Add(score);
        }

        public void Delete(Score score)
        {
            _scores.Remove(score);
        }

        public IEnumerable<Score> GetAll()
        {
            return _scores;
        }

        public Score GetById(int id)
        {
            return _scores.FirstOrDefault(s => s.ScoreId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Score GetByAmount(int amount, int id)
        {
            return _scores.Where(s => s.MemberId == id).FirstOrDefault(s => s.Amount == amount);
        }
    }
}
