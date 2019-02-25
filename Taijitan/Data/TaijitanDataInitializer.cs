using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class TaijitanDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TaijitanDataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                City bekegem = new City("8480", "Bekegem");
                _dbContext.Cities.Add(bekegem);
                City gent = new City("9000", "Gent");
                _dbContext.Cities.Add(gent);
                City nazareth = new City("9810", "Nazareth");
                _dbContext.Cities.Add(nazareth);
                City adinkerke = new City("8660", "Adinkerke");
                _dbContext.Cities.Add(adinkerke);

                IEnumerable<Member> members = new List<Member>
                {
                     new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, "Belgium", "16", "0492554616", "jarne.deschacht@student.hogent.be"),
                     new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, "Belgium", "Unknown", "0499721771", "tijs.martens@student.hogent.be"),
                     new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", adinkerke, "Belgium", "Unknown", "0000000000", "robbe.dekien@student.hogent.be"),
                     new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Bijlokeweg", gent, "Belgium", "73", "0000000000", "stef.verlinde@student.hogent.be")
                };

                IEnumerable<Teacher> teachers = new List<Teacher>
                {
                     new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", gent, "Belgie", "1", "+23456987447", "teacher@taijitan.be" )

                };

                IEnumerable<Admin> admins = new List<Admin>
                {
                     new Admin("Admin", "Administrator", new DateTime(1980, 1, 15), "StationStraat", nazareth, "Belgie", "15", "+3249981557", "admin@taijitan.be"),
                };


                //Member jarne = new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, "Belgium", "16", "0492554616", "jarne.deschacht@student.hogent.be");
                //_dbContext.Members.Add(jarne);
                //Member tijs = new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, "Belgium", "Unknown", "0499721771", "tijs.martens@student.hogent.be");
                //_dbContext.Members.Add(tijs);
                //Member robbe = new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", adinkerke, "Belgium", "Unknown", "0000000000", "robbe.dekien@student.hogent.be");
                //_dbContext.Members.Add(robbe);
                //Member stef = new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Unknown", gent, "Belgium", "Unknown", "0000000000", "stef.verlinde@student.hogent.be");
                //_dbContext.Members.Add(stef);

                foreach (User member in members)
                {
                    _dbContext.Users_Domain.Add(member);
                    var username = member.Email;
                    var email = member.Email;
                    var password = "P@ssword1";
                    var role = "Member";
                    await CreateUser(username, email, password, role);
                }

                foreach (User teacher in teachers)
                {
                    _dbContext.Users_Domain.Add(teacher);
                    var username = teacher.Email;
                    var email = teacher.Email;
                    var password = "P@ssword1";
                    var role = "Teacher";
                    await CreateUser(username, email, password, role);
                }

                foreach (User admin in admins)
                {
                    _dbContext.Users_Domain.Add(admin);
                    var username = admin.Email;
                    var email = admin.Email;
                    var password = "P@ssword1";
                    var role = "Admin";
                    await CreateUser(username, email, password, role);
                }

                //await CreateUser("admin@taijitan.be", "admin@taijitan.be", "P@ssword1", "Admin");
                //await CreateUser("teacher@taijitan.be", "teacher@taijitan.be", "P@ssword1", "Teacher");
                //await CreateUser("member@taijitan.be", "member@taijitan.be", "P@ssword1", "Member");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string userName, string email, string password, string role)
        {
            var user = new IdentityUser { UserName = userName, Email = email };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }
    }
}
