using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Formula
    {
        private IEnumerable<TrainingDay> _trainingDays;

        public int FormulaId { get; set; }
        public ICollection<FormulaTrainingDay> FormulaTrainingDays { get; set; }
        public ICollection<SessionFormula> SessionFormulas { get; set; }
        public IEnumerable<Session> Sessions => SessionFormulas.Select(sf => sf.Session).ToList();
        public IEnumerable<TrainingDay> TrainingDays { get; set; }
        public string Name { get; set; }

        public Formula(string name, IEnumerable<TrainingDay> trainingDays)
        {
            Name = name;
            FormulaTrainingDays = new List<FormulaTrainingDay>();
            SessionFormulas = new List<SessionFormula>();
            List<FormulaTrainingDay> test = new List<FormulaTrainingDay>();
            foreach (TrainingDay day in trainingDays)
            {
                test.Add(new FormulaTrainingDay(FormulaId, this, day.TrainingDayId, day));
            }
            FormulaTrainingDays = test;
        }
        public Formula()
        {
            TrainingDays = new List<TrainingDay>();
            FormulaTrainingDays = new List<FormulaTrainingDay>();
            SessionFormulas = new List<SessionFormula>();

        }
    }
}
