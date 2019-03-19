using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Models.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CourseMaterialViewModel
    {
        [JsonProperty]
        public Session Session { get; set; }
        public IEnumerable<CourseMaterial> CourseMaterials { get; set; }
        public IEnumerable<Rank> AllRanks { get; set; }
        [JsonProperty]
        public CourseMaterial SelectedCourseMaterial { get; set; }
        [JsonProperty]
        public Rank SelectedRank { get; set; }
        [JsonProperty]
        public Member SelectedMember { get; set; }

        public CourseMaterialViewModel()
        {
            CourseMaterials = new List<CourseMaterial>();
            AllRanks = new List<Rank>();
        }
    }
}
