using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public enum Niveau
    {
        [Display(Name = "witte gordel")]
        Beginneling,

        [Display(Name = "witte gordel met geel streepje")]
        Kyu6,

        [Display(Name = "gele gordel")]
        Kyu5,

        [Display(Name = "oranje gordel")]
        Kyu4,

        [Display(Name = "groene gordel")]
        Kyu3,

        [Display(Name = "blauwe gordel")]
        Kyu2,

        [Display(Name = "bruine gordel")]
        Kyu1,

        [Display(Name = "zwarte gordel met 1 witte streep ")]
        Dan1,

        [Display(Name = "zwarte gordel met 2 witte strepen")]
        Dan2,

        [Display(Name = "zwarte gordel met 3 witte strepen")]
        Dan3,

        [Display(Name = "zwarte gordel met 4 witte strepen")]
        Dan4,

        [Display(Name = "zwarte gordel met 3 strepen (wit-rood-wit) ")]
        Dan5,

        [Display(Name = "rood-wit geblokte gordel")]
        Dan6,

        [Display(Name = "rood-wit geblokte gordel")]
        Dan7,

        [Display(Name = "rood-wit geblokte gordel")]
        Dan8,

        [Display(Name = "rode gordel ")]
        Dan9,

        [Display(Name = "rode gordel ")]
        Dan10,

        [Display(Name = "rode gordel ")]
        Dan11,

        [Display(Name = "brede witte gordel")]
        Dan12
    }
}
