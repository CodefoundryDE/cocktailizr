using System;
using System.ServiceModel;

namespace CocktailizrTypes.Model.Message
{
    [MessageContract]
    public class DownloadRequest
    {
         [MessageBodyMember]
         public Guid CocktailGuid { get; set; }
    }
}