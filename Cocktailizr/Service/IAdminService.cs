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

        /// <summary>
        /// Prüft, ob die eingetragenen Zugangsdaten gültig sind.
        /// </summary>
        /// <returns>True, wenn die Zugangsdaten gültig sind.</returns>
        [OperationContract]
        Task<bool> CredentialsOk();

        /// <summary>
        /// Prüft die Rolle des aktuell angemeldeten Benutzers.
        /// </summary>
        /// <returns>Gibt die Rolle des aktuell angemeldeten Benutzers zurück.</returns>
        [OperationContract]
        Task<UserRole> GetUserRole();

        /// <summary>
        /// Fügt der Datenbank einen Cocktail hinzu.
        /// </summary>
        /// <param name="cocktail">Cocktail-Objekt</param>
        /// <returns>Das Cocktail-Objekt mit der neuen eindeutigen Id.</returns>
        [OperationContract]
        Task<Cocktail> AddCocktail(Cocktail cocktail);

        /// <summary>
        /// Ändert einen Cocktail in der Datenbank.
        /// </summary>
        /// <param name="cocktailId">Id des zu ändernden Cocktails.</param>
        /// <param name="cocktail">Cocktail-Objekt</param>
        /// <returns>Das Cocktail-Objekt mit der neuen eindeutigen Id.</returns>
        [OperationContract]
        Task<Cocktail> ModifyCocktail(Guid cocktailId, Cocktail cocktail);

        /// <summary>
        /// Löscht einen Cocktail aus der Datenbank.
        /// </summary>
        /// <param name="cocktailId">Id des zu löschenden Cocktails.</param>
        /// <returns>True, wenn der Vorgang erfolgreich war.</returns>
        [OperationContract]
        Task<bool> RemoveCocktail(Guid cocktailId);
    }
}
