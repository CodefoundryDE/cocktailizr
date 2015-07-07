using Cocktailizr.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cocktailizr.Model.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cocktailizr.Service.Impl
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "CocktailService" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts CocktailService.svc oder CocktailService.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class CocktailService : ICocktailService
    {
        #region Properties



        #endregion

        #region Vriables

        private readonly CocktailizrDataContext _context;

        #endregion

        #region Constructor

        public CocktailService()
        {
            _context = new CocktailizrDataContext();
        }

        #endregion

        #region Methods

        public async Task<Cocktail> GetRandomCocktail()
        {
            var count = await _context.Cocktails.CountAsync(new BsonDocument());
            var rnd = (int)LongRandom(0, count > int.MaxValue ? count : int.MaxValue, new Random());

            return await _context.Cocktails.Find(new BsonDocument()).Skip(rnd).FirstOrDefaultAsync();
        }

        public async Task<IAsyncCursor<Cocktail>> GetCocktailsByName(string name)
        {
            return await _context.Cocktails.FindAsync(cocktail => cocktail.Name.Contains(name));
        }

        public async Task<IAsyncCursor<Cocktail>> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten)
        {
           return await _context.Cocktails.FindAsync(cocktail => !cocktail.Zutaten.Keys.Except(zutaten).Any());
        }

        #region DontLookAtIt

        long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
        }

        #endregion

        #endregion
    }
}
