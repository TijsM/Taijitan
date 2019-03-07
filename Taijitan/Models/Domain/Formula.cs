using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    //public enum Formula
    //{
    //    [Display(Name="dinsdag en donderdag")]
    //    Formule1,

    //    [Display(Name = "dinsdag en zaterdag")]
    //    Formule2,

    //    [Display(Name = "woensdag en zaterdag")]
    //    Formule3,

    //    [Display(Name = "woensdag")]
    //    Formule4,
        
    //    [Display(Name = "zaterdag")]
    //    Formule5,

    //    [Display(Name = "deelname aan activiteit")]
    //    Formule6,

    //    [Display(Name = "deelname meerdaagse stage")]
    //    Formule7
    //}
    public class Formula
    {
        public int FormulaId { get; set; }
        public IEnumerable<FormulaTrainingDay> FormulaTrainingDays{ get; set; }
        public IEnumerable<TrainingDay> TrainingDays { get; }
        public string Name { get; set; }

        public Formula(string name,IEnumerable<TrainingDay> trainingDays)
        {
            Name = name;
            TrainingDays = new List<TrainingDay>();
            FormulaTrainingDays = new List<FormulaTrainingDay>();
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
        }
    }
}
