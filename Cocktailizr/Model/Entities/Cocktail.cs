using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cocktailizr.Helper.Serializer;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Cocktailizr.Model.Entities
{
    [BsonIgnoreExtraElements]
    public class Cocktail
    {
        [BsonElement]
        public string Name { get; set; }

        [BsonElement]
        public IEnumerable Tags { get; set; }

        [BsonElement, BsonSerializer(typeof(ColorSerializer))]
        public Color DrinkColor { get; set; }

        [BsonElement]
        public bool Alcoholic { get; set; }
        [BsonElement, BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public IDictionary<decimal, Zutat> Zutaten { get; set; }

        [BsonElement]
        public Rezept Rezept { get; set; }


    }

    [BsonIgnoreExtraElements]
    public class Rezept
    {
        [BsonElement]
        public TimeSpan Zubereitungszeit { get; set; }

        [BsonElement]
        public IEnumerable<Step> ZubereitungsSchritte { get; set; }

    }

    [BsonIgnoreExtraElements]
    public class Step
    {
        [BsonElement]
        public string Beschreibung { get; set; }

        [BsonElement]
        public string Headline { get; set; }

    }
}
