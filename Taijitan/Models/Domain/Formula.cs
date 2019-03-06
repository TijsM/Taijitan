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
        //public IEnumerable<TrainingDay> TrainingDays { get; set; }
        public string Name { get; set; }

        public Formula(string name,IEnumerable<FormulaTrainingDay> trainingDays)
        {
            FormulaTrainingDays = trainingDays;
            Name = name;
        }
        public Formula()
        {
            FormulaTrainingDays = new List<FormulaTrainingDay>();
        }
    }
}
