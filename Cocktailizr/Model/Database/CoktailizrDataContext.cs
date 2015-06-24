using System.Collections.Generic;
using Cocktailizr.Model.Entities;
using MongoDB.Driver;

namespace Cocktailizr.Model.Database
{
    public class CocktailizrDataContext
    {
        private readonly IMongoDatabase _mongoDb;

        public IMongoCollection<Cocktail> Cocktails { get { return _mongoDb.GetCollection<Cocktail>(Cocktailizr.Properties.Resources.CocktailsCollectionName); } }

        public IMongoCollection<Zutat> Zutaten { get { return _mongoDb.GetCollection<Zutat>(Cocktailizr.Properties.Resources.ZutatenCollectionName); } }

        public CocktailizrDataContext()
        {
            _mongoDb = MongoClientFactory.DatabaseConnection;
        }



    }
}