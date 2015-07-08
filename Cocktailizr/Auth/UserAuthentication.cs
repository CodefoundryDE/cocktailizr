using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Cocktailizr.Model.Database;
using Cocktailizr.Model.Service;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cocktailizr.Auth
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        private BenutzerDbService _benutzerService;

        public UserAuthentication()
        {
            _benutzerService = CocktailizrServiceLocator.BenutzerService;
        }

        public UserAuthentication(BenutzerDbService benutzerService)
        {
            _benutzerService = benutzerService;
        }

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new SecurityTokenException("Username and Password are required.");
            }

            if (!_benutzerService.CredentialsOk(userName, password).Result)
            {
                throw new SecurityTokenException("Unknown Username or Incorrect Password");
            }

        }
    }

}