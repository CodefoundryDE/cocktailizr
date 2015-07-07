using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cocktailizr.Model.Entities;

namespace Cocktailizr.Service
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IAdminService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        bool AddCocktail(Cocktail cocktail);

        [OperationContract]
        bool ModifyCocktail(Guid cocktailId, Cocktail cocktail);

        [OperationContract]
        bool RemoveCocktail(Guid cocktailId);
    }
}
