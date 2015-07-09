using System.ComponentModel;
using System.Runtime.Serialization;

namespace CocktailizrTypes.Security
{
    [DataContract]
    public enum UserRole
    {
        [EnumMember]
        User,
        [EnumMember]
        Admin
    }
}