using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public User Member { get; set; }
        public int MemberId { get; set; }
    }
}
