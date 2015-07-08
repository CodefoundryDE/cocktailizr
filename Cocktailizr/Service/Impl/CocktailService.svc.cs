﻿using CocktailizrTypes.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cocktailizr.Model.Database;
using Cocktailizr.Model.Service;
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

        private readonly CocktailDbService _cocktailDbService;

        #endregion

        #region Constructor

        public CocktailService()
        {
            _cocktailDbService = new CocktailDbService();
        }

        #endregion

        #region Methods
        //[PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task<Cocktail> GetRandomCocktail()
        {
            var cocktail = await _cocktailDbService.GetCocktailById(new Guid("6556d36b-0901-944e-806c-baa9cf58584a"));
            return await _cocktailDbService.GetRandomCocktail();
        }

        public async Task<IEnumerable<Cocktail>> GetCocktailsByName(string name)
        {
            var cocktails = await _cocktailDbService.GetCocktailsByName(name);
            return await cocktails.ToListAsync();
        }

        public async Task<IEnumerable<Cocktail>> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten)
        {

            return await _cocktailDbService.GetCocktailsByIndigrents(zutaten);
        }

        public async Task<IEnumerable<Zutat>> GetAllZutaten()
        {
            return await _cocktailDbService.GetAllZutaten();
        }
        public async Task<Cocktail> GetCocktailById(Guid guid)
        {
            return await _cocktailDbService.GetCocktailById(guid);
        }
        #endregion






    }
}
