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
        private readonly IList<User> _users;
        private readonly Member _jarne;
        private readonly Member _robbe;
        private readonly Member _stef;
        private readonly Member _tijs;
        #endregion

        #region Properties
        public IEnumerable<City> Cities => new List<City> { _gent, _antwerpen, _brussel };
        public IEnumerable<User> Users => _users;
        public IEnumerable<Member> UsersFormula1 { get; set; }
        public Member UserTomJansens { get;  }
        public City TomJansensCity { get; set; }
        public IEnumerable<User> UsersByPartOfName { get; set; }
      
        #endregion

        #region Constructors
        public DummyApplicationDbContext()
        {
            _gent = new City("9000", "Gent");
            _antwerpen = new City("2000", "Antwerpen");
            _brussel = new City("1000", "Brussel");
            TomJansensCity = _gent;
            _tijs = new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", _brussel, "Belgie", "Unknown", "0499721771", "tijs.martens@student.hogent.be", Formula.Formule4);
            _stef = new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Unknown", _antwerpen, "Belgie", "Unknown", "0000000000", "stef.verlinde@student.hogent.be", Formula.Formule4);
            _jarne = new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", _gent, "Belgie", "16", "0492554616", "jarne.deschacht@student.hogent.be", Formula.Formule1);
            _robbe = new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", _gent, "Belgie", "Unknown", "0000000000", "robbe.dekien@student.hogent.be", Formula.Formule1);
            UserTomJansens = new Member("Jansens", "Tom", new DateTime(1999, 8, 9), "Hoogstraat", _gent, "Belgie", "8", "+32499854775", "tom.jansens@gmail.com",Formula.Formule1);

            _users = new List<User>
            {
                UserTomJansens,
                new Admin("Alain", "Vandamme", new DateTime(1980, 1, 15), "StationStraat", _antwerpen, "Belgie", "15", "+3249981557", "alain.vandamma@synalco.be"),
                _robbe,_jarne,_stef,_tijs,

                new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", _gent, "Belgie", "1", "+23456987447", "jacky.chan@hollywood.com" )


            };

            UsersFormula1 = new List<Member> {
                _robbe,_jarne,UserTomJansens
            };
            UsersByPartOfName = new List<Member> {
                _robbe,_jarne,_stef
            };
        }

        #endregion
    }
}
