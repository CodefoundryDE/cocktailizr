using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktailizr.Model.Entities
{
    [BsonIgnoreExtraElements]
    public class Zutat
    {
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public bool IsOptional { get; set; }

        [BsonElement, BsonRepresentation(BsonType.String)]
        public ZutatenSkala Skala { get; set; }
    }

    public enum ZutatenSkala
    {
        Cl,
        Liter,
        Stueck,
        Gramm,
        Priese,
    }

}
