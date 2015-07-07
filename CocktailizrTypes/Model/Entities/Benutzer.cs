using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CocktailizrTypes.Model.Entities
{
    public class Benutzer : CocktailizrEntityBase
    {
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public string Role { get; set; }

        [BsonElement]
        public string HashedPassword { get; set; }
    }
}
