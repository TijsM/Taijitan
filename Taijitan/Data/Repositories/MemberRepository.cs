using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Member> _members;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
            _members = context.Members;
        }


        public void Add(Member member)
        {
            _members.Add(member);
        }

        public void Delete(Member member)
        {
            _members.Remove(member);
        }

        public IEnumerable<Member> GetAll()
        {
            return _members.AsNoTracking().ToList();
        }

        public Member GetById(int id)
        {
            return _members.SingleOrDefault(m => m.MemberId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
