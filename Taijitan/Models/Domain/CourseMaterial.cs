using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class CourseMaterial
    {
        public int MaterialId { get; set; }
        public string YoutubeURL { get; set; }
        public string FullDescription { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public Rank Rank { get; set; }

        public CourseMaterial(Rank rank,string url,string fullDescription,IEnumerable<Image> images)
        {
            Rank = rank;
            YoutubeURL = url;
            FullDescription = fullDescription;
            Images = images == null ? new HashSet<Image>() : images;
        }
        public CourseMaterial()
        {
            Images = new HashSet<Image>();
        }
    }
}
