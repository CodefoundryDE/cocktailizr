using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Cocktailizr.Auth
{
    public class AuthPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        private readonly IEnumerable<string> _roles;

        public AuthPrincipal(IIdentity client, IEnumerable<string> roles)
        {
            Identity = client;
            _roles = roles;
        }

        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }

    }
}