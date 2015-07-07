using System;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using CocktailizrTypes.Model.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Cocktailizr.Model.Database
{
    internal static class MongoClientFactory
    {

        private static readonly MongoClient MongoClient;

        static MongoClientFactory()
        {
            SetupClassMap();

            MongoClient = new MongoClient(ConfigurationManager.ConnectionStrings["CocktailizrDb"].ConnectionString);
        }

        private static void SetupClassMap()
        {
            var modelTypes =
                Assembly.GetAssembly(typeof(MongoClientFactory)).GetTypes()
                .Where(t => string.Equals(t.Namespace, "Entities", StringComparison.Ordinal))
                .Where(t => t != typeof(CocktailizrEntityBase));

            foreach (var entity in modelTypes)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(entity));
            }
        }

        public static IMongoDatabase DatabaseConnection
        {
            get { return MongoClient.GetDatabase(ConfigurationManager.ConnectionStrings["CocktailizrDb"].Name); }
        }

    }
}