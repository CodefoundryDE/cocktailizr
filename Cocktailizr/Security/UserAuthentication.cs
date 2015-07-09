using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using Cocktailizr.Model.Service;
using Cocktailizr.Properties;

namespace Cocktailizr.Security
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        private readonly BenutzerDbService _benutzerService;

        public UserAuthentication()
        {
            _benutzerService = CocktailizrServiceLocator.BenutzerDbService;
        }

        public UserAuthentication(BenutzerDbService benutzerService)
        {
            _benutzerService = benutzerService;
        }

        public override void Validate(string userName, string password)
        {
            if (userName.Equals(Resources.AnonymousCredentials) && password.Equals(Resources.AnonymousCredentials))
            {
                return;
            }
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