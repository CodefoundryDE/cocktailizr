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

        /// <summary>
        /// Holt einen zufälligen Cocktail aus der Datenbank.
        /// </summary>
        /// <returns>Gibt ein Cocktail-Objekt zurück.</returns>
        [OperationContract]
        Task<Cocktail> GetRandomCocktail();

        /// <summary>
        /// Sucht einen Cocktail anhand seines Namens in der Datenbank.
        /// </summary>
        /// <param name="name">Name des Cocktails</param>
        /// <returns>Gibt ein Cocktail-Objekt zurück.</returns>
        [OperationContract]
        Task<List<Cocktail>> GetCocktailsByName(string name);

        /// <summary>
        /// Holt einen Cocktail anhand seiner Id aus der Datenbank.
        /// </summary>
        /// <param name="guid">Id des Cocktails.</param>
        /// <returns>Gibt ein Cocktail-Objekt zurück.</returns>
        [OperationContract]
        Task<Cocktail> GetCocktailById(Guid guid);

        /// <summary>
        /// Sucht einen Cocktail anhand seiner Zutaten in der Datenbank.
        /// </summary>
        /// <param name="zutaten">Eine Menge von Zutaten, die zur Verfügung stehen.</param>
        /// <returns>Gibt eine Liste von Cocktail-Objekten zurück.</returns>
        [OperationContract]
        Task<List<Cocktail>> GetCocktailsByIndigrents(IEnumerable<Zutat> zutaten);

        /// <summary>
        /// Gibt alle verfügbaren Zutaten aus.
        /// </summary>
        /// <returns>Gibt eine Liste von Zutat-Objekten zurück.</returns>
        [OperationContract]
        Task<List<Zutat>> GetAllZutaten();

    }
}
