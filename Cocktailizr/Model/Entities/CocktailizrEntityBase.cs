using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Configuration;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktailizr.Model.Entities
{

    [DataContract]
    public class CocktailizrEntityBase
    {

        public CocktailizrEntityBase()
        {
            Id = Guid.NewGuid();
        }

        [BsonElement]
        [DataMember]
        public Guid Id { get; set; }
    }

}