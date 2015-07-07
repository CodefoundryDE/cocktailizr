using Cocktailizr.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using Cocktailizr.Security;
using CocktailizrTypes.Model.Entities;

namespace Cocktailizr.Model.Service
{
    public class BenutzerService
    {
        private CocktailizrDataContext _context;

        public BenutzerService()
        {
            _context = new CocktailizrDataContext();
        }

        public async Task<bool> CredentialsOk(string userName, string password)
        {
            var task = await _context.Benutzer.FindAsync(x => x.Name.Equals(userName));
            var benutzer = (await task.ToListAsync()).First();

            return PasswordHashHelper.VerifyPassword(benutzer.HashedPassword, password);
        }
    }
}