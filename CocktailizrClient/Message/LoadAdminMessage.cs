using CocktailizrTypes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace CocktailizrClient.Message
{
    public class LoadAdminMessage : MessageBase
    {
        public Cocktail CocktailToBeEdited;
    }
}
