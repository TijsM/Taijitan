using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class FormulaTrainingDay
    {
        public int FormulaId { get; set; }
        public Formula Formula { get; set; }
        public int TrainingsDayId { get; set; }
        public TrainingDay TrainingDay { get; set; }



        public FormulaTrainingDay(int formulaId, Formula formula, int trainingDayId, TrainingDay trainingDay)
        {
            FormulaId = formulaId;
            Formula = formula;
            TrainingsDayId = trainingDayId;
            TrainingDay = trainingDay;
        }




    }

    
}
