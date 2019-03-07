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
               // TrainingDay zondag = new TrainingDay("Zondag", 11.00, 12.50, DayOfWeek.Sunday);

                IEnumerable<TrainingDay> trainingDays = new List<TrainingDay>
                {
                     dinsdag,woensdag,donderdag,zaterdag//,zondag
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
                    dinZat,woeZat,dinDon,woe,zat,activiteit,stage
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
                City antwerpen = new City("2000", "Antwerpen");
                _dbContext.Cities.Add(antwerpen);
                City brussel = new City("1000", "Brussel");
                _dbContext.Cities.Add(brussel);

                IEnumerable<Member> members = new List<Member>
                {


                     new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, Country.Belgium, "16", "0492554616", "jarne.deschacht@student.hogent.be",dinDon, new DateTime(2016 / 01 / 30), Gender.Man, Country.Belgium, "09-08-1999.400-08", "Gent"),
                     new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, Country.Belgium, "Unknown", "0499721771", "tijs.martens@student.hogent.be",woe,  new DateTime(2014/06/2), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"),
                     new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", adinkerke, Country.Belgium, "Unknown", "0000000000", "robbe.dekien@student.hogent.be",dinZat, new DateTime(2016 / 05 / 30), Gender.Man, Country.Belgium, "02-06-1999.100-20", "Gent"),
                     new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Bijlokeweg", gent, Country.Belgium ,"73", "0000000000", "stef.verlinde@student.hogent.be",dinDon, new DateTime(2015 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-37", "Gent"),
                     new Member("Swets", "Jefferyf", new DateTime(1997, 7, 01), "Stationstraat", gent, Country.Belgium ,"105", "+32458732447", "jeffrey.swets@hotmail.com",woe, new DateTime(2016 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-38", "Gent"),
                     new Member("Middeljans", "Eef", new DateTime(1980, 02, 01), "Bergstraat", nazareth, Country.Belgium ,"20", "+32499875236", "eef.middeljans@gmail.com",zat, new DateTime(2017 / 02 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-39", "Antwerpen"),
                     new Member("Van der Ende", "Yolanda", new DateTime(1980, 03, 18), "Rue de la Tannerie", bekegem, Country.Belgium ,"357", "0480822560", "YolandevanderEnde@rhyta.com",woeZat, new DateTime(2014 / 05 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-40", "Gent"),
                     new Member("Velders", "Kressy", new DateTime(1985, 5, 16), "Passieweik", nazareth, Country.Belgium ,"21", "+32499721558", "krissyvelders@gmail.com",dinDon, new DateTime(2015 / 01 / 02), Gender.Women, Country.Belgium, "02-08-1998.306-40", "Gent"),
                     new Member("Sluis", "Willem", new DateTime(1995, 12, 12), "Hoekstraat", gent, Country.Belgium ,"77", "+32589657448", "sluis.willem@skynet.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-40", "Gent"),
                     new Member("Idris", "Roskam", new DateTime(2000, 08, 28), "Booischotsewag", gent, Country.Belgium ,"28", "+324896875210", "idris.roskam@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"),
                     new Member("Tervoort", "Djamel", new DateTime(1996, 11, 01), "Kapelstraat", adinkerke, Country.Belgium ,"12", "+32487541225", "Djamel.tervoort@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"),
                     new Member("Tom", "Nijs", new DateTime(1980, 01, 22), "Fietsstraat", bekegem, Country.Belgium ,"12", "+32487541225", "tom.nijs@gmial.com",dinDon, new DateTime(2014 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"),
                     new Member("Sofia", "Colpeart", new DateTime(1990, 10, 10), "Hoogstraat",brussel , Country.Belgium ,"100", "+32588754221", "colpeart.sofia@gmail.com",dinDon, new DateTime(2014 / 1 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-42", "Brussel"),
                     new Member("Jord", "Kroos", new DateTime(1997, 01, 06), "Steinstraat",nazareth , Country.Belgium ,"25", "+32499875421", "jord.kroos@gmail.com",dinDon, new DateTime(2019 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"),
                     new Member("Kristien", "Vandewalle", new DateTime(1960, 05, 20), "Muziekstraat",nazareth , Country.Belgium ,"45", "+32588748775", "kristien.vandewalle@gmail.com",dinDon, new DateTime(2019 / 2 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-42", "Gent"),
                     new Member("Koen", "Jansens", new DateTime(1966, 09, 22), "Sterrenbosstraat",nazareth , Country.Belgium ,"10", "+324998752214", "Koen.jansens@gmail.com",dinDon, new DateTime(2018 / 06 / 22), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"),
                     new Member("Robin", "De Groot", new DateTime(1996, 02, 26), "stationstraat",adinkerke, Country.Belgium ,"20", "+32687325741", "groot.robin@gmail.com",dinDon, new DateTime(2018 / 06 / 23), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"),
                     new Member("Nemo", "Donselaar", new DateTime(1995, 03, 18), "Linieweg",brussel, Country.Belgium ,"98", "+32875463220", "nemo.donselaar@gmail.com",dinDon, new DateTime(2014 / 05 / 30), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"),
                     new Member("Enrique", "Van Wetten", new DateTime(2002, 08, 30), "Koestraat",antwerpen, Country.Belgium ,"874", "+32499721771", "vanwetten.enrique@gmail.com",dinDon, new DateTime(2014 / 05 / 30), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent", "093859142", "kristien.bekkens@gmail.com"),
                     new Member("Jef", "Koppens", new DateTime(1999, 10, 12), "Rue du Chapy",brussel, Country.Belgium ,"351", "+32488512547", "jef.koppens@gmail.com",dinDon, new DateTime(2019 / 03 / 1), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent")






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
