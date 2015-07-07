using CocktailizrTypes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
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
        //[PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public Cocktail GetRandomCocktail()
        {
            var count = _context.Cocktails.CountAsync(new BsonDocument()).Result;
            var rnd = (int)LongRandom(0, count > int.MaxValue ? count : int.MaxValue, new Random());

            return _context.Cocktails.Find(new BsonDocument()).Skip(rnd).FirstOrDefaultAsync().Result;
        }

        public IEnumerable<Cocktail> GetCocktailsByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cocktail> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten)
        {
            throw new NotImplementedException();
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
