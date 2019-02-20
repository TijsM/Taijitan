using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member GetById(int id);
        void Add(Member member);
        void Delete(Member member);
        void SaveChanges();
    }
}
