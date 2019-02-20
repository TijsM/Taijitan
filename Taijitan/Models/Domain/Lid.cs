using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Lid
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Adres { get; set; }
        public int Telefoonnummer { get; set; }
        public string Email { get; set; }

        public Lid(string naam, string voornaam, DateTime geboortedatum, string adres, int telefoonnummer, string email)
        {
            Naam = naam;
            Voornaam = voornaam;
            Geboortedatum = geboortedatum;
            Adres = adres;
            Telefoonnummer = telefoonnummer;
            Email = email;
        }

        public void WijzigGegevens(string naam, string voornaam, DateTime geboortedatum, string adres, int telefoonnummer, string email)
        {
            Naam = naam;
            Voornaam = voornaam;
            Geboortedatum = geboortedatum;
            Adres = adres;
            Telefoonnummer = telefoonnummer;
            Email = email;
        }

    }
}
