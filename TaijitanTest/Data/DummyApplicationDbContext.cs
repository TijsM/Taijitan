using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;

namespace TaijitanTest.Data
{
    public class DummyApplicationDbContext
    {
        #region Fields
        private readonly City _gent;
        private readonly City _antwerpen;
        private readonly City _brussel;
        private readonly IEnumerable<User> _users;
        private readonly Member _jarne;
        private readonly Member _robbe;
        private readonly Member _stef;
        private readonly Member _tijs;

        private readonly Teacher _teacher1;
        #endregion

        #region Properties
        public IEnumerable<City> Cities => new List<City> { _gent, _antwerpen, _brussel };
        public IEnumerable<User> Users => _users;
        public IEnumerable<Member> UsersFormula1 { get; set; }
        public Member UserTomJansens { get;  }
        public City TomJansensCity { get; set; }
        public IEnumerable<User> UsersByPartOfName { get; set; }
        public Session Session1 { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<Formula> _formulas { get; set; }
        public IEnumerable<Member> _members { get; set; }
        public Admin Alain { get; set; }
        #endregion



        #region Constructors
        public DummyApplicationDbContext()
        {
            _gent = new City("9000", "Gent");
            _antwerpen = new City("2000", "Antwerpen");
            _brussel = new City("1000", "Brussel");
            TomJansensCity = _gent;


            TrainingDay dinsdag = new TrainingDay("Dinsdag", 18.00, 20.00, DayOfWeek.Tuesday);
            TrainingDay woensdag = new TrainingDay("Woensdag", 14.00, 15.50, DayOfWeek.Wednesday);
            TrainingDay donderdag = new TrainingDay("Donderdag", 18.00, 20.00, DayOfWeek.Thursday);
            TrainingDay zaterdag = new TrainingDay("Zaterdag", 10.00, 11.50, DayOfWeek.Saturday);
            TrainingDay zondag = new TrainingDay("Zondag", 11.00, 12.50, DayOfWeek.Sunday);

            Formula dinDon = new Formula("dinsdag en donderdag", new List<TrainingDay> { dinsdag, donderdag });
            Formula dinZat = new Formula("dinsdag en zaterdag", new List<TrainingDay> { dinsdag, zaterdag });
            Formula woeZat = new Formula("woensdag en zaterdag", new List<TrainingDay> { woensdag, zaterdag });
            Formula woe = new Formula("woensdag", new List<TrainingDay> { woensdag });
            Formula zat = new Formula("zaterdag", new List<TrainingDay> { zaterdag });
            Formula activiteit = new Formula("deelname aan activiteit", new List<TrainingDay>());
            Formula stage = new Formula("deelname aan meerdaagse stage", new List<TrainingDay>());

            _formulas = new List<Formula>
            {
                dinDon, dinZat, woeZat, woe, zat/*, activiteit, stage*/
            };



            _tijs = new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", _brussel, Country.Belgium, "Unknown", "0499721771", "tijs.martens@student.hogent.be", dinDon, new DateTime(2014/06/2), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent");
            _stef = new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Unknown", _antwerpen, Country.Belgium, "Unknown", "0000000000", "stef.verlinde@student.hogent.be", dinZat, new DateTime(2015 / 08 / 4), Gender.Man, Country.Belgium, "02-08-1998.306-37", "Gent");
            _jarne = new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", _gent, Country.Belgium, "16", "0492554616", "jarne.deschacht@student.hogent.be", woeZat, new DateTime(2016 / 01 / 30), Gender.Man, Country.Belgium, "09-08-1999.400-08", "Gent");
            _robbe = new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", _gent, Country.Belgium, "Unknown", "0000000000", "robbe.dekien@student.hogent.be", woe, new DateTime(2016 / 05 / 30), Gender.Man, Country.Belgium, "02-06-1999.100-20", "Gent");
            UserTomJansens = new Member("Jansens", "Tom", new DateTime(1999, 8, 9), "Hoogstraat", _gent, Country.Belgium, "8", "+32499854775", "tom.jansens@gmail.com",woeZat, new DateTime(2017 / 05 / 18), Gender.Man, Country.Belgium, "09-08-1999.400-09", "Gent");
            Alain = new Admin("Alain", "Vandamme", new DateTime(1980, 1, 15), "StationStraat", _antwerpen, Country.Belgium, "15", "+3249981557", "alain.vandamma@synalco.be", new DateTime(2005 / 01 / 30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent");

            _teacher1 = new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", _gent, Country.Belgium, "1", "+23456987447", "jacky.chan@hollywood.com", new DateTime(2005 / 01 / 30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent");

            _users = new List<User>
            {
                UserTomJansens,Alain,
                _robbe,_jarne,_stef,_tijs,_teacher1

            };

            _members = new List<Member>
            {
                _tijs, _stef, _jarne, _robbe
            };

            UsersFormula1 = new List<Member> {
                _robbe,_jarne,UserTomJansens
            };
            UsersByPartOfName = new List<Member> {
                _robbe,_jarne,_stef
            };
            Session1 = new Session(_formulas, _teacher1, _members);
            Sessions = new List<Session> { Session1 };
        }
        #endregion
    }
}
