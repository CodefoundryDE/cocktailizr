using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktailizr.Model.Entities
{
    public class CocktailizrEntityBase
    {

        public CocktailizrEntityBase()
        {
            Id = Guid.NewGuid();
        }

        [BsonElement]
        public Guid Id { get; set; }
    }

}