using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using Cocktailizr.Model.Service;

namespace Cocktailizr.Auth
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        private BenutzerService _benutzerService;

        public UserAuthentication()
        {
            _benutzerService = CocktailizrServiceLocator.BenutzerService;
        }

        public UserAuthentication(BenutzerService benutzerService)
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