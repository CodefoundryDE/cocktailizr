using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Configuration;
using GalaSoft.MvvmLight;
using MongoDB.Bson.Serialization.Attributes;

namespace CocktailizrTypes.Model.Entities
{

    [DataContract]
    public class CocktailizrEntityBase : ObservableObject
    {
        public CocktailizrEntityBase()
        {
            Id = Guid.NewGuid();
        }

        private Guid _id;

        [BsonElement]
        [DataMember]
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
    }

}