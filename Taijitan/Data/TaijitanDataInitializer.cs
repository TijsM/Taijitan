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
                     new Member("Middeljans", "Eef", new DateTime(1980, 02, 01), "Bergstraat", nazareth, Country.Belgium ,"20", "+32499875236", "eef.middeljans@gmail.com",zat, new DateTime(2017 / 02 / 18), Gender.Vrouw, Country.Belgium, "02-08-1998.306-39", "Antwerpen"){ Rank = Rank.Kyu4},
                     new Member("Van der Ende", "Yolanda", new DateTime(1980, 03, 18), "Rue de la Tannerie", bekegem, Country.Belgium ,"357", "0480822560", "YolandevanderEnde@rhyta.com",woeZat, new DateTime(2014 / 05 / 18), Gender.Vrouw, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Kyu5},
                     new Member("Velders", "Kressy", new DateTime(1985, 5, 16), "Passieweik", nazareth, Country.Belgium ,"21", "+32499721558", "krissyvelders@gmail.com",dinDon, new DateTime(2015 / 01 / 02), Gender.Vrouw, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Dan5},
                     new Member("Sluis", "Willem", new DateTime(1995, 12, 12), "Hoekstraat", gent, Country.Belgium ,"77", "+32589657448", "sluis.willem@skynet.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-40", "Gent"){ Rank = Rank.Dan7},
                     new Member("Idris", "Roskam", new DateTime(2000, 08, 28), "Booischotsewag", gent, Country.Belgium ,"28", "+324896875210", "idris.roskam@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Dan8},
                     new Member("Tervoort", "Djamel", new DateTime(1996, 11, 01), "Kapelstraat", adinkerke, Country.Belgium ,"12", "+32487541225", "Djamel.tervoort@gmail.be",dinDon, new DateTime(2010 / 11 / 08), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Kyu2},
                     new Member("Tom", "Nijs", new DateTime(1980, 01, 22), "Fietsstraat", bekegem, Country.Belgium ,"12", "+32487541225", "tom.nijs@gmial.com",dinDon, new DateTime(2014 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Antwerpen"){ Rank = Rank.Kyu4},
                     new Member("Sofia", "Colpeart", new DateTime(1990, 10, 10), "Hoogstraat",brussel , Country.Belgium ,"100", "+32588754221", "colpeart.sofia@gmail.com",dinDon, new DateTime(2014 / 1 / 18), Gender.Vrouw, Country.Belgium, "02-08-1998.306-42", "Brussel"){ Rank = Rank.Dan3},
                     new Member("Jord", "Kroos", new DateTime(1997, 01, 06), "Steinstraat",nazareth , Country.Belgium ,"25", "+32499875421", "jord.kroos@gmail.com",dinDon, new DateTime(2019 / 1 / 12), Gender.Man, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan3},
                     new Member("Kristien", "Vandewalle", new DateTime(1960, 05, 20), "Muziekstraat",nazareth , Country.Belgium ,"45", "+32588748775", "kristien.vandewalle@gmail.com",dinDon, new DateTime(2019 / 2 / 18), Gender.Vrouw, Country.Belgium, "02-08-1998.306-42", "Gent"){ Rank = Rank.Dan9},
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
                     new Admin("Gracie", "Rickson", new DateTime(1980, 1, 15), "StationStraat", nazareth, Country.Belgium, "15", "+3249981557", "admin@taijitan.be",  new DateTime(2005/01/30), Gender.Man, Country.Belgium, "14-06-1999.306-37", "Gent"),

                };
                #endregion

                #region CourseMaterial
                Image NoImage = new Image("Ninja3", "Er is geen afbeelding voor dit onderdeel");
                #region kyu6
                #region Vorderen
                Image vorderen_1 = new Image("/Vorderen/Vorderen_1", "Foto bij vorderen");

                CourseMaterial Vorderen = new CourseMaterial(Rank.Kyu6
                    , " "
                    , "Het vorderen naar voor gebeurt zonder de stand of de positie van het bovenlichaam te veranderen en door met de heupen op één lijn snel naar voor te stoten. Breng de voet van het achterste been naast de voet van het afzetbeen (voorste been waar overwegend het gewicht op rust). Het oude achterste been glijdt schuin naar voor terwijl de knie van het afzetbeen gestrekt wordt en de voetzool, vooral de hiel, krachtig tegen de grond gedrukt wordt (waardoor de voet 30° draait). Tijdens de verplaatsing wordt het lichaamsgewicht van de ene voet naar de andere verplaatst. Maak een draaibeweging zodat het vorderen in de tegenovergestelde richting opnieuw kan worden uitgevoerd (doe dit na herhaaldelijk de vorderbeweging te hebben uitgevoerd). Maak de draaibeweging door het achterste been bij te trekken, het lichaam 180° terug te draaien en het oude achterste been naar voor te brengen. De draaibeweging kan/mag ook worden uitgevoerd door het voorste been (afzetbeen) naar het achterste been te brengen, 180° te draaien en het oude voorste been verder diagonaal terug naar voor te brengen. Achteruit vorderen moet eveneens ingeoefend worden."
                    , new List<Image> { vorderen_1 }
                    , "Basistand - Vorderen"
                    );
                #endregion
                #region voorwaardse stand
                Image voorwaartse_stand_1 = new Image("/VoorwaartseStand/voorwaartse_stand_1", "Foto 1 bij voachterwaartseStand");
                Image voorwaartse_stand_2 = new Image("/VoorwaartseStand/voorwaartse_stand_2", "Foto 2 bij voachterwaartseStand");
                Image voorwaartse_stand_3 = new Image("/VoorwaartseStand/voorwaartse_stand_3", "Foto 3 bij voachterwaartseStand");

                CourseMaterial VoorwaarteStand = new CourseMaterial(Rank.Kyu6, "https://www.youtube.com/embed/OFaR7tlcKN0", "De benen zijn gespreid en" +
                  " staan in een rechthoekige driehoek (zie tekening). Het achterste been is gestrekt en het voorste been is gebogen zodat de knie " +
                  "zich recht boven de voet bevindt. De voorste voet staat recht naar voor en de achterste voet staat schuin op 30°. " +
                  "De heupen zijn omlaag gebracht, het bovenlichaam staat loodrecht ten opzichte van de grond en is naar voor gericht. " +
                  "Het gezicht is recht naar voor. Ongeveer 60% van het gewicht rust op het voorste en 40% op het achterste been.",
                  new List<Image> { voorwaartse_stand_1, voorwaartse_stand_2, voorwaartse_stand_3 },
                  "Basistand - Voorwaartse stand");
                #endregion
                #region achterwaartse stand
                Image achterwaartse_stand_1 = new Image("/AchterwaartseStand/achterwaartse_stand_1", "Foto 1 bij achterwaartseStand");
                Image achterwaartse_stand_2 = new Image("/AchterwaartseStand/achterwaartse_stand_2", "Foto 2 bij achterwaartseStand");
                Image achterwaartse_stand_3 = new Image("/AchterwaartseStand/achterwaartse_stand_3", "Foto 3 bij achterwaartseStand");

                CourseMaterial AchterwaartseStand = new CourseMaterial(Rank.Kyu6
                    , "https://www.youtube.com/embed/OFaR7tlcKN0"
                    , "De benen zijn gespreid " +
                    "en staan op één lijn. De knie van het achterste been is sterk gebogen, naar buiten gedraaid en bevindt zich recht boven de voet." +
                    " Het voorste been is licht gebogen. De voorste voet staat recht naar voor en de achterste voet staat schuin op 90° in T of L stand." +
                    " De heupen zijn omlaag gebracht, het bovenlichaam staat loodrecht ten opzichte van de grond en is half weggedraaid. " +
                    "Het gezicht is recht naar voor. Ongeveer 30% van het gewicht rust op het voorste en 70% op het achterste been.",
                    new List<Image> { achterwaartse_stand_1, achterwaartse_stand_2, achterwaartse_stand_3 },
                    "Basistand - Achterwaartse stand");
                #endregion
                #region natuurlijke standen
                Image natuurlijke_stand_1 = new Image("/NatuurlijkeStand/Natuurlijke_stand_1", "Foto 1 bij natuurlijke stand");

                CourseMaterial NatuurlijkeStand = new CourseMaterial(
                    Rank.Kyu6
                    , " "
                    , "In de natuurlijke houding is het lichaam ontspannen, maar in een toestand van waakzaamheid, klaar om iedere situatie het hoofd te bieden. De knieën moeten altijd soepel en ontspannen blijven zodat snel iedere gewenste positie kan ingenomen worden. De stand van de voeten varieert in de verschillende vormen.Mogelijke uitvoering:Start met de voeten bij elkaar en buig een weinig door de knieën(informele militaire stand), houd de hielen dicht en breng de voeten op 45 graden(informele militaire stand met de voeten naar buiten), plaats de rechtervoet 30cm zijwaarts(spreidstand), breng de tenen naar binnen(spreidstand met de tenen naar binnen), breng de tenen recht(evenwijdige voetenstand).Breng de rechtervoet achter de linkervoet(T - stand), voer eerst de grote T - stand uit(voeten staan 30cm uit elkaar) gevolgd door de kleine T - stand(hiel en voetbrug staan tegen elkaar).Keer terug naar een evenwijdige voetenstand en voer dezelfde standen links uit.Ga opnieuw in de evenwijdige voetenstand staan, breng de rechtervoet achter de linkervoet(L - stand), voer eerst de grote L - stand uit(voeten staan 30cm uit elkaar) gevolgd door de kleine L - stand(hiel en hiel staan tegen elkaar).Keer terug naar een evenwijdige voetenstand en voer dezelfde standen links uit.Keer terug naar een evenwijdige voetenstand, breng de voeten bij elkaar en groet af."
                    , new List<Image> { natuurlijke_stand_1 }
                    , "Basistand - Natuurlijke stand");
                #endregion
                #region basisslagenALgemeen
                Image basislag_algemeen_1 = new Image("/Basisslagen_algemeen/BasisSlagenAlgemeen_1", "Foto 1 bij basisslagen");

                CourseMaterial BasisSlagenAlgemeen = new CourseMaterial(
                    Rank.Kyu6
                    , " "
                    , "Een goede stoot begint vanuit een ontspannen uitgangshouding (de kiba dashi stand) waarbij er geen onnodige spierspanningen mogen zijn. Er wordt gestart met de rechtervuist in de heup en met de andere vuist gestrekt voor het lichaam en in het verlengde van de neus."
                    , new List<Image> { basislag_algemeen_1 }
                    , "Basisslag - algemeen");
                #endregion
                #region voorwaartse stoot
                Image voorwaartste_stoot_1 = new Image("/Voorwaartse_stoot/VoorwaartseStoot_1", "Foto 1 bij voorwaartse stoot");
                Image voorwaartste_stoot_2 = new Image("/Voorwaartse_stoot/VoorwaartseStoot_2", "Foto 2 bij voorwaartse stoot");

                CourseMaterial VoorwaartseStoot = new CourseMaterial(
                    Rank.Kyu6
                    , " "
                    , "De elleboog van de stootarm moet licht langs het lichaam strijken en de onderarm moet naar binnen draaien. De arm maakt een voorwaartse rechtlijnige beweging. De vuist moet stevig gebald zijn en beschrijft tijdens de beweging een halve cirkel. Raak met het voorste deel van de vuist, met name de knokkels van de wijs- en middenvinger, de plexus. De andere arm wordt zo snel mogelijk terug naar de heup getrokken."
                    , new List<Image> { voorwaartste_stoot_1, voorwaartste_stoot_2 }
                    , "Basisslag - voorwaartse stoot");
                #endregion
                #region Omhooggaande stoot
                Image omhooggaande_stoot_1 = new Image("Omhooggaande_stoot/Omhooggaande_stoot_1", "Foto 1 bij omhooggaande stoot");
                Image omhooggaande_stoot_2 = new Image("Omhooggaande_stoot/Omhooggaande_stoot_2", "Foto 2 bij omhooggaande stoot");
                Image omhooggaande_stoot_3 = new Image("Omhooggaande_stoot/Omhooggaande_stoot_3", "Foto 3 bij omhooggaande stoot");

                CourseMaterial OmhooggaandeStoot = new CourseMaterial(
                    Rank.Kyu6
                    , ""
                    , "De elleboog van de stootarm moet licht langs het lichaam strijken en de onderarm moet naar binnen draaien. De arm maakt een omhooggaande rechtlijnige beweging. De vuist moet stevig gebald zijn en beschrijft tijdens de beweging een halve cirkel. Raak met het voorste deel van de vuist, met name de knokkels van de wijs- en middenvinger, de strot of het gezicht. De andere arm wordt zo snel mogelijk terug naar de heup getrokken."
                    , new List<Image> { omhooggaande_stoot_1, omhooggaande_stoot_2, omhooggaande_stoot_3 }
                    , " Basisslag - omhooggaande stoot");
                #endregion
                #region verticale stoot
                Image verticale_stoot_1 = new Image("Verticale_stoot/Verticale_stoot_1", "Foto 1 bij verticale stoot");
                Image verticale_stoot_2 = new Image("Verticale_stoot/Verticale_stoot_2", "Foto 2 bij verticale stoot");
                Image verticale_stoot_3 = new Image("Verticale_stoot/Verticale_stoot_3", "Foto 3 bij verticale stoot");

                CourseMaterial VerticaleStoot = new CourseMaterial(
                    Rank.Kyu6
                    , " "
                    , "De elleboog van de stootarm moet licht langs het lichaam strijken en de onderarm moet naar binnen draaien. De arm maakt een voorwaartse of omhooggaande rechtlijnige beweging. De vuist moet stevig gebald zijn en beschrijft tijdens de beweging een kwart van een cirkel. Raak met het voorste deel van de vuist, met name de knokkels van de wijs- en middenvinger, de plexus, de strot of het gezicht. De andere arm wordt zo snel mogelijk terug naar de heup getrokken."
                    , new List<Image> { verticale_stoot_1, verticale_stoot_2, verticale_stoot_3 }
                    , "Basisslag - verticale stoot"
                    );
                #endregion
                #region Hoekstoot
                Image hoek_stoot_1 = new Image("hoek_stoot/Hoek_stoot_1", "Foto 1 bij hoekstoot");
                Image hoek_stoot_2 = new Image("hoek_stoot/Hoek_stoot_2", "Foto 2 bij hoekstoot");
                Image hoek_stoot_3 = new Image("hoek_stoot/Hoek_stoot_3", "Foto 3 bij hoekstoot");
                Image hoek_stoot_4 = new Image("hoek_stoot/Hoek_stoot_4", "Foto 4 bij hoekstoot");

                CourseMaterial HoekStoot = new CourseMaterial(
                    Rank.Kyu6,
                    " "
                    , "De elleboog van de stootarm moet licht langs het lichaam strijken en de onderarm moet naar binnen draaien. De arm maakt een voorwaartse hoekbeweging. De vuist moet stevig gebald zijn en beschrijft tijdens de beweging een halve cirkel. Raak met het voorste deel van de vuist (de elleboog is volledig geplooid), met name de knokkels van de wijs- en middenvinger, de kin, de slaap of de plexus. De andere arm wordt zo snel mogelijk terug naar de heup getrokken."
                    , new List<Image> { hoek_stoot_1, hoek_stoot_2, hoek_stoot_3, hoek_stoot_4 }
                    , "Basisslag - hoek stoot");
                #endregion

                _dbContext.CourseMaterials.AddRange(
                    Vorderen
                    , VoorwaarteStand
                    , AchterwaartseStand
                    , NatuurlijkeStand
                    , BasisSlagenAlgemeen
                    , VoorwaartseStoot
                    , OmhooggaandeStoot
                    , VerticaleStoot
                    , HoekStoot);
                #endregion

                #region kyu5
                #region Paarzitstand
                Image paardzit_stand_1 = new Image("PaardZitStand/PaardZitStand_1", "Foto 1 bij paardzitstand");
                Image paardzit_stand_2 = new Image("PaardZitStand/PaardZitStand_2", "Foto 2 bij paardzitstand");

                CourseMaterial PaardZitStand = new CourseMaterial(
                    Rank.Kyu5,
                    " "
                    , "Geen uitleg beschikbaar"
                    , new List<Image> { paardzit_stand_1, paardzit_stand_2 },
                    "Basisstand - Paardzitstand");
                #endregion

                #region Hurkstand
                Image hurk_stand_1 = new Image("HurkStand/HurkStand_1", "Foto 1 bij hurkstand");
                Image hurk_stand_2 = new Image("HurkStand/HurkStand_2", "Foto 2 bij hurkstand");

                CourseMaterial HurkStand = new CourseMaterial(
                    Rank.Kyu5
                    , " "
                    , "Geen uitleg beschikbaar"
                    , new List<Image> { hurk_stand_1, hurk_stand_2 }
                    , "Basisstand - Hurkstand");


                #endregion

                #region VerankerdeStand
                Image verankerde_stand_1 = new Image("VerankerdeStand/VerankerdeStand_1", "Foto 1 bij verankerde stand");
                Image verankerde_stand_2 = new Image("VerankerdeStand/VerankerdeStand_2", "Foto 2 bij verankerde stand");

                CourseMaterial VerankerdeStand = new CourseMaterial(
                    Rank.Kyu5,
                    " "
                    , "Geen uitleg beschikbaar",
                    new List<Image> { verankerde_stand_1, verankerde_stand_2 }
                    , "Basisstand - Verankerde stand");
                #endregion

                #region HandpalmHielslag
                Image handpalm_hielslag_1 = new Image("HandpalmHielslag/HandpalmHielslag_1", "Foto 1 bij handpalm hielslag");

                CourseMaterial HandpalmHielslag = new CourseMaterial(
                    Rank.Kyu5
                    , " "
                    , "Geen uitleg beschikbaar"
                    , new List<Image> { handpalm_hielslag_1 }
                    , "Basisslag - Handpalm hielslag");
                #endregion

                _dbContext.CourseMaterials.AddRange(
                    PaardZitStand,
                    HurkStand,
                    VerankerdeStand
                    , HandpalmHielslag);

                #endregion

                #region Kyu2

                #region slagenk2

                CourseMaterial SlagenKyu2 = new CourseMaterial(
                    Rank.Kyu2
                    , ""
                    , "1.1.	Aanval: De aanvaller geeft een rechte vuiststoot naar jouw gezicht.Reactie: Weer de aanval van binnen naar buiten af gevolgd door een diagonale uppercut.Keer terug met een empi in de nek.1.2.Aanval: De aanvaller geeft een rechte vuiststoot naar jouw gezicht.Reactie: Weer de aanval van binnen naar buiten af gevolgd door een swing. Keer terug met een empi in de nek 1.3.Aanval: De aanvaller geeft een rechte vuiststoot naar jouw gezicht. Reactie: Weer de aanval van buiten naar binnen af. Houd met het rechterhand controle over de pols van de aanvaller en vervolg met een gedraaide horizontale omgekeerde vuistslag met het linkerhand. Neem met het linkerhand de pols over en zet een polsklem aan en plaats de rechterhand op de elleboog van de aanvaller. Voer een drukking uit richting de oksel van de aanvaller. 1.4.Aanval: De aanvaller geeft een rechte vuiststoot naar jouw gezicht.Reactie: Weer de aanval van binnen naar buiten af gevolgd door een uppercut recht naar boven, terugkeren met een empi naar de plexus, een tweede empi in de ribben en afwerken met elleboogbreuk door een tezelfdertijd een slag te geven op de bovenarm en voorarm"
                    , new List<Image> { NoImage }
                    , "slagen");
                #endregion

                _dbContext.CourseMaterials.AddRange(
                    SlagenKyu2
                    );
                #endregion




                #endregion

                #region Comment

               Comment comment1 = new Comment("Dit is een test", VoorwaarteStand, members.First());
                Comment comment2 = new Comment("Dit is een tweede test, hier komt de effectieve commentaar die de gebruiker heeft ingegevne", AchterwaartseStand, members.First());
                Comment comment3 = new Comment("Dit is een derde test, hier komt de effectieve commentaar die de gebruiker heeft ingegevne", AchterwaartseStand, members.First());
                Comment comment4 = new Comment("Dit is een vierde test, hier komt de effectieve commentaar die de gebruiker heeft ingegevne", VoorwaarteStand, members.First());
                Comment comment5 = new Comment("Dit is een vijfde test, hier komt de effectieve commentaar die de gebruiker heeft ingegevne", VoorwaarteStand, members.First());
                Comment comment6 = new Comment("Dit is een zesde test, hier komt de effectieve commentaar die de gebruiker heeft ingegevne", VoorwaarteStand, members.First());

                _dbContext.Comments.AddRange(comment1, comment2, comment3, comment4, comment5, comment6);
                //_dbContext.Comments.Add(comment1);
                ////_dbContext.Comments.Add(comment2);
                ////_dbContext.Comments.Add(comment3);
                ////_dbContext.Comments.Add(comment4);

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
