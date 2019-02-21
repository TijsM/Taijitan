using System;
using System.Text.RegularExpressions;

namespace Taijitan.Models.Domain {
    public class City {
        #region Properties
        public string Postalcode { get; set; }
        public string Name { get; set; }
        #endregion

        #region Contstructors
        public City(string postalcode, string name) {
            Postalcode = postalcode;
            Name = name;
        }
        #endregion
    }
}