using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CocktailizrTypes.Model.Entities;
using MongoDB.Driver;

namespace Cocktailizr.Service
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "ICocktailService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface ICocktailService
    {
        [OperationContract]
        Task<Cocktail> GetRandomCocktail();

        [OperationContract]
        Task<IAsyncCursor<Cocktail>> GetCocktailsByName(string name);

        [OperationContract]
        Task<Cocktail> GetCocktailById(Guid guid);
[OperationContract]        Task<IEnumerable<Cocktail>> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten);

    }
}
