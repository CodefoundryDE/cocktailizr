using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailizrClient.Message
{
    public class LoginMessage
    {
        public LoginAction LoginAction { get; set; }
    }


    public enum LoginAction
    {
        Show,
        Cancel,
        RoleChange
    }
}
