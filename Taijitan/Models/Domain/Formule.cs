using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public enum Formule
    {
        [Display(Name="dinsdag en donderdag")]
        formule1,

        [Display(Name = "dinsdag en zaterdag")]
        formule2,

        [Display(Name = "woensdag en zaterdag")]
        formule3,

        [Display(Name = "woensdag")]
        formule4,
        
        [Display(Name = "zaterdag")]
        formule5,

        [Display(Name = "deelname aan activiteit")]
        formule6,

        [Display(Name = "deelname meerdaagse stage")]
        formule7



    }
}
