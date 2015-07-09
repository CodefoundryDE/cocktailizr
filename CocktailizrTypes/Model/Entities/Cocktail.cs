using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
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
        private string _name;


        [DataMember]
        [BsonElement]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<string> _tags;
        [DataMember]
        [BsonElement]
        public IEnumerable<string> Tags
        {
            get { return _tags ?? (_tags = new List<string>()); }
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }

        private Color _drinkColor;

        [DataMember]
        [BsonElement, BsonSerializer(typeof(ColorBsonSerializer))]
        public Color DrinkColor
        {
            get { return _drinkColor; }
            set
            {
                _drinkColor = value;
                OnPropertyChanged();
            }
        }

        private bool _alcoholic;

        [DataMember]
        [BsonElement]
        public bool Alcoholic
        {
            get { return _alcoholic; }
            set
            {
                _alcoholic = value;
                OnPropertyChanged();
            }
        }

        private IDictionary<Zutat, decimal> _zutaten;

        [DataMember]
        [BsonElement, BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public IDictionary<Zutat, decimal> Zutaten
        {
            get { return _zutaten ?? (_zutaten = new Dictionary<Zutat, decimal>()); }
            set
            {
                _zutaten = value;
                OnPropertyChanged();
            }
        }


        private Rezept _rezept;
        private byte[] _imageBytes;

        [DataMember]
        [BsonElement]
        public Rezept Rezept
        {
            get { return _rezept ?? (_rezept = new Rezept()); }
            set
            {
                _rezept = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        [BsonElement]
        public byte[] ImageBytes
        {
            get { return _imageBytes ?? (_imageBytes = new byte[] { }); }
            set
            {
                _imageBytes = value; 
                OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged("Image");
            }
        }

        [BsonIgnore]
        public Image Image
        {
            get
            {
                if (ImageBytes == null) { return null; }
                var ms = new MemoryStream(ImageBytes);
                return ms.Length > 0 ? new Bitmap(ms) : null;
            }
            set
            {
                if (value == null)
                {
                    ImageBytes = new byte[] { };
                    return;
                }
                using (var ms = new MemoryStream())
                {
                    value.Save(ms, ImageFormat.Bmp);
                    ImageBytes = ms.ToArray();
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [DataContract]
    [BsonIgnoreExtraElements]
    public class Rezept : INotifyPropertyChanged
    {
        private IEnumerable<Step> _zubereitungsSchritte;
        private TimeSpan _zubereitungszeit;

        [DataMember]
        [BsonElement]
        public TimeSpan Zubereitungszeit
        {
            get { return _zubereitungszeit; }
            set
            {
                _zubereitungszeit = value; OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged("ZubereitungMinutes");
            }
        }

        public double ZubereitungMinutes
        {
            get { return Zubereitungszeit.TotalMinutes; }
            set
            {
                Zubereitungszeit = TimeSpan.FromMinutes(value);
                OnPropertyChanged();
            }
        }

        [DataMember]
        [BsonElement]
        public IEnumerable<Step> ZubereitungsSchritte
        {
            get { return _zubereitungsSchritte ?? (_zubereitungsSchritte = new List<Step>()); }
            set { _zubereitungsSchritte = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
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
    public class Zutat : INotifyPropertyChanged
    {
        private string _name = String.Empty;
        private bool _isOptional;
        private EZutatenSkala _skala = EZutatenSkala.Stueck;

        [DataMember]
        [BsonElement]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        [BsonElement]
        public bool IsOptional
        {
            get { return _isOptional; }
            set
            {
                _isOptional = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        [BsonElement, BsonRepresentation(BsonType.String)]
        public EZutatenSkala Skala
        {
            get { return _skala; }
            set
            {
                _skala = value;
                OnPropertyChanged();
            }
        }

        [BsonIgnore]
        public bool IsSelected { get; set; }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Zutat p = obj as Zutat;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.GetHashCode() == GetHashCode());
        }

        public bool Equals(Zutat p)
        {
            // If parameter is null return false:
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.GetHashCode() == GetHashCode());
        }

        public override int GetHashCode()
        {
            var hash = Name.GetHashCode() * 17;
            hash += Skala.GetHashCode() * 17;
            return hash;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    [DataContract]
    public enum EZutatenSkala
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
