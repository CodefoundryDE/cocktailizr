using System.Collections;
using System.Collections.Generic;
using CocktailizrTypes.Model.Entities;
using GalaSoft.MvvmLight.Messaging;

namespace CocktailizrClient.Message
{
    public class CocktailSearchMessage : MessageBase
    {
        public CocktailSearchType CocktailSearchType { get; set; }

        public string SearchString { get; set; }

        public IEnumerable<Zutat> Ingredients { get; set; }

    }

    public enum CocktailSearchType
    {
        Random,
        ByName,
        ByIngredients,
    }
}