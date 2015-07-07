using Cocktailizr.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using CocktailizrTypes.Model.Entities;

namespace Cocktailizr.Model.Service
{
    public class BenutzerService
    {
        private CocktailizrDataContext _context;

        public BenutzerService()
        {
            _context = new CocktailizrDataContext();
            _context.Benutzer.InsertOneAsync(new Benutzer() {Name = "test", HashedPassword = "test123"}).Wait();
        }

        public async Task<bool> CredentialsOk(string userName, string password)
        {
            var task = await _context.Benutzer.FindAsync(x => x.Name.Equals(userName) && x.HashedPassword.Equals(password));
            var benutzer = await task.ToListAsync();
            return benutzer.Count == 1;
        }
    }
}