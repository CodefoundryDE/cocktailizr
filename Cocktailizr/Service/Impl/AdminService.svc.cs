using Cocktailizr.Model.Database;
using CocktailizrTypes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cocktailizr.Service.Impl
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "AdminService" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts AdminService.svc oder AdminService.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class AdminService : IAdminService
    {
        private readonly CocktailizrDataContext _context;

        public AdminService()
        {
            _context = new CocktailizrDataContext();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<Cocktail> AddCocktail(Cocktail cocktail)
        {
            await _context.Cocktails.InsertOneAsync(cocktail);
            return cocktail;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<Cocktail> ModifyCocktail(Guid cocktailId, Cocktail cocktail)
        {
            await _context.Cocktails.ReplaceOneAsync(Builders<Cocktail>.Filter.Eq(x => x.Id, cocktailId), cocktail);
            return cocktail;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<bool> RemoveCocktail(Guid cocktailId)
        {
            try
            {
                await _context.Cocktails.DeleteOneAsync(Builders<Cocktail>.Filter.Eq(x => x.Id, cocktailId));
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

