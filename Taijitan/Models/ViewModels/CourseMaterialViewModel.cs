using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.ViewModels
{
    public class CourseMaterialViewModel
    {
        public Session Session { get; set; }
        public IEnumerable<CourseMaterial> CourseMaterials { get; set; }
        public IEnumerable<Rank> AllRanks { get; set; }
        public CourseMaterial SelectedCourseMaterial { get; set; }
        public Rank SelectedRank { get; set; }
        public Member SelectedMember { get; set; }

        public CourseMaterialViewModel()
        {
            CourseMaterials = new List<CourseMaterial>();
            AllRanks = new List<Rank>();
        }
    }
}
