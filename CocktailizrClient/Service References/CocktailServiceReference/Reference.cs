﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.18444
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CocktailizrClient.CocktailServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Color", Namespace="http://schemas.datacontract.org/2004/07/System.Drawing")]
    [System.SerializableAttribute()]
    public partial struct Color : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private short knownColorField;
        
        private string nameField;
        
        private short stateField;
        
        private long valueField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short knownColor {
            get {
                return this.knownColorField;
            }
            set {
                if ((this.knownColorField.Equals(value) != true)) {
                    this.knownColorField = value;
                    this.RaisePropertyChanged("knownColor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public short state {
            get {
                return this.stateField;
            }
            set {
                if ((this.stateField.Equals(value) != true)) {
                    this.stateField = value;
                    this.RaisePropertyChanged("state");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long value {
            get {
                return this.valueField;
            }
            set {
                if ((this.valueField.Equals(value) != true)) {
                    this.valueField = value;
                    this.RaisePropertyChanged("value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CocktailServiceReference.ICocktailService")]
    public interface ICocktailService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetRandomCocktail", ReplyAction="http://tempuri.org/ICocktailService/GetRandomCocktailResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<CocktailizrTypes.Model.Entities.Zutat, decimal>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrClient.CocktailServiceReference.Color))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.CocktailizrEntityBase))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Rezept))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Step[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Step))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Zutat))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.ZutatenSkala))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Cocktail[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Zutat[]))]
        CocktailizrTypes.Model.Entities.Cocktail GetRandomCocktail();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetRandomCocktail", ReplyAction="http://tempuri.org/ICocktailService/GetRandomCocktailResponse")]
        System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail> GetRandomCocktailAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailsByName", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailsByNameResponse")]
        CocktailizrTypes.Model.Entities.Cocktail[] GetCocktailsByName(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailsByName", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailsByNameResponse")]
        System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail[]> GetCocktailsByNameAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailById", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailByIdResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<CocktailizrTypes.Model.Entities.Zutat, decimal>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrClient.CocktailServiceReference.Color))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.CocktailizrEntityBase))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Rezept))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Step[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Step))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Zutat))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.ZutatenSkala))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Cocktail[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CocktailizrTypes.Model.Entities.Zutat[]))]
        CocktailizrTypes.Model.Entities.Cocktail GetCocktailById(System.Guid guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailById", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailByIdResponse")]
        System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail> GetCocktailByIdAsync(System.Guid guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailsByIndigrents", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailsByIndigrentsResponse")]
        CocktailizrTypes.Model.Entities.Cocktail[] GetCocktailsByIndigrents(CocktailizrTypes.Model.Entities.Zutat[] zutaten);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetCocktailsByIndigrents", ReplyAction="http://tempuri.org/ICocktailService/GetCocktailsByIndigrentsResponse")]
        System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail[]> GetCocktailsByIndigrentsAsync(CocktailizrTypes.Model.Entities.Zutat[] zutaten);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetAllZutaten", ReplyAction="http://tempuri.org/ICocktailService/GetAllZutatenResponse")]
        CocktailizrTypes.Model.Entities.Zutat[] GetAllZutaten();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICocktailService/GetAllZutaten", ReplyAction="http://tempuri.org/ICocktailService/GetAllZutatenResponse")]
        System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Zutat[]> GetAllZutatenAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICocktailServiceChannel : CocktailizrClient.CocktailServiceReference.ICocktailService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CocktailServiceClient : System.ServiceModel.ClientBase<CocktailizrClient.CocktailServiceReference.ICocktailService>, CocktailizrClient.CocktailServiceReference.ICocktailService {
        
        public CocktailServiceClient() {
        }
        
        public CocktailServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CocktailServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CocktailServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CocktailServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CocktailizrTypes.Model.Entities.Cocktail GetRandomCocktail() {
            return base.Channel.GetRandomCocktail();
        }
        
        public System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail> GetRandomCocktailAsync() {
            return base.Channel.GetRandomCocktailAsync();
        }
        
        public CocktailizrTypes.Model.Entities.Cocktail[] GetCocktailsByName(string name) {
            return base.Channel.GetCocktailsByName(name);
        }
        
        public System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail[]> GetCocktailsByNameAsync(string name) {
            return base.Channel.GetCocktailsByNameAsync(name);
        }
        
        public CocktailizrTypes.Model.Entities.Cocktail GetCocktailById(System.Guid guid) {
            return base.Channel.GetCocktailById(guid);
        }
        
        public System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail> GetCocktailByIdAsync(System.Guid guid) {
            return base.Channel.GetCocktailByIdAsync(guid);
        }
        
        public CocktailizrTypes.Model.Entities.Cocktail[] GetCocktailsByIndigrents(CocktailizrTypes.Model.Entities.Zutat[] zutaten) {
            return base.Channel.GetCocktailsByIndigrents(zutaten);
        }
        
        public System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Cocktail[]> GetCocktailsByIndigrentsAsync(CocktailizrTypes.Model.Entities.Zutat[] zutaten) {
            return base.Channel.GetCocktailsByIndigrentsAsync(zutaten);
        }
        
        public CocktailizrTypes.Model.Entities.Zutat[] GetAllZutaten() {
            return base.Channel.GetAllZutaten();
        }
        
        public System.Threading.Tasks.Task<CocktailizrTypes.Model.Entities.Zutat[]> GetAllZutatenAsync() {
            return base.Channel.GetAllZutatenAsync();
        }
    }
}
