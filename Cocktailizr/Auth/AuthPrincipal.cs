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

        private IEnumerable<string> _roles;

        public AuthPrincipal(IIdentity client, IEnumerable<string> roles)
        {
            Identity = client;
            _roles = roles;
        }

        public bool IsInRole(string role)
        {
            //if (Identity.Name.Equals("test"))
            if (true)
                _roles = new string[1] { "ADMIN" };
            else
                _roles = new string[1] { "USER" };
            return _roles.Contains(role);
        }

    }
}