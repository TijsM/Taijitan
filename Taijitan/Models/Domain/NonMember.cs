using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class NonMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SessionId { get; set; }

        public NonMember()
        {

        }

        public NonMember(string firstName, string lastName, string email, int sessionId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SessionId = sessionId;
        }
    }
}
