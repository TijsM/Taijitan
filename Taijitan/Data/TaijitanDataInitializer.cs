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
                TrainingDay dinsdag = new TrainingDay("Dinsdag", 18.00, 20.00, DayOfWeek.Tuesday);
                TrainingDay woensdag = new TrainingDay("Woensdag", 14.00, 15.50, DayOfWeek.Wednesday);
                TrainingDay donderdag = new TrainingDay("Donderdag", 18.00, 20.00, DayOfWeek.Thursday);
                TrainingDay zaterdag = new TrainingDay("Zaterdag", 10.00, 11.50, DayOfWeek.Saturday);
                TrainingDay zondag = new TrainingDay("Zondag", 11.00, 12.30, DayOfWeek.Sunday);

                IEnumerable<TrainingDay> trainingDays = new List<TrainingDay>
                {
                     dinsdag,woensdag,donderdag,zaterdag,zondag
                };
                _dbContext.TrainingDays.AddRange(trainingDays);




                Formula dinDon = new Formula("dinsdag en donderdag", new List<TrainingDay> { dinsdag, donderdag });
                Formula dinZat = new Formula("dinsdag en zaterdag", new List<TrainingDay> { dinsdag, zaterdag });
                Formula woeZat = new Formula("woensdag en zaterdag", new List<TrainingDay> { woensdag, zaterdag });
                Formula woe = new Formula("woensdag", new List<TrainingDay> { woensdag });
                Formula zat = new Formula("zaterdag", new List<TrainingDay> { zaterdag });
                Formula activiteit = new Formula("deelname aan activiteit", new List<TrainingDay>());
                Formula stage = new Formula("deelname aan meerdaagse stage", new List<TrainingDay>());

                IEnumerable<Formula> formulas = new List<Formula>
                {
                    dinDon,woe,woeZat,zat,activiteit,stage,dinZat
                };
                _dbContext.Formulas.AddRange(formulas);

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
                     new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, Country.Belgium, "16", "0492554616", "jarne.deschacht@student.hogent.be",dinDon, new DateTime(2016 / 01 / 30), Gender.Man, Country.Belgium, "09-08-1999.400-08", "Gent"),
                     new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, Country.Belgium, "Unknown", "0499721771", "tijs.martens@student.hogent.be",woe,  new DateTime(2014/06/2), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"),
                     new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", adinkerke, Country.Belgium, "Unknown", "0000000000", "robbe.dekien@student.hogent.be",dinDon, new DateTime(2016 / 05 / 30), Gender.Man, Country.Belgium, "02-06-1999.100-20", "Gent"),
                     new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Bijlokeweg", gent, Country.Belgium ,"73", "0000000000", "stef.verlinde@student.hogent.be",dinDon, new DateTime(2015 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-37", "Gent")
            };

                IEnumerable<Teacher> teachers = new List<Teacher>
                {
                     new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", gent, Country.Belgium, "1", "+23456987447", "teacher@taijitan.be" , new DateTime(2005/01/30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent")


                };

                IEnumerable<Admin> admins = new List<Admin>
                {
                     new Admin("Admin", "Administrator", new DateTime(1980, 1, 15), "StationStraat", nazareth, Country.Belgium, "15", "+3249981557", "admin@taijitan.be",  new DateTime(2005/01/30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"),

                };

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
