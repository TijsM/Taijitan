using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class NonMemberRepository : INonMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<NonMember> _nonMembers;

        public NonMemberRepository(ApplicationDbContext context)
        {
            _context = context;
            _nonMembers = context.NonMembers;
        }
        public void Add(NonMember nonMember)
        {
            _nonMembers.Add(nonMember);
        }

        public void Delete(NonMember nonMember)
        {
            _nonMembers.Remove(nonMember);
        }

        public IEnumerable<NonMember> GetAll()
        {
            return _nonMembers.AsNoTracking().ToList();
        }

        public NonMember GetByFirstName(string firstName)
        {
            return _nonMembers.Where(nm => nm.FirstName.Equals(firstName)).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
