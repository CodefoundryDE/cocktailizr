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
using CocktailizrTypes.Security;

namespace Cocktailizr.Model.Service
{
    public class BenutzerDbService
    {
        private readonly CocktailizrDataContext _context;

        public BenutzerDbService()
        {
            _context = new CocktailizrDataContext();
        }

        public async Task<bool> CredentialsOk(string userName, string password)
        {
            try
            {
                var task = await _context.Benutzer.FindAsync(x => x.Name.Equals(userName));
                var benutzer = (await task.ToListAsync()).First();

                return PasswordHashHelper.VerifyPassword(benutzer.HashedPassword, password);
            }
            catch
            {
                // Benutzer nicht gefunden
                return false;
            }
        }

        public async Task<UserRole> GetUserRole(string userName)
        {
            try
            {
                var benutzer = await _context.Benutzer.FindAsync(x => x.Name.Equals(userName));

                var role = (await benutzer.ToListAsync()).First().Role;

                switch (role)
                {
                    case "ADMIN":
                        return UserRole.Admin;
                    default:
                        return UserRole.User;
                }
            }
            catch
            {
                // Benutzer / Rolle nicht gefunden
                return UserRole.User;
            }
        }
    }
}