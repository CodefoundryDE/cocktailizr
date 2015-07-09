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
using Cocktailizr.Model.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using CocktailizrTypes.Security;

namespace Cocktailizr.Service.Impl
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "AdminService" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts AdminService.svc oder AdminService.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class AdminService : IAdminService
    {
        private readonly CocktailDbService _cocktailDbService;
        private readonly BenutzerDbService _benutzerDbService;

        public AdminService()
        {
            _cocktailDbService = CocktailizrServiceLocator.CocktailDbService;
            _benutzerDbService = CocktailizrServiceLocator.BenutzerDbService;
        }

        public async Task<bool> CredentialsOk()
        {
            return await Task.FromResult(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.IsAuthenticated);
        }

        public Task<UserRole> GetUserRole()
        {
            return _benutzerDbService.GetUserRole(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<Cocktail> AddCocktail(Cocktail cocktail)
        {
            return await _cocktailDbService.AddCocktail(cocktail);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<Cocktail> ModifyCocktail(Guid cocktailId, Cocktail cocktail)
        {
            return await _cocktailDbService.ModifyCocktail(cocktailId, cocktail);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<bool> RemoveCocktail(Guid cocktailId)
        {
            return await _cocktailDbService.RemoveCocktail(cocktailId);
        }

    }
}

