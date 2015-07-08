using System;
using System.Collections.Concurrent;
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

            var cocktail = await _context.Cocktails.Find(new BsonDocument()).Skip(rnd).FirstOrDefaultAsync();
            return cocktail;
        }

        public async Task<IAsyncCursor<Cocktail>> GetCocktailsByName(string name)
        {
            return await _context.Cocktails.FindAsync(cocktail => cocktail.Name.Contains(name));
        }

        public async Task<IEnumerable<Cocktail>> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten)
        {
                var cocktails = await _context.Cocktails.Find(c => c.Zutaten.Any()).ToListAsync();
                var cocktailsFiltered = cocktails.Where(cocktail => cocktail.Zutaten.Keys.Except(zutaten).Count() <= 2);
                return cocktailsFiltered.Distinct();
        }

        public async Task<ISet<Zutat>> GetAllZutaten()
        {
            var cocktails = await _context.Cocktails.Find(c => c.Zutaten.Any()).ToListAsync();
            ISet<Zutat> zutatenSet = new HashSet<Zutat>();
            foreach (var zutat in cocktails.SelectMany(cocktail => cocktail.Zutaten))
            {
                zutat.Key.IsOptional = false;
                zutatenSet.Add(zutat.Key);
            }
            return zutatenSet;
        }



        public async Task<Cocktail> GetCocktailById(Guid guid)
        {
            var cocktailCursor = await _context.Cocktails.FindAsync(c => c.Id.Equals(new Guid("6bd35665-0109-4e94-806c-baa9cf58584a")));
            var matchingCocktails = await cocktailCursor.ToListAsync();
            return matchingCocktails.FirstOrDefault();
        }
    }
}