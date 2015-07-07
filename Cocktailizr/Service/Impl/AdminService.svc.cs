using Cocktailizr.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cocktailizr.Service.Impl
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "AdminService" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts AdminService.svc oder AdminService.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class AdminService : IAdminService
    {

        public bool AddCocktail(Cocktail cocktail)
        {
            throw new NotImplementedException();
        }

        public bool ModifyCocktail(Guid cocktailId, Cocktail cocktail)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCocktail(Guid cocktailId)
        {
            throw new NotImplementedException();
        }
    }
}
