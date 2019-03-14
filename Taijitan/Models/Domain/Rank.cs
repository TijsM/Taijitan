using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public enum Rank
    {
        //[Display(Name = "witte gordel")]
        //Starter = 0,

        [Display(Name = "Rokku-kyu")]
        [Description("Witte band")]
        Kyu6 = 1,

        [Display(Name = "Go-kyu")]
        [Description("Gele band")]
        Kyu5 = 2,

        [Display(Name = "Yon-kyu")]
        [Description("Oranje band")]
        Kyu4 = 3,

        [Display(Name = "San-kyu")]
        [Description("Groene band")]
        Kyu3 = 4,

        [Display(Name = "Ni-kyu")]
        [Description("Blauwe band")]
        Kyu2 = 5,

        [Display(Name = "Ichi-kyu")]
        [Description("Bruine band")]
        Kyu1 = 6,

        [Display(Name = "Sho-dan")]
        [Description("Zwarte band")]
        Dan1 = 7,

        [Display(Name = "Ni-dan")]
        [Description("Zwarte band")]
        Dan2 = 8,

        [Display(Name = "San-dan")]
        [Description("Zwarte band")]
        Dan3 = 9,

        [Display(Name = "Yon-dan")]
        [Description("Zwarte band")]
        Dan4 = 10,

        [Display(Name = "Go-dan")]
        [Description("Zwarte band")]
        Dan5 = 11,

        [Description("witte breede band")]
        [Display(Name = "Rood-witte band")]
        Dan6 = 12,

        [Display(Name = "Shichi-dan")]
        [Description("Rood-witte band")]
        Dan7 = 13,

        [Display(Name = "Hachi-dan")]
        [Description("Rood-witte band")]
        Dan8 = 14,

        [Display(Name = "Ku-dan")]
        [Description("Rode band")]
        Dan9 = 15,

        [Description("Rode band")]
        [Display(Name = "Ju-dan")]
        Dan10 = 16,

        [Display(Name = "Rode band")]
        [Description("witte breede band")]
        Dan11 = 17,

        [Display(Name = "Juni-dan")]
        [Description("witte brede band")]
        Dan12 = 18
    }
}
