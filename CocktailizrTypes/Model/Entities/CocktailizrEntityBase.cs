using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Configuration;
using CocktailizrTypes.Helper.Serializer;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CocktailizrTypes.Model.Entities
{

    [DataContract]
    public class CocktailizrEntityBase
    {

        public CocktailizrEntityBase()
        {
            Id = Guid.NewGuid();
        }

        [BsonElement]
        [DataMember, BsonSerializer(typeof(GuidSerializer))]
        public Guid Id { get; set; }
    }

}