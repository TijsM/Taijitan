using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CourseMaterial
    {
        [JsonProperty]
        public int MaterialId { get; set; }
        public string YoutubeURL { get; set; }
        public string FullDescription { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public Rank Rank { get; set; }
        public string Title { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public CourseMaterial(Rank rank,string url,string fullDescription,IEnumerable<Image> images, string title)
        {
            Rank = rank;
            YoutubeURL = url;
            FullDescription = fullDescription;
            Images = images == null ? new HashSet<Image>() : images;
            Title = title;
            Comments = new List<Comment>();
        }
        public CourseMaterial()
        {
            Images = new HashSet<Image>();
            Comments = new List<Comment>();
        }
    }
}
