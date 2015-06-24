using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktailizr.Model.Entities
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class Zutat
    {
        [DataMember]
        [BsonElement]
        public string Name { get; set; }

        [DataMember]
        [BsonElement]
        public bool IsOptional { get; set; }

        [DataMember]
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
