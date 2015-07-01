using System;
using System.Collections.Generic;
using CocktailizrTypes.Model.Entities;
using MongoDB.Driver;

namespace Cocktailizr.Model.Database
{
    public class CocktailizrDataContext
    {
        private readonly IMongoDatabase _mongoDb;

        public IMongoCollection<Cocktail> Cocktails { get { return _mongoDb.GetCollection<Cocktail>(Properties.Resources.CocktailsCollectionName); } }

        public CocktailizrDataContext()
        {
            _mongoDb = MongoClientFactory.DatabaseConnection;
        }
    }
}