﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using CocktailizrTypes.Helper.Serializer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace CocktailizrTypes.Model.Entities
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class Cocktail : CocktailizrEntityBase
    {
        [DataMember]
        [BsonElement]
        public string Name { get; set; }

        [DataMember]
        [BsonElement]
        public IEnumerable Tags { get; set; }

        [DataMember]
        [BsonElement, BsonSerializer(typeof(ColorSerializer))]
        public Color DrinkColor { get; set; }

        [DataMember]
        [BsonElement]
        public bool Alcoholic { get; set; }

        [DataMember]
        [BsonElement, BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public IDictionary<Zutat, decimal> Zutaten { get; set; }

        [DataMember]
        [BsonElement]
        public Rezept Rezept { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [DataContract]
    [BsonIgnoreExtraElements]
    public class Rezept
    {
        [DataMember]
        [BsonElement]
        public TimeSpan Zubereitungszeit { get; set; }

        [DataMember]
        [BsonElement]
        public IEnumerable<Step> ZubereitungsSchritte { get; set; }

    }

    [DataContract]
    [BsonIgnoreExtraElements]
    public class Step
    {
        [DataMember]
        [BsonElement]
        public string Beschreibung { get; set; }

        [DataMember]
        [BsonElement]
        public string Headline { get; set; }

    }

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

    [DataContract]
    public enum ZutatenSkala
    {
        [EnumMember]
        Cl,
        [EnumMember]
        Liter,
        [EnumMember]
        Stueck,
        [EnumMember]
        Gramm,
        [EnumMember]
        Priese,
    }
}