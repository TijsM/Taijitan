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

                Member jarne = new Member("Deschacht", "Jarne", new DateTime(1999, 8, 9), "Zilverstraat", bekegem, "Belgium", "16", 0492554616, "jarne.deschacht@student.hogent.be");
                _dbContext.Members.Add(jarne);
                Member tijs = new Member("Martens", "Tijs", new DateTime(1999, 6, 14), "Unknown", nazareth, "Belgium", "Unknown", 0499721771, "tijs.martens@student.hogent.be");
                _dbContext.Members.Add(tijs);
                Member robbe = new Member("Dekien", "Robbe", new DateTime(1998, 8, 26), "Unknown", adinkerke, "Belgium", "Unknown", 0000000000, "robbe.dekien@student.hogent.be");
                _dbContext.Members.Add(robbe);
                Member stef = new Member("Verlinde", "Stef", new DateTime(1999, 4, 25), "Unknown", gent, "Belgium", "Unknown", 0000000000, "stef.verlinde@student.hogent.be");
                _dbContext.Members.Add(stef);
                await CreateUser("admin@taijitan.be", "admin@taijitan.be", "P@ssword1", "Admin");
                await CreateUser("lid@taijitan.be", "admin@taijitan.be", "P@ssword1", "Admin");
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
