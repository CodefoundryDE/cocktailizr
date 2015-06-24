using Cocktailizr.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cocktailizr.Service.Impl
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "CocktailService" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts CocktailService.svc oder CocktailService.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class CocktailService : ICocktailService
    {
        public Cocktail GetRandomCocktail()
        {
            return new Cocktail()
            {
                Name = "Bloody Mary"
            };
        }

        public IEnumerable<Cocktail> GetCocktailsByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cocktail> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten)
        {
            throw new NotImplementedException();
        }
    }
}
