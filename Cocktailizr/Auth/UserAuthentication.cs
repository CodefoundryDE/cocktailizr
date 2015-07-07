using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Cocktailizr.Auth
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new FaultException("username and password required");
            }

            if (userName != "test" || password != "test123")
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }

        }
    }

}