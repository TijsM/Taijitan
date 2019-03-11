using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public Image(string name,string description)
        {
            Name = name;
            Description = description;
        }
    }
}
