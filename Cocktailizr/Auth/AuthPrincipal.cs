using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using CocktailizrTypes.Security;

namespace Cocktailizr.Auth
{
    public class AuthPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        private readonly UserRole _role;

        public AuthPrincipal(IIdentity client, UserRole role)
        {
            Identity = client;
            _role = role;
        }

        public bool IsInRole(string role)
        {
            return _role.ToString().ToUpper().Equals(role);
        }

    }
}