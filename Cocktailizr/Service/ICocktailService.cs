using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CocktailizrTypes.Model.Entities;

namespace Cocktailizr.Service
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "ICocktailService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface ICocktailService
    {
        [OperationContract]
        Cocktail GetRandomCocktail();

        //[OperationContract]
        //IEnumerable<Cocktail> GetCocktailsByName(string name);

        //[OperationContract]
        //IEnumerable<Cocktail> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten);
    }
}
