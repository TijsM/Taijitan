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
        #endregion

        #region Properties
        public IEnumerable<City> cities => new List<City> { _gent, _antwerpen, _brussel };
        public IEnumerable<User> Users => _users;
        public User UserTomJansens { get;  }
        #endregion

        #region Constructors
        public DummyApplicationDbContext()
        {
            _gent = new City("9000", "Gent");
            _antwerpen = new City("2000", "Antwerpen");
            _brussel = new City("1000", "Brussel");

            
            UserTomJansens = new Admin("Tom", "Jansens", new DateTime(1999, 8, 9), "Hoogstraat", _gent, "Belgie", "8", "+32499854775", "tom.jansens@gmail.com");

            _users = new List<User>
            {
                new Admin("Alain", "Vandamme", new DateTime(1980, 1, 15), "StationStraat", _antwerpen, "Belgie", "15", "+3249981557", "alain.vandamma@synalco.be"),

                new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", _gent, "Belgie", "16", "0492554616", "jarne.deschacht@student.hogent.be",Formula.Formule1),
                new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", _brussel, "Belgie", "Unknown", "0499721771", "tijs.martens@student.hogent.be", Formula.Formule4),
                new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Garzebekeveldstraat", _gent, "Belgie", "Unknown", "0000000000", "robbe.dekien@student.hogent.be", Formula.Formule1),
                new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Unknown", _antwerpen, "Belgie", "Unknown", "0000000000", "stef.verlinde@student.hogent.be", Formula.Formule4),

                new Teacher("Chan", "Jacky", new DateTime(1960, 10, 18), "HongkongStreet", _gent, "Belgie", "1", "+23456987447", "jacky.chan@hollywood.com" )

            };
        }

        #endregion
    }
}
