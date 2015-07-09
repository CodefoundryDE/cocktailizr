using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CocktailizrTypes.Model.Entities;
using CocktailizrTypes.Security;

namespace Cocktailizr.Service
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IAdminService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        Task<bool> CredentialsOk();

        [OperationContract]
        Task<UserRole> GetUserRole();

        [OperationContract]
        Task<Cocktail> AddCocktail(Cocktail cocktail);

        [OperationContract]
        Task<Cocktail> ModifyCocktail(Guid cocktailId, Cocktail cocktail);

        [OperationContract]
        Task<bool> RemoveCocktail(Guid cocktailId);
    }
}
