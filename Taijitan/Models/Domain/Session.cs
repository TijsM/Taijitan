using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Session
    {
        public IEnumerable<Member> members { get; set; }
        public DateTime datum { get; set; }
        public Formula formula { get; set; }
    }
}
