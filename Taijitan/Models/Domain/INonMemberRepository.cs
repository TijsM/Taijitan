using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public interface INonMemberRepository
    {
        IEnumerable<NonMember> GetAll();
        NonMember GetByFirstName(string firstName);
        void Add(NonMember nonMember);
        void Delete(NonMember nonMember);
        void SaveChanges();
    }
}
