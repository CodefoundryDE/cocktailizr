using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Cocktailizr.Model.Database;
using CocktailizrTypes.Model.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cocktailizr.Model.Service
{
    public class CocktailDbService
    {
        private CocktailizrDataContext _context;

        public CocktailDbService()
        {
            _context = new CocktailizrDataContext();
        }


        public async Task<Cocktail> GetRandomCocktail()
        {
            int rnd;
            try
            {
                var count = (int)await _context.Cocktails.CountAsync(new BsonDocument());
                rnd = new Random().Next(0, count);
            }
            catch (OverflowException ex)
            {
                rnd = new Random().Next();
            }
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

        public async Task<IAsyncCursor<Cocktail>> GetAllZutaten()
        {
            try
            {
                var unwind = new BsonDocument()
                {
                    {
                        "$unwind", "$Zutaten"
                    }
                };
                var match = new BsonDocument
            {
                {
                    "$match",
                    new BsonDocument
                    {
                        {"Zutaten", new BsonDocument
                            {
                                {"$exists", true}
                            }
                        }
                    }
                }
            };

            var pipeline = new[] { match, unwind };
                
                var result = await _context.Cocktails.AggregateAsync<object>(pipeline);

                await result.MoveNextAsync();
                var x = result.Current;

                return null;
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return null;
        }
    }
}