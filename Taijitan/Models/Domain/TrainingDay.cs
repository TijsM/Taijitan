using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class TrainingDay
    {
        public int TrainingDayId { get; set; }
        public string Name { get; set; }
        public double StartHour { get; set; }
        public double StopHour { get; set; }
        public double Duration { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public ICollection<FormulaTrainingDay> FormulaTrainingDays { get; set; }

        public TrainingDay(string name,double startHour,double stopHour,DayOfWeek dayOfWeek)
        {
            Name = name;
            StartHour = startHour;
            StopHour = stopHour;
            DayOfWeek = dayOfWeek;
            Duration = stopHour - startHour;
            FormulaTrainingDays = new List<FormulaTrainingDay>();

        }
        public TrainingDay()
        {
            FormulaTrainingDays = new List<FormulaTrainingDay>();
        }
    }
}
