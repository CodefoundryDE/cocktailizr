﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Configuration;
using CocktailizrTypes.Helper.Serializer;
using GalaSoft.MvvmLight;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CocktailizrTypes.Model.Entities
{

    [DataContract]
    public class CocktailizrEntityBase : INotifyPropertyChanged
    {
        public CocktailizrEntityBase()
        {
            Id = Guid.Empty;
        }

        private Guid _id;

        [BsonElement]
        [DataMember, BsonSerializer(typeof(GuidSerializer))]
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}