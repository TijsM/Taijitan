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
                #region TrainingDays
                TrainingDay dinsdag = new TrainingDay("Dinsdag", 18.00, 20.00, DayOfWeek.Tuesday);
                TrainingDay woensdag = new TrainingDay("Woensdag", 14.00, 15.50, DayOfWeek.Wednesday);
                TrainingDay donderdag = new TrainingDay("Donderdag", 18.00, 20.00, DayOfWeek.Thursday);
                TrainingDay zaterdag = new TrainingDay("Zaterdag", 10.00, 11.50, DayOfWeek.Saturday);

                _dbContext.TrainingDays.AddRange(dinsdag, woensdag, donderdag, zaterdag);
                #endregion

                #region Formulas
                Formula dinDon = new Formula("dinsdag en donderdag", new List<TrainingDay> { dinsdag, donderdag });
                Formula dinZat = new Formula("dinsdag en zaterdag", new List<TrainingDay> { dinsdag, zaterdag });
                Formula woeZat = new Formula("woensdag en zaterdag", new List<TrainingDay> { woensdag, zaterdag });
                Formula woe = new Formula("woensdag", new List<TrainingDay> { woensdag });
                Formula zat = new Formula("zaterdag", new List<TrainingDay> { zaterdag });
                Formula activiteit = new Formula("deelname aan activiteit", new List<TrainingDay>());
                Formula stage = new Formula("deelname aan meerdaagse stage", new List<TrainingDay>());

                _dbContext.Formulas.AddRange(dinDon, dinZat, woeZat, woe, zat, activiteit, stage);
                #endregion

                #region Cities
                City bekegem = new City("8480", "Bekegem");
                City gent = new City("9000", "Gent");
                City nazareth = new City("9810", "Nazareth");
                City adinkerke = new City("8660", "Adinkerke");
                City antwerpen = new City("2000", "Antwerpen");
                City brussel = new City("1000", "Brussel");
                _dbContext.Cities.AddRange(bekegem, gent, brussel, nazareth, adinkerke, antwerpen); 
                #endregion

                #region Members
                IEnumerable<Member> members = new List<Member>
                {


                     new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, Country.Belgium, "16", "0492554616", "jarne.deschacht@student.hogent.be",dinDon, new DateTime(2016 / 01 / 30), Gender.Man, Country.Belgium, "09-08-1999.400-08", "Gent"){ Rank = Rank.Dan1},
                     new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, Country.Belgium, "Unknown", "0499721771", "tijs.martens@student.hogent.be",woe,  new DateTime(2014/06/2), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"){ Rank = Rank.Dan10},
                     new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", adinkerke, Country.Belgium, "Unknown", "0000000000", "robbe.dekien@student.hogent.be",dinZat, new DateTime(2016 / 05 / 30), Gender.Man, Country.Belgium, "02-06-1999.100-20", "Gent"){ Rank = Rank.Dan5},
                     new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Bijlokeweg", gent, Country.Belgium ,"73", "0000000000", "stef.verlinde@student.hogent.be",dinDon, new DateTime(2015 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-37", "Gent"){ Rank = Rank.Kyu1},
                     new Member("Swets", "Jefferyf", new DateTime(1997, 7, 01), "Stationstraat", gent, Country.Belgium ,"105", "+32458732447", "jeffrey.swets@hotmail.com",woe, new DateTime(2016 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-38", "Gent"){ Rank = Rank.Kyu2},
                     new Member("Middeljans", "Eef", new DateTime(1980, 02, 01), "Bergstraat", nazareth, Country.Belgium ,"20", "+32499875236", "eef.middeljans@gmail.com",zat, new DateTime(2017 / 02 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-39", "Antwerpen"){ Rank = Rank.Kyu4},
                     new Member("Van der Ende", "Yolanda", new DateTime(1980, 03, 18), "Rue de la Tannerie", bekegem, Country.Belgium ,"357", "0480822560", "YolandevanderEnde@rhyta.com",woeZat, new DateTime(2014 / 05 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Kyu5},
                     new Member("Velders", "Kressy", new DateTime(1985, 5, 16), "Passieweik", nazareth, Country.Belgium ,"21", "+32499721558", "krissyvelders@gmail.com",dinDon, new DateTime(2015 / 01 / 02), Gender.Women, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Dan5},
                     new Member("Sluis", "Willem", new DateTime(1995, 12, 12), "Hoekstraat", gent, Country.Belgium ,"77", "+32589657448", "sluis.willem@skynet.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Dan7},
                     new Member("Idris", "Roskam", new DateTime(2000, 08, 28), "Booischotsewag", gent, Country.Belgium ,"28", "+324896875210", "idris.roskam@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Dan8},
                     new Member("Tervoort", "Djamel", new DateTime(1996, 11, 01), "Kapelstraat", adinkerke, Country.Belgium ,"12", "+32487541225", "Djamel.tervoort@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Kyu2},
                     new Member("Tom", "Nijs", new DateTime(1980, 01, 22), "Fietsstraat", bekegem, Country.Belgium ,"12", "+32487541225", "tom.nijs@gmial.com",dinDon, new DateTime(2014 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Kyu4},
                     new Member("Sofia", "Colpeart", new DateTime(1990, 10, 10), "Hoogstraat",brussel , Country.Belgium ,"100", "+32588754221", "colpeart.sofia@gmail.com",dinDon, new DateTime(2014 / 1 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-42", "Brussel"){ Rank = Rank.Dan3},
                     new Member("Jord", "Kroos", new DateTime(1997, 01, 06), "Steinstraat",nazareth , Country.Belgium ,"25", "+32499875421", "jord.kroos@gmail.com",dinDon, new DateTime(2019 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan3},
                     new Member("Kristien", "Vandewalle", new DateTime(1960, 05, 20), "Muziekstraat",nazareth , Country.Belgium ,"45", "+32588748775", "kristien.vandewalle@gmail.com",dinDon, new DateTime(2019 / 2 / 18), Gender.Women, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan9},
                     new Member("Koen", "Jansens", new DateTime(1966, 09, 22), "Sterrenbosstraat",nazareth , Country.Belgium ,"10", "+324998752214", "Koen.jansens@gmail.com",dinDon, new DateTime(2018 / 06 / 22), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Kyu5},
                     new Member("Robin", "De Groot", new DateTime(1996, 02, 26), "stationstraat",adinkerke, Country.Belgium ,"20", "+32687325741", "groot.robin@gmail.com",dinDon, new DateTime(2018 / 06 / 23), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Kyu6},
                     new Member("Nemo", "Donselaar", new DateTime(1995, 03, 18), "Linieweg",brussel, Country.Belgium ,"98", "+32875463220", "nemo.donselaar@gmail.com",dinDon, new DateTime(2014 / 05 / 30), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan1},
                     new Member("Enrique", "Van Wetten", new DateTime(2002, 08, 30), "Koestraat",antwerpen, Country.Belgium ,"874", "+32499721771", "vanwetten.enrique@gmail.com",dinDon, new DateTime(2014 / 05 / 30), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent", "093859142", "kristien.bekkens@gmail.com"){ Rank = Rank.Dan9},
                     new Member("Jef", "Koppens", new DateTime(1999, 10, 12), "Rue du Chapy",brussel, Country.Belgium ,"351", "+32488512547", "jef.koppens@gmail.com",dinDon, new DateTime(2019 / 03 / 1), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan12}
                };
                #endregion

                #region Teachers
                IEnumerable<Teacher> teachers = new List<Teacher>
                {
                     new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", gent, Country.Belgium, "1", "+23456987447", "teacher@taijitan.be" , new DateTime(2005/01/30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent")


                };
                #endregion

                #region Admins
                IEnumerable<Admin> admins = new List<Admin>
                {
                     new Admin("Admin", "Administrator", new DateTime(1980, 1, 15), "StationStraat", nazareth, Country.Belgium, "15", "+3249981557", "admin@taijitan.be",  new DateTime(2005/01/30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"),

                };
                #endregion

                #region CourseMaterial

                #region Rang 1
                Image img_1_1_01 = new Image("1_1_01", "Foto 1 bij eerste les");
                Image img_1_1_02 = new Image("1_1_02", "Foto 2 bij eerste les");
                Image img_1_1_03 = new Image("1_1_03", "Foto 3 bij eerste les");

                Image img_1_2_01 = new Image("1_2_01", "Foto 1 bij tweede les");
                Image img_1_2_02 = new Image("1_2_02", "Foto 2 bij tweede les");
                Image img_1_2_03 = new Image("1_2_03", "Foto 3 bij tweede les");

                Image img_1_3_01 = new Image("1_3_01", "Foto 1 bij derde les");
                Image img_1_3_02 = new Image("1_3_02", "Foto 2 bij derde les");
                Image img_1_3_03 = new Image("1_3_03", "Foto 3 bij derde les");

                CourseMaterial mat_1_1 = new CourseMaterial(Rank.Kyu6, "https://www.youtube.com/watch?v=3PEj8_0Q9Qo", "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en" +
                    " zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker" +
                    " een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen" +
                    " overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair" +
                    " geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing " +
                    "software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
                    new List<Image> { img_1_1_01, img_1_1_02, img_1_1_03 },
                    "Voorbeeld1");
                CourseMaterial mat_1_2 = new CourseMaterial(Rank.Kyu6, "https://www.youtube.com/watch?v=hY35pBOfSNk", "door de tekstuele inhoud. Het belangrijke punt van het gebruik van" +
                    " Lorem Ipsum is dat het uit een min of meer normale verdeling van letters bestaat, in tegenstelling tot wat het tot min" +
                    " of meer leesbaar nederlands maakt. Veel desktop publishing pakketten en web pagina editors gebruiken tegenwoordig Lorem" +
                    " Ipsum als hun standaard model tekst, en een zoekopdracht naar ontsluit veel websites die nog in aanbouw zijn. " +
                    "Verscheidene versies hebben zich ontwikkeld in de loop van de jaren, soms per ongeluk soms expres (ingevoegde humor en" +
                    " dergelijke).",
                    new List<Image> { img_1_2_01, img_1_2_02, img_1_2_03 },
                    "voorbeeld2");
                CourseMaterial mat_1_3 = new CourseMaterial(Rank.Kyu6, "https://www.youtube.com/watch?v=D9LL_ivHTXc", "Er zijn vele variaties van passages van Lorem Ipsum beschikbaar maar" +
                    " het merendeel heeft te lijden gehad van wijzigingen in een of andere vorm, door ingevoegde humor of willekeurig gekozen" +
                    " woorden die nog niet half geloofwaardig ogen. Als u een passage uit Lorum Ipsum gaat gebruiken dient u zich ervan te " +
                    "verzekeren dat er niets beschamends midden in de tekst verborgen zit. Alle Lorum Ipsum generators op Internet hebben de " +
                    "eigenschap voorgedefinieerde stukken te herhalen waar nodig zodat dit de eerste echte generator is op internet. Het " +
                    "gebruikt een woordenlijst van 200 latijnse woorden gecombineerd met een handvol zinsstructuur modellen om een Lorum Ipsum " +
                    "te genereren die redelijk overkomt. De gegenereerde Lorum Ipsum is daardoor altijd vrij van herhaling, ingevoegde humor of" +
                    " ongebruikelijke woorden etc.",
                    new List<Image> { img_1_3_01, img_1_3_02, img_1_3_03 },
                    "voorbeeld3"); 
                #endregion

                

                _dbContext.CourseMaterials.AddRange(mat_1_1, mat_1_2, mat_1_3);

                #endregion

                #region Give users an account in Identity
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
                #endregion

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
